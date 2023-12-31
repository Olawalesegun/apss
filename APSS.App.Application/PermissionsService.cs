﻿using APSS.Domain.Entities;
using APSS.Domain.Entities.Util;
using APSS.Domain.Repositories;
using APSS.Domain.Repositories.Extensions;
using APSS.Domain.Services;
using APSS.Domain.Services.Exceptions;

namespace APSS.Application.App;

public sealed class PermissionsService : IPermissionsService
{
    #region Fields

    private readonly IUnitOfWork _uow;

    #endregion Fields

    #region Public Constructors

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="uow">The unit of work of the application</param>
    public PermissionsService(IUnitOfWork uow)
    {
        _uow = uow;
    }

    #endregion Public Constructors

    #region Public Methods

    /// <inheritdoc/>
    public async Task<(Account, Account)> ValidateAccountPatenthoodAsync(
        long superUserAccountId,
        long accountId,
        PermissionType permissions)
    {
        var account = await _uow.Accounts.Query()
            .Include(a => a.User)
            .FindAsync(accountId);

        var superUserAccount = await ValidateUserPatenthoodAsync(superUserAccountId, account.User.Id, permissions);

        return (superUserAccount, account);
    }

    /// <inheritdoc/>
    public async Task<Account> ValidateUserPatenthoodAsync(
        long accountId,
        long userId,
        PermissionType permissions)
    {
        var account = await _uow.Accounts.Query()
            .Include(a => a.User)
            .FindAsync(accountId);

        var distance = await GetSubuserDistanceAsync(account.User.Id, userId, false);

        if (!CorrelateDistanceWithPermissions(distance, permissions, account.Permissions))
        {
            throw new InsufficientPermissionsException(
                accountId,
                 $"account #{accountId} of user #{account.User.Id} with permissions {account.Permissions.ToSetFormattedString()} does not have permissions {permissions.ToSetFormattedString()} on or does not own user #{userId}");
        }

        return account;
    }

    /// <inheritdoc/>
    public async Task<Account> ValidatePermissionsAsync(long accountId, long userId, PermissionType permissions)
    {
        var account = await _uow.Accounts.Query()
            .Include(a => a.User)
            .FindAsync(accountId);

        var distance = await GetSubuserDistanceAsync(account.User.Id, userId);

        if (!CorrelateDistanceWithPermissions(distance, permissions, account.Permissions))
        {
            throw new InsufficientPermissionsException(
                accountId,
                $"account #{accountId} of user #{account.User.Id} with permissions {account.Permissions.ToSetFormattedString()} does not have permissions {permissions.ToSetFormattedString()} on user #{userId}");
        }

        return account;
    }

    #endregion Public Methods

    #region Private Methods

    private static bool CorrelateDistanceWithPermissions(
        int distance,
        PermissionType expectedPermissions,
        PermissionType actualPermissions)
    {
        if (distance < 0)
            return false;

        if (distance == 0)
            return actualPermissions.HasFlag(expectedPermissions);

        return actualPermissions.HasFlag(expectedPermissions) && actualPermissions.HasFlag(PermissionType.Read);
    }

    private async Task<int> GetSubuserDistanceAsync(long superuserId, long subuserId, bool checkSelf = true)
    {
        if (checkSelf && superuserId == subuserId)
            return 0;

        var superuser = await _uow.Users.Query().FindAsync(superuserId);

        if (superuser.AccessLevel == AccessLevel.Root)
            return 0;

        var subuser = await _uow.Users
            .Query()
            .Include(u => u.SupervisedBy!)
            .FindAsync(subuserId);

        if (superuser.AccessLevel.IsBelow(subuser.AccessLevel))
            return -1;

        for (int i = 0; ; ++i)
        {
            if (subuser.SupervisedBy is null)
                return -1;

            if (subuser.SupervisedBy.Id == superuser.Id)
                return i;

            subuser = await _uow.Users
                .Query()
                .Include(u => u.SupervisedBy!)
                .FindAsync(subuser.SupervisedBy.Id);
        }
    }

    #endregion Private Methods
}