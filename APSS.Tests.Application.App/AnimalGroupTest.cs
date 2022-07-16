﻿using APSS.Domain.Entities;
using APSS.Domain.Repositories;
using APSS.Domain.Repositories.Exceptions;
using APSS.Domain.Repositories.Extensions;
using APSS.Domain.Repositories.Extensions.Exceptions;
using APSS.Domain.Services;
using APSS.Domain.Services.Exceptions;
using APSS.Tests.Domain.Entities.Validators;
using APSS.Tests.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace APSS.Tests.Application.App
{
    public class AnimalGroupTest
    {
        #region Private Field

        private readonly IUnitOfWork _uow;
        private readonly IAnimalService _animal;

        #endregion Private Field

        #region Constructor

        public AnimalGroupTest(IUnitOfWork uow, IAnimalService animal)
        {
            _uow = uow;
            _animal = animal;
        }

        #endregion Constructor

        #region Test

        [Theory]
        [InlineData(AccessLevel.Farmer, PermissionType.Create, true)]
        [InlineData(AccessLevel.Farmer, PermissionType.Read | PermissionType.Delete | PermissionType.Update, false)]
        [InlineData(AccessLevel.Group, PermissionType.Create, false)]
        [InlineData(AccessLevel.Governorate, PermissionType.Create, false)]
        [InlineData(AccessLevel.Directorate, PermissionType.Create, false)]
        [InlineData(AccessLevel.Village, PermissionType.Create, false)]
        [InlineData(AccessLevel.District, PermissionType.Create, false)]
        [InlineData(AccessLevel.Presedint, PermissionType.Create, false)]
        public async Task<(Account, AnimalGroup?)> AnimalAddedTheory(
            AccessLevel accessLevel = AccessLevel.Farmer,
            PermissionType permessionType = PermissionType.Create,
            bool shouldSuccess = true)
        {
            var account = await _uow.CreateTestingAccountAsync(accessLevel, permessionType);
            var animalobj = ValidEntitiesFactory.CreateValidAnimalGroup();

            var addAnimalGroupTask = _animal.AddAnimalGroupAsync(
                account.Id,
                animalobj.Type,
                animalobj.Name,
                animalobj.Quantity,
                animalobj.Sex
                );

            if (!shouldSuccess)
            {
                await Assert.ThrowsAsync<InvalidAccessLevelException>(async () => await addAnimalGroupTask);
                return (account, null);
            }

            var animalGroup = await addAnimalGroupTask;

            Assert.True(await _uow.AnimalGroups.Query().ContainsAsync(animalGroup));
            Assert.Equal(account.User.Id, animalGroup.OwnedBy.Id);
            Assert.Equal(animalobj.Type, animalGroup.Type);
            Assert.Equal(animalobj.Name, animalGroup.Name);
            Assert.Equal(animalobj.Quantity, animalGroup.Quantity);
            Assert.Equal(animalobj.Sex, animalGroup.Sex);

            return (account, animalGroup);
        }

        [Theory]
        [InlineData(PermissionType.Delete, true)]
        [InlineData(PermissionType.Create | PermissionType.Update | PermissionType.Read, false)]
        public async Task AnimalDeleteTheory(PermissionType permissionType, bool ShouldSuccess = true)
        {
            var (account, animalGroup) = await AnimalAddedTheory(
                AccessLevel.Farmer,
                PermissionType.Create | permissionType, true
                );

            Assert.True(await _uow.AnimalGroups.Query().ContainsAsync(animalGroup!));

            var otherAccount = await _uow.CreateTestingAccountAsync(AccessLevel.Farmer, permissionType);

            var DeleteAnimalTask = _animal.RemoveAnimalGroupAsync(account.Id, animalGroup!.Id);

            if (!ShouldSuccess)
            {
                await Assert.ThrowsAsync<InvalidPermissionsExceptions>(async () => await DeleteAnimalTask);
                return;
            }
            await DeleteAnimalTask;
            Assert.False(await _uow.AnimalGroups.Query().ContainsAsync(animalGroup));
        }

        [Theory]
        [InlineData(AccessLevel.Farmer, PermissionType.Create, true)]
        [InlineData(AccessLevel.Farmer, PermissionType.Read | PermissionType.Delete | PermissionType.Update, false)]
        [InlineData(AccessLevel.Group, PermissionType.Create, false)]
        [InlineData(AccessLevel.Governorate, PermissionType.Create, false)]
        [InlineData(AccessLevel.Directorate, PermissionType.Create, false)]
        [InlineData(AccessLevel.Village, PermissionType.Create, false)]
        [InlineData(AccessLevel.District, PermissionType.Create, false)]
        [InlineData(AccessLevel.Presedint, PermissionType.Create, false)]
        public async Task<(Account, AnimalProduct?)> AnimalProductAddedTheory(
            AccessLevel accesssLevel = AccessLevel.Farmer,
            PermissionType permissionType = PermissionType.Create,
            bool shouldSuccess = true
            )
        {
            /*var (account, animalGroup) = await AnimalAddedTheory(
               accesssLevel,
               permissionType, shouldSuccess
               );*/
            var account = await _uow.CreateTestingAccountAsync(AccessLevel.Farmer, PermissionType.Create);
            var templateGroup = ValidEntitiesFactory.CreateValidAnimalGroup();

            var animalGroup = await _animal.AddAnimalGroupAsync(
                account.Id,
                templateGroup.Type,
                templateGroup.Name,
                templateGroup.Quantity,
                templateGroup.Sex);

            var productUnit = await AnimalProductUnitAddedTheory(accesssLevel, permissionType, shouldSuccess);

            var animalProduct = ValidEntitiesFactory.CreateValidAnimalProduct();

            var unit = ValidEntitiesFactory.CreateValidAnimalProductUnit();

            var animalProductTask = _animal.AddAnimalProductAsync(
                account.Id,
                animalGroup!.Id,
                unit!.Id,
                animalProduct.Name,
                animalProduct.Quantity,
                animalProduct.PeriodTaken

                );
            if (animalProductTask == null)
            {
                throw new InvalidOperationException();
            }
            if (!shouldSuccess)
            {
                await Assert.ThrowsAsync<NotFoundException>(async () => await animalProductTask);
                return (account, null);
            }

            var product = await animalProductTask;

            Assert.True(await _uow.AnimalProducts.Query().ContainsAsync(product));
            Assert.Equal(account.User.Id, product.Producer.OwnedBy.Id);
            Assert.Equal(animalProduct.Name, product.Name);
            Assert.Equal(animalProduct.Quantity, product.Quantity);
            Assert.Equal(animalProduct.PeriodTaken, product.PeriodTaken);

            return (account, product);
        }

        [Theory]
        [InlineData(PermissionType.Delete, true)]
        [InlineData(PermissionType.Create | PermissionType.Update | PermissionType.Read, false)]
        public async Task AnimalProductDeletedTheory(PermissionType permissionType, bool shouldSuccess = true)
        {
            var (account, animalProduct) = await AnimalProductAddedTheory(
                AccessLevel.Farmer,
                PermissionType.Delete | permissionType,
                true
                );

            Assert.True(await _uow.AnimalProducts.Query().ContainsAsync(animalProduct!));

            /*  var otherAccount = await _uow.CreateTestingAccountAsync(AccessLevel.Farmer, PermissionType.Delete);*/

            var removeAninalProductTask = _animal.RemoveAnimalProductAsync(account.Id, animalProduct!.Id);

            if (!shouldSuccess)
            {
                await Assert.ThrowsAsync<InvalidPermissionsExceptions>(async () => await removeAninalProductTask);
                return;
            }

            await removeAninalProductTask;
            Assert.False(await _uow.AnimalProducts.Query().ContainsAsync(animalProduct));
        }

        [Theory]
        [InlineData(AccessLevel.Farmer, PermissionType.Create, true)]
        [InlineData(AccessLevel.Farmer, PermissionType.Read | PermissionType.Delete | PermissionType.Update, false)]
        [InlineData(AccessLevel.Group, PermissionType.Create, false)]
        [InlineData(AccessLevel.Governorate, PermissionType.Create, false)]
        [InlineData(AccessLevel.Directorate, PermissionType.Create, false)]
        [InlineData(AccessLevel.Village, PermissionType.Create, false)]
        [InlineData(AccessLevel.District, PermissionType.Create, false)]
        [InlineData(AccessLevel.Presedint, PermissionType.Create, false)]
        public async Task<AnimalProductUnit?> AnimalProductUnitAddedTheory(AccessLevel accessLevel, PermissionType permissionType, bool shouldSuccess)
        {
            var account = await _uow.CreateTestingAccountAsync(accessLevel, permissionType);

            var templateProductUnit = ValidEntitiesFactory.CreateValidLandProductUnit();

            var productUnitTask = _animal.AddAnimalProductUnit(account.Id, templateProductUnit.Name);

            if (!shouldSuccess)
            {
                await Assert.ThrowsAsync<InvalidAccessLevelException>(async () => await productUnitTask);
                return null;
            }

            var productUnit = await productUnitTask;
            Assert.True(await _uow.AnimalProductUnits.Query().ContainsAsync(productUnit));
            Assert.Equal(templateProductUnit.Name, productUnit.Name);
            return productUnit;
        }

        #endregion Test
    }
}