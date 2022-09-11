using APSS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APSS.Domain.Services;

public interface IConfirmSrevice
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
    /// <returns>The confimred or declined land</returns>
    Task<LandProduct> ConfirmProductAsync(long accountId, long landProductId, bool confirm);
}