using System.Text;

using APSS.Domain.Entities;
using APSS.Domain.Repositories;
using APSS.Domain.Repositories.Extensions;
using APSS.Domain.Services;

namespace APSS.Application.App;

public sealed class AccountsService : IAccountsService
{
    #region Fields

    private const int PASSWORD_SALT_LENGTH = 0x7F;

    private readonly ICryptoHashService _cryptoHashSvc;
    private readonly IPermissionsService _permissionsSvc;
    private readonly IRandomGeneratorService _rndSvc;
    private readonly IUnitOfWork _uow;

    #endregion Fields

    #region Public Constructors

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="permissionsSvc">The permissions managment service</param>
    public AccountsService(
        IRandomGeneratorService rndSvc,
        ICryptoHashService cryptoHashSvc,
        IUnitOfWork uow,
        IPermissionsService permissionsSvc)
    {
        _rndSvc = rndSvc;
        _cryptoHashSvc = cryptoHashSvc;
        _uow = uow;
        _permissionsSvc = permissionsSvc;
    }

    #endregion Public Constructors

    #region Public Methods

    /// <inheritdoc/>
    public async Task<Account> CreateAsync(
        long superUserAccountId,
        long userId,
        string holderName,
        string password,
        PermissionType permissions,
        bool isActive)
    {
        var superAccount = await _permissionsSvc
            .ValidatePermissionsAsync(superUserAccountId, userId, PermissionType.Create | permissions);

        var user = await _uow.Users.Query().FindAsync(userId);

        return await DoCreateAsync(user, superAccount.User, holderName, password, permissions, isActive);
    }

    /// <inheritdoc/>
    public Task<Account> CreateUncheckedAsync(
        User owner,
        string holderName,
        string password,
        PermissionType permissions,
        bool isActive,
        IAsyncDatabaseTransaction? tx)
    {
        return DoCreateAsync(owner, owner, holderName, password, permissions, isActive, tx);
    }

    /// <inheritdoc/>
    public async Task RemoveAsync(long superUserAccountId, long accountId)
    {
        var (_, account) = await _permissionsSvc.ValidateAccountPatenthoodAsync(
            superUserAccountId,
            accountId,
            PermissionType.Delete);

        _uow.Accounts.Remove(account);
        await _uow.CommitAsync();
    }

    /// <inheritdoc/>
    public async Task<Account> SetActiveAsync(long superUserAccountId, long accountId, bool newActiveStatus)
        => await UpdateAsync(
            superUserAccountId,
            accountId,
            a => a.IsActive = newActiveStatus);

    /// <inheritdoc/>
    public async Task<Account> SetPermissionsAsync(long superUserAccountId, long accountId, PermissionType permissions)
        => await UpdateAsync(
            superUserAccountId,
            accountId,
            a => a.Permissions = permissions);

    /// <inheritdoc/>
    public async Task<Account> UpdateAsync(long superUserAccountId, long accountId, Action<Account> updater)
    {
        var (_, account) = await _permissionsSvc.ValidateAccountPatenthoodAsync(
            superUserAccountId,
            accountId,
            PermissionType.Update);

        updater(account);

        _uow.Accounts.Update(account);
        await _uow.CommitAsync();

        return account;
    }

    /// <inheritdoc/>
    public IQueryBuilder<Account> GetAccountAsync(long accountId)
        => _uow.Accounts.Query().Where(a => a.Id == accountId);

    /// <inheritdoc/>
    public async Task<IQueryBuilder<Account>> GetAccountsAsync(long accountId, long userId)
    {
        await _permissionsSvc.ValidatePermissionsAsync(accountId, userId, PermissionType.Read);

        return _uow.Accounts.Query().Where(a => a.User.Id == userId);
    }

    #endregion Public Methods

    #region Private Methods

    private async Task<Account> DoCreateAsync(
        User owner,
        User addedBy,
        string holderName,
        string password,
        PermissionType permissions,
        bool isActive,
        IAsyncDatabaseTransaction? tx = null)
    {
        var passwordSalt = _rndSvc.NextBytes(PASSWORD_SALT_LENGTH).ToArray();
        var passwordHash = await _cryptoHashSvc.HashAsync(Encoding.UTF8.GetBytes(password), passwordSalt);

        var account = new Account
        {
            User = owner,
            HolderName = holderName,
            PasswordHash = Convert.ToBase64String(passwordHash),
            PasswordSalt = Convert.ToBase64String(passwordSalt),
            Permissions = permissions,
            AddedBy = addedBy,
            IsActive = isActive,
        };

        _uow.Accounts.Add(account);

        await _uow.CommitAsync(tx);

        return account;
    }

    #endregion Private Methods
}