﻿using System.Linq;
using System.Threading.Tasks;

using APSS.Domain.Entities;
using APSS.Domain.Repositories;
using APSS.Domain.Repositories.Extensions;
using APSS.Domain.Services;
using APSS.Domain.Services.Exceptions;
using APSS.Tests.Domain.Entities.Validators;
using APSS.Tests.Extensions;

using Xunit;

namespace APSS.Tests.Application.App;

public sealed class AccountServiceTest
{
    #region Private fields

    private readonly IRandomGeneratorService _rndSvc;
    private readonly ICryptoHashService _cryptoHashSvc;
    private readonly IUnitOfWork _uow;
    private readonly IAccountsService _accountSvc;
    private readonly IUsersService _usersSvc;

    #endregion Private fields

    #region Constructors

    public AccountServiceTest(
        IRandomGeneratorService rndSvc,
        ICryptoHashService cryptoHashSvc,
        IUnitOfWork uow,
        IAccountsService accountsSvc,
        IUsersService userSvc)
    {
        _rndSvc = rndSvc;
        _cryptoHashSvc = cryptoHashSvc;
        _uow = uow;
        _accountSvc = accountsSvc;
        _usersSvc = userSvc;
    }

    #endregion Constructors

    #region Tests

    [Theory]
    [InlineData(PermissionType.Create, true)]
    [InlineData(PermissionType.Update, false)]
    [InlineData(PermissionType.Delete, false)]
    [InlineData(PermissionType.Read, false)]
    public async Task<(Account, Account?)> AccountsAddedTheory(
           PermissionType permissions = PermissionType.Create, bool shouldSucceed = true)
    {
        var user = ValidEntitiesFactory.CreateValidUser(_rndSvc.NextAccessLevel());

        _uow.Users.Add(user);
        await _uow.CommitAsync();

        var templateAccount = ValidEntitiesFactory.CreateValidAccount(permissions);
        var password = _rndSvc.NextString(64);

        _uow.Accounts.Add(templateAccount);
        await _uow.CommitAsync();

        Assert.True(await _uow.Accounts.Query().ContainsAsync(templateAccount));
        Assert.True(await _uow.Users.Query().ContainsAsync(user));

        var superAccount =
            await _uow
            .CreateTestingAccountAboveUserAsync(user.Id, user.AccessLevel.NextLevelUpove(), permissions);

        var createAccountTask = _accountSvc.CreateAsync(
            superAccount.Id,
            user.Id,
            templateAccount.HolderName,
            password,
            templateAccount.Permissions);

        if (!shouldSucceed)
        {
            await Assert.ThrowsAsync<InsufficientPermissionsException>(() => createAccountTask);
            return (superAccount, null);
        }

        var otherSuper = await _uow.CreateTestingAccountAsync(
            user.AccessLevel == AccessLevel.Farmer
                ? user.AccessLevel
                : user.AccessLevel.NextLevelBelow(),
            PermissionType.Create);

        await Assert.ThrowsAsync<InsufficientPermissionsException>(async () => await
              _accountSvc.CreateAsync(
              otherSuper.Id,
              user.Id,
              templateAccount.HolderName,
              password,
              templateAccount.Permissions));

        var account = await createAccountTask;

        Assert.True(await _uow.Accounts.Query().ContainsAsync(account));
        Assert.Equal(user, account.User);
        Assert.Equal(templateAccount.HolderName, account.HolderName);
        Assert.Equal(templateAccount.Permissions, account.Permissions);
        Assert.True(await _cryptoHashSvc.VerifyAsync(password, account.PasswordHash, account.PasswordSalt));

        return (superAccount, account);
    }

    [Theory]
    [InlineData(PermissionType.Create, false)]
    [InlineData(PermissionType.Update, true)]
    [InlineData(PermissionType.Delete, false)]
    [InlineData(PermissionType.Read, false)]
    public async Task AccountUpdatedTheory(
        PermissionType permissions = PermissionType.Update, bool shouldSuccesd = true)
    {
        var (superAccount, account) = await AccountsAddedTheory();

        Assert.True(await _uow.Accounts.Query().ContainsAsync(account!));
        Assert.True(await _uow.Accounts.Query().ContainsAsync(superAccount));

        var name = _rndSvc.NextString(40, RandomStringOptions.Mixed);
        var phone = _rndSvc.NextString(15, RandomStringOptions.Numeric);
        var job = _rndSvc.NextString(20, RandomStringOptions.Alpha);

        var superAccountnew = await _uow
            .CreateTestingAccountForUserAsync(superAccount.User.Id, permissions);

        Assert.True(await _uow.Accounts.Query().ContainsAsync(superAccountnew));

        var updateAccountTask = _accountSvc
            .UpdateAsync(
            superAccountnew.Id,
            account!.Id,
            a =>
            {
                a.HolderName = name;
                a.PhoneNumber = phone;
                a.Job = job;
            });

        if (!shouldSuccesd)
        {
            await Assert.ThrowsAsync<InsufficientPermissionsException>(() => updateAccountTask);
            return;
        }

        var othersuper = _uow
            .CreateTestingAccountAsync(superAccount!.User.AccessLevel, PermissionType.Update);

        await Assert.ThrowsAsync<InsufficientPermissionsException>(async () => await
        _accountSvc.UpdateAsync(
                    othersuper.Id,
                    account!.Id,
                    a =>
                    {
                        a.HolderName = name;
                        a.PhoneNumber = phone;
                        a.Job = job;
                    }));

        var accountnew = await updateAccountTask;

        var accountupdated = await _uow.Accounts.Query().FindAsync(accountnew.Id);

        Assert.Equal(name, accountupdated.HolderName);
        Assert.Equal(phone, accountupdated.PhoneNumber);
        Assert.Equal(job, accountupdated.Job);
    }

    [Theory]
    [InlineData(PermissionType.Create, false)]
    [InlineData(PermissionType.Update, true)]
    [InlineData(PermissionType.Delete, false)]
    [InlineData(PermissionType.Read, false)]
    public async Task AccountActivatedTheory(
        PermissionType permissions = PermissionType.Update, bool shouldSuccesd = true)
    {
        var (superAccount, account) = await AccountsAddedTheory();

        Assert.True(await _uow.Accounts.Query().ContainsAsync(account!));

        var superAccountnew = await _uow
            .CreateTestingAccountForUserAsync(superAccount.User.Id, permissions);

        var status = _rndSvc.NextBool();

        var activeAccountTask = _accountSvc.SetActiveAsync(superAccountnew.Id, account!.Id, status);

        if (!shouldSuccesd)
        {
            await Assert.ThrowsAsync<InsufficientPermissionsException>(() => activeAccountTask);
            return;
        }

        var othersuper = _uow
            .CreateTestingAccountAsync(superAccount!.User.AccessLevel, PermissionType.Update);

        await Assert.ThrowsAsync<InsufficientPermissionsException>(async () => await
                     _accountSvc.SetActiveAsync(othersuper.Id, account!.Id, status));

        var accountnew = await activeAccountTask;

        var accountupdated = await _uow.Accounts.Query().FindAsync(accountnew.Id);

        Assert.Equal(status, accountupdated.IsActive);
    }

    [Theory]
    [InlineData(PermissionType.Create, false)]
    [InlineData(PermissionType.Update, true)]
    [InlineData(PermissionType.Delete, false)]
    [InlineData(PermissionType.Read, false)]
    public async Task SetPermissionTheory(
        PermissionType permissions = PermissionType.Update, bool shouldSuccesd = true)
    {
        var (superAccount, account) = await AccountsAddedTheory();

        Assert.True(await _uow.Accounts.Query().ContainsAsync(account!));

        var superAccountnew = await _uow
            .CreateTestingAccountForUserAsync(superAccount.User.Id, permissions);

        var permissionnew = permissions;

        var setPermissionTask =
            _accountSvc.SetPermissionsAsync(superAccountnew.Id, account!.Id, permissionnew);

        if (!shouldSuccesd)
        {
            await Assert.ThrowsAsync<InsufficientPermissionsException>(() => setPermissionTask);
            return;
        }

        var othersuper = _uow
            .CreateTestingAccountAsync(superAccount!.User.AccessLevel, PermissionType.Update);

        await Assert.ThrowsAsync<InsufficientPermissionsException>(async () => await
                  _accountSvc.SetPermissionsAsync(othersuper.Id, account!.Id, permissionnew));

        var accountnew = await setPermissionTask;

        var accountupdated = await _uow.Accounts.Query().FindAsync(accountnew.Id);

        Assert.Equal(permissionnew, accountupdated.Permissions);
    }

    [Theory]
    [InlineData(PermissionType.Update, false)]
    [InlineData(PermissionType.Delete, true)]
    [InlineData(PermissionType.Create, false)]
    [InlineData(PermissionType.Read, false)]
    public async Task AccountRemovedTheory(
        PermissionType permissions, bool shouldSucceed)
    {
        var (superaccount, account) = await AccountsAddedTheory();

        Assert.True(await _uow.Accounts.Query().ContainsAsync(account!));

        var supaerAcountnew = await _uow
            .CreateTestingAccountForUserAsync(superaccount.User.Id, permissions);

        var removAccountTask = _accountSvc.RemoveAsync(supaerAcountnew.Id, account!.Id);

        if (!shouldSucceed)
        {
            await Assert.ThrowsAsync<InsufficientPermissionsException>(() => removAccountTask);
            return;
        }

        await removAccountTask;

        Assert.False(await _uow.Accounts.Query().ContainsAsync(account!));
    }

    #endregion Tests
}