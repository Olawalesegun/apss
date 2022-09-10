using APSS.Domain.Entities;
using APSS.Domain.Repositories;
using APSS.Domain.Repositories.Extensions;
using APSS.Domain.Services;
using APSS.Domain.Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APSS.Application.App
{
    internal class ConfirmService : IConfirmSrevice
    {
        private readonly IPermissionsService _permissionsService;
        private readonly IUnitOfWork _uow;

        public ConfirmService(IUnitOfWork uow, IPermissionsService permissionsService)
        {
            _uow = uow;
            _permissionsService = permissionsService;
        }

        public async Task<AnimalGroup> ConfirmAnimalGroup(long accountId, long animalGroupId, bool isConfirm)
        {
            var animalGroup = await _uow.AnimalGroups.Query().
            Include(u => u.OwnedBy!)
            .Where(s => s.Id == animalGroupId).FirstAsync();

            await _permissionsService.ValidateUserPatenthoodAsync(accountId, animalGroup.OwnedBy.Id, PermissionType.Update);

            if (isConfirm)
            {
                _uow.AnimalGroups.Confirm(animalGroup);
            }
            else
            {
                _uow.AnimalGroups.Decline(animalGroup);
            }

            await _uow.CommitAsync();

            return animalGroup;
        }

        public async Task<AnimalProduct> ConfirmAnimalProduct(long accountId, long animalProductId, bool isConfirm)
        {
            var animalProduct = await _uow.AnimalProducts.Query()
                .Include(u => u.Producer.OwnedBy)
                .FindAsync(animalProductId);
            var farmer = await _uow.Users.Query().Include(a => a.Accounts).FindAsync(animalProduct.Producer.OwnedBy.Id);

            await _permissionsService.ValidateUserPatenthoodAsync(accountId, farmer.Id, PermissionType.Update);

            if (isConfirm) _uow.AnimalProducts.Confirm(animalProduct);
            else _uow.AnimalProducts.Decline(animalProduct);

            await _uow.CommitAsync();
            return animalProduct;
        }

        public Task<Land> ConfirmLandAsync(long accountId, long landId, bool confirm)
        {
            throw new NotImplementedException();
        }

        public Task<LandProduct> ConfirmProductAsync(long accountId, long landProductId, bool confirm)
        {
            throw new NotImplementedException();
        }
    }
}