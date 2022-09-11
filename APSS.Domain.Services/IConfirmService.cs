using APSS.Domain.Entities;
using APSS.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APSS.Domain.Services;

public interface IConfirmService
{
    Task<AnimalProduct> ConfirmAnimalProduct(long accountId, long animalProductId, bool isConfirm);

    Task<AnimalGroup> ConfirmAnimalGroup(long accountId, long animalGroupId, bool isConfirm);

    /// <summary>
    /// Asynchronously confirms or decline land information
    /// </summary>
    /// <param name="accountId">The account id of the land owner supervisor</param>
    /// <param name="landId">The id of the land</param>
    /// <param name="confirm">The status of the land</param>
    /// <returns>The confimred or declined land</returns>
    Task<Land> ConfirmLandAsync(long accountId, long landId, bool confirm);

    /// <summary>
    /// Asynchronously confirms or decline land product information
    /// </summary>
    /// <param name="accountId">The account id of the land product owner supervisor</param>
    /// <param name="landProductId">The id of the land product</param>
    /// <param name="confirm">The status of the land</param>
    /// <returns>The confimred or declined land product</returns>
    Task<LandProduct> ConfirmLandProductAsync(long accountId, long landProductId, bool confirm);

    /// <summary>
    /// Asynchronously gets all Unconfirmed lands for group access level account
    /// </summary>
    /// <param name="accountId">The id of the account who wants to show the Unconfirmed lands</param>
    /// <returns>list of the unconfirmed lands under that user</returns>
    Task<IQueryBuilder<Land>> DeclinedLandsAsync(long accountId);

    /// <summary>
    /// Asynchronously gets all confirmed lands for group access level account
    /// </summary>
    /// <param name="accountId">The id of the account who wants to show the confirmed lands</param>
    /// <returns>list of the confirmed lands under that user</returns>
    Task<IQueryBuilder<Land>> ConfirmedLandsAsync(long accountId);

    /// <summary>
    /// Asynchronously gets all Unconfirmed land products for group access level account
    /// </summary>
    /// <param name="accountId">The id of the account who wants to show the Unconfirmed land products</param>
    /// <returns>list of the unconfirmed land products under that user</returns>
    Task<IQueryBuilder<LandProduct>> DeclinedLandProductsAsync(long accountId);

    /// <summary>
    /// Asynchronously gets all confirmed land products for group access level account
    /// </summary>
    /// <param name="accountId">The id of the account who wants to show the confirmed land products</param>
    /// <returns>list of the confirmed land products under that user</returns>
    Task<IQueryBuilder<LandProduct>> ConfirmedLandProductsAsync(long accountId);

    /// <summary>
    /// Asynchronously gets all user unconfirmed land products
    /// </summary>
    /// <param name="accountId">The account id of the group user who wants to get the products</param>
    /// <returns>The products list that are not confirmed </returns>
    Task<IQueryBuilder<LandProduct>> UnConfirmedLandProductsAsync(long accountId);

    /// <summary>
    /// Asynchronously gets all user unconfirmed lands
    /// </summary>
    /// <param name="accountId">The account id of the group user who wants to get the lands</param>
    /// <returns>The lands list that are not confirmed </returns>
    Task<IQueryBuilder<Land>> UnConfirmedLandsAsync(long accountId);

    Task<IQueryBuilder<AnimalGroup>> GetAllAnimal(long accountId);

    Task<IQueryBuilder<AnimalProduct>> GetAllAnimalProduct(long accountId);
}