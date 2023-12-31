﻿using APSS.Domain.Entities;
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
    /// <param name="isActive">Whether the account should be active on creation</param>
    /// <returns>The created account object</returns>
    Task<Account> CreateAsync(
        long superUserAccountId,
        long userId,
        string holderName,
        string password,
        PermissionType permissions,
        bool isActive = true);

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
    /// <param name="isActive">Whether the account should be active on creation</param>
    /// <param name="tx">An optional transaction object</param>
    /// <returns>The created account object</returns>
    Task<Account> CreateUncheckedAsync(
        User owner,
        string holderName,
        string password,
        PermissionType permissions,
        bool isActive = true,
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
    /// Asynchronously gets account details using ID
    /// </summary>
    /// <param name="superUserAccountId">The id of the account to make the change with</param>
    /// <param name="accountId">The id of the account to get its data</param>
    /// <returns></returns>
    Task<Account> GetAccountAsync(long superUserAccountId, long accountId);

    /// <summary>
    /// Asynchronously gets accounts for a user
    /// </summary>
    /// <param name="accountId">The id of the account to access data with</param>
    /// <param name="userId">The id of the user to get accounts for</param>
    /// <returns></returns>
    Task<IQueryBuilder<Account>> GetAccountsAsync(long accountId, long userId);

    /// <summary>
    /// Asynchronously updates an account's password
    /// </summary>
    /// <param name="superUserAccountId">The id of the account to make the change with</param>
    /// <param name="accountId">The id of the account to change its passowrd</param>
    /// <param name="password">The new password</param>
    /// <returns></returns>
    Task<Account> UpdatePasswordAsync(long superUserAccountId, long accountId, string password);

    #endregion Public Methods
}