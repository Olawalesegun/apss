using APSS.Domain.Entities;
using APSS.Domain.Repositories;

namespace APSS.Domain.Services;

/// <summary>
/// An interface to be implemented by accounts service
/// </summary>
public interface IAccountsService
{
    #region Public Methods

    /// <summary>
    /// Asynchrnously creates an account for a user
    /// </summary>
    /// <param name="superUserAccountId">The id of the account of the superuser of the user</param>
    /// <param name="userId">The id of the user to add the account for</param>
    /// <param name="holderName">The name of the holder of the account</param>
    /// <param name="password">The password of the account</param>
    /// <param name="permissions">The permissions of the account</param>
    /// <returns>The created account object</returns>
    Task<Account> CreateAsync(
        long superUserAccountId,
        long userId,
        string holderName,
        string password,
        PermissionType permissions);

    /// <summary>
    /// Asynchrnously creates an account for a user, without checking for permissions
    ///
    /// This method should only be used internally, since it overrides the system's internal
    /// security validatoin mechanisms. The created account will be owned by the user identified by <see cref="userId"/>
    /// </summary>
    /// <param name="owner">The user to use as the owner of the account</param>
    /// <param name="holderName">The name of the holder of the account</param>
    /// <param name="password">The password of the account</param>
    /// <param name="permissions">The permissions of the account</param>
    /// <param name="tx">An optional transaction object</param>
    /// <returns>The created account object</returns>
    Task<Account> CreateUncheckedAsync(
        User owner,
        string holderName,
        string password,
        PermissionType permissions,
        IAsyncDatabaseTransaction? tx = null);

    /// <summary>
    /// Asynchronously removes an account
    /// </summary>
    /// <param name="superUserAccountId">The id of the account to remove with</param>
    /// <param name="accountId">The id of the account to remove</param>
    /// <returns></returns>
    Task RemoveAsync(long superUserAccountId, long accountId);

    /// <summary>
    /// Asynchronously enables/disables an account
    /// </summary>
    /// <param name="superUserAccountId">The id of the account to make the change with</param>
    /// <param name="accountId">The id of the account to enable/disable</param>
    /// <param name="newActiveStatus">The new active status</param>
    /// <returns>The updated account</returns>
    Task<Account> SetActiveAsync(long superUserAccountId, long accountId, bool newActiveStatus);

    /// <summary>
    /// Asynchronously sets permissions of an account
    /// </summary>
    /// <param name="superUserAccountId">The id of the account to make the change with</param>
    /// <param name="accountId">The id of the account whose permissoins are to be set</param>
    /// <param name="permissions">The new permissoins</param>
    /// <returns>The updated account</returns>
    Task<Account> SetPermissionsAsync(long superUserAccountId, long accountId, PermissionType permissions);

    /// <summary>
    /// Asynchronously updates an account
    /// </summary>
    /// <param name="superUserAccountId">The id of the account to make the change with</param>
    /// <param name="accountId">The id of the account to be updated</param>
    /// <param name="updater">The updating callback</param>
    /// <returns>The updated account</returns>
    Task<Account> UpdateAsync(long superUserAccountId, long accountId, Action<Account> updater);

    /// <summary>
    /// Asynchronously Get Account
    /// </summary>
    /// <param name="SuperId">The id of the account who seeks for the account</param>
    /// <param name="accountId">the id of the found account </param>
    /// <returns>The Account</returns>
    Task<Account> GetAccountAsync(long SuperId, long accountId);

    Task<IQueryBuilder<Account>> GetUserAccounts(long accountId, long userId);

    #endregion Public Methods
}