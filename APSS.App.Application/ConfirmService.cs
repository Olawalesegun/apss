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
        private readonly IPermissionsService _permissionsSvc;
        private readonly IUnitOfWork _uow;

        public ConfirmService(IUnitOfWork uow, IPermissionsService permissionsService)
        {
            _uow = uow;
            _permissionsSvc = permissionsService;
        }

        public async Task<AnimalGroup> ConfirmAnimalGroup(long accountId, long animalGroupId, bool isConfirm)
        {
            var animalGroup = await _uow.AnimalGroups.Query().
            Include(u => u.OwnedBy!)
            .Where(s => s.Id == animalGroupId).FirstAsync();

            await _permissionsSvc.ValidateUserPatenthoodAsync(accountId, animalGroup.OwnedBy.Id, PermissionType.Update);

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

            await _permissionsSvc.ValidateUserPatenthoodAsync(accountId, farmer.Id, PermissionType.Update);

            if (isConfirm) _uow.AnimalProducts.Confirm(animalProduct);
            else _uow.AnimalProducts.Decline(animalProduct);

            await _uow.CommitAsync();
            return animalProduct;
        }

        /// <inhertdoc/>
        public async Task<Land> ConfirmLandAsync(long accountId, long landId, bool confirm)
        {
            var account = await _uow.Accounts.Query()
                .FindWithAccessLevelValidationAsync(accountId, AccessLevel.Group, PermissionType.Update);
            var land = await _uow.Lands.Query()
                .Include(u => u.OwnedBy)
                .FindAsync(landId);

            await _permissionsSvc.ValidateUserPatenthoodAsync(
                accountId,
                land.OwnedBy.Id,
                PermissionType.Update);

            if (confirm)
                _uow.Lands.Confirm(land);
            else
                _uow.Lands.Decline(land);

            await _uow.CommitAsync();

            return land;
        }

        /// <inhertdoc/>
        public async Task<LandProduct> ConfirmLandProductAsync(long accountId, long landProductId, bool confirm)
        {
            var account = await _uow.Accounts.Query()
                .FindWithAccessLevelValidationAsync(accountId, AccessLevel.Group, PermissionType.Update);
            var landProduct = await _uow.LandProducts.Query()
                .Include(a => a.AddedBy)
                .FindAsync(landProductId);

            await _permissionsSvc.ValidateUserPatenthoodAsync(
                accountId,
                landProduct.AddedBy.Id,
                PermissionType.Update);

            if (confirm)
                _uow.LandProducts.Confirm(landProduct);
            else
                _uow.LandProducts.Decline(landProduct);
            await _uow.CommitAsync();

            return landProduct;
        }

        /// <inhertdoc/>
        public async Task<IQueryBuilder<Land>> DeclinedLandsAsync(long accountId)
        {
            var account = await _uow.Accounts.Query()
                .Include(u => u.User)
                .FindWithAccessLevelValidationAsync(accountId, AccessLevel.Group, PermissionType.Read);

            return _uow.Lands.Query()
                .Include(u => u.OwnedBy)
                .Include(u => u.OwnedBy.SupervisedBy!)
                .Where(l => l.OwnedBy.SupervisedBy!.Id == account.User.Id && l.IsConfirmed == false);
        }

        /// <inhertdoc/>
        public async Task<IQueryBuilder<Land>> UnConfirmedLandsAsync(long accountId)
        {
            var account = await _uow.Accounts.Query()
                .Include(u => u.User)
                .FindWithAccessLevelValidationAsync(accountId, AccessLevel.Group, PermissionType.Read);

            return _uow.Lands.Query()
                .Include(u => u.OwnedBy)
                .Include(u => u.OwnedBy.SupervisedBy!)
                .Where(l => l.OwnedBy.SupervisedBy!.Id == account.User.Id && l.IsConfirmed == null);
        }

        /// <inhertdoc/>
        public async Task<IQueryBuilder<Land>> ConfirmedLandsAsync(long accountId)
        {
            var account = await _uow.Accounts.Query()
                .Include(u => u.User)
                .FindWithAccessLevelValidationAsync(accountId, AccessLevel.Group, PermissionType.Read);

            return _uow.Lands.Query()
                .Include(u => u.OwnedBy)
                .Include(u => u.OwnedBy.SupervisedBy!)
                .Where(l => l.OwnedBy.SupervisedBy!.Id == account.User.Id && l.IsConfirmed == true);
        }

        /// <inhertdoc/>
        public async Task<IQueryBuilder<LandProduct>> DeclinedLandProductsAsync(long accountId)
        {
            var account = await _uow.Accounts.Query()
                .Include(u => u.User)
                .FindWithAccessLevelValidationAsync(accountId, AccessLevel.Group, PermissionType.Read);

            return _uow.LandProducts.Query()
                .Include(u => u.AddedBy)
                .Include(u => u.Unit)
                .Include(s => s.ProducedIn)
                .Include(p => p.Producer)
                .Include(s => s.AddedBy.SupervisedBy!)
                .Where(p => p.AddedBy.SupervisedBy!.Id == account.User.Id && p.IsConfirmed == false);
        }

        /// <inhertdoc/>
        public async Task<IQueryBuilder<LandProduct>> UnConfirmedLandProductsAsync(long accountId)
        {
            var account = await _uow.Accounts.Query()
                .Include(u => u.User)
                .FindWithAccessLevelValidationAsync(accountId, AccessLevel.Group, PermissionType.Read);

            return _uow.LandProducts.Query()
                .Include(u => u.AddedBy)
                .Include(u => u.Unit)
                .Include(s => s.ProducedIn)
                .Include(p => p.Producer)
                .Include(s => s.AddedBy.SupervisedBy!)
                .Where(p => p.AddedBy.SupervisedBy!.Id == account.User.Id && p.IsConfirmed == null);
        }

        /// <inhertdoc/>
        public async Task<IQueryBuilder<LandProduct>> ConfirmedLandProductsAsync(long accountId)
        {
            var account = await _uow.Accounts.Query()
                .Include(u => u.User)
                .FindWithAccessLevelValidationAsync(accountId, AccessLevel.Group, PermissionType.Read);

            return _uow.LandProducts.Query()
                .Include(u => u.AddedBy)
                .Include(s => s.AddedBy.SupervisedBy!)
                .Where(p => p.AddedBy.SupervisedBy!.Id == account.User.Id && p.IsConfirmed == true);
        }
    }
}