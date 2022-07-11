﻿using APSS.Domain.Entities;
using APSS.Domain.Repositories.Extensions.Exceptions;
using APSS.Domain.Services.Exceptions;

namespace APSS.Domain.Repositories.Extensions;

public static class QueryBuilderSecurityExtensions
{
    /// <summary>
    /// Asynchronously gets an account by id, with access level and permissions validation
    /// </summary>
    /// <param name="self"></param>
    /// <param name="accountId">The id of the account to get and validate</param>
    /// <param name="accessLevel">The required access level</param>
    /// <param name="permissions">Optional permissions</param>
    /// <returns>The account with the supplied account</returns>
    /// <exception cref="InvalidAccessLevelException"></exception>
    public static async Task<Account> FindWithAccessLevelValidationAsync(
        this IQueryBuilder<Account> self,
        long accountId,
        AccessLevel accessLevel,
        PermissionType permissions)
    {
        var account = await self
            .Include(a => a.User)
            .FindAsync(accountId);

        if (account.User.AccessLevel != accessLevel || !account.Permissions.HasFlag(permissions))
        {
            throw new InvalidAccessLevelException(
                accountId,
                account.User.Id,
                accessLevel,
                account.User.AccessLevel,
                permissions,
                account.Permissions);
        }

        return account;
    }

    /// <summary>
    /// Asynchronously gets an item with ownership validation
    /// </summary>
    /// <typeparam name="T">The type of the item</typeparam>
    /// <param name="self"></param>
    /// <param name="itemId">The id of the item to get</param>
    /// <param name="account">The account of the item's owner</param>
    /// <returns>The matching item</returns>
    /// <exception cref="InsufficientPermissionsException">
    /// Thrown if the accuont's user does not own the item
    /// </exception>
    public static async Task<T> FindWithOwnershipValidationAync<T>(
        this IQueryBuilder<T> self,
        long itemId,
        Account account) where T : Ownable
    {
        var item = await self
            .Include(i => i.OwnedBy)
            .FindAsync(itemId);

        if (account.User.Id != item.OwnedBy.Id)
        {
            throw new InsufficientPermissionsException(
                account.Id,
                $"user #{account.User.Id} with account #{account.Id} does not own {nameof(T)} #{itemId}");
        }

        return item;
    }
}