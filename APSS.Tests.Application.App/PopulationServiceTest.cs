﻿using APSS.Domain.Entities;
using APSS.Domain.Repositories;
using APSS.Domain.Repositories.Extensions;
using APSS.Domain.Repositories.Extensions.Exceptions;
using APSS.Domain.Services;
using APSS.Domain.Services.Exceptions;
using APSS.Tests.Domain.Entities.Validators;
using APSS.Tests.Extensions;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace APSS.Tests.Application.App;

public sealed class PopulationServiceTest: IDisposable
{
    #region Private fields

    private readonly IUnitOfWork _uow;
    private readonly IPopulationService _populationSvc;

    #endregion Private fields

    #region Constructors

    public PopulationServiceTest(IUnitOfWork uow, IPopulationService populationSvc)
    {
        _uow = uow;
        _populationSvc = populationSvc;
    }

    #endregion Constructors

    #region Tests

    [Theory]
    [InlineData(AccessLevel.Group, PermissionType.Create, true)]
    [InlineData(AccessLevel.Group, PermissionType.Update, false)]
    [InlineData(AccessLevel.Farmer, PermissionType.Create, false)]
    [InlineData(AccessLevel.Farmer, PermissionType.Read, false)]
    [InlineData(AccessLevel.Farmer, PermissionType.Delete, false)]
    [InlineData(AccessLevel.Farmer, PermissionType.Update, false)]
    [InlineData(AccessLevel.Village, PermissionType.Create, false)]
    [InlineData(AccessLevel.District, PermissionType.Create, false)]
    [InlineData(AccessLevel.Directorate, PermissionType.Create, false)]
    [InlineData(AccessLevel.Governorate, PermissionType.Create, false)]
    [InlineData(AccessLevel.Presedint, PermissionType.Create, false)]
    public async Task<(Account, Family?)> FamilyaddedTheory(
        AccessLevel accessLevel = AccessLevel.Group,
        PermissionType permissions = PermissionType.Create,
        bool shouldSucceed = true)
    {
        var account = await _uow.CreateTestingAccountAsync(accessLevel, permissions);
        var templateFamily = ValidEntitiesFactory.CreateValidFamily();

        var addFamilyTask = _populationSvc.AddFamilyAsync(
            account.Id,
           templateFamily.Name,
           templateFamily.LivingLocation);

        if (!shouldSucceed)
        {
            await Assert.ThrowsAsync<InvalidAccessLevelException>(async () => await addFamilyTask);
            return (account, null);
        }

        var family = await addFamilyTask;

        Assert.True(await _uow.Families.Query().ContainsAsync(family));
        Assert.Equal(account.User.Id, family.AddedBy.Id);
        Assert.Equal(templateFamily.Name, family.Name);
        Assert.Equal(templateFamily.LivingLocation, family.LivingLocation);

        return (account, family);
    }

    [Fact]
    public async Task<(Account, Individual?)> IndividualAddedFact()
    {
        var (account,family)=await FamilyaddedTheory(AccessLevel.Group ,PermissionType.Create,true);
        
        var templateIndividual = ValidEntitiesFactory.CreateValidIndividual();
        var addIndividualTask = _populationSvc
            .AddIndividualAsync(
            account.Id,
            family!.Id,
            templateIndividual.Name,
            templateIndividual.Address,
            templateIndividual.Sex,
            false,
            false
            );

        Assert.True(await _uow.Families.Query().ContainsAsync(family!));

        var individual = await addIndividualTask;
        Assert.True(await _uow.FamilyIndividuals.Query().AnyAsync(f =>  f.Individual.Id == individual.Id &  f.Family.Id==family.Id));
        Assert.True(await _uow.Individuals.Query().ContainsAsync(individual));
        Assert.Equal(account.User.Id, individual.AddedBy.Id);
        Assert.Equal(templateIndividual.Name, individual.Name);
        Assert.Equal(templateIndividual.Sex, individual.Sex);
        Assert.Equal(templateIndividual.Address, individual.Address);

        return (account, individual);
    }

    [Theory]
    [InlineData(PermissionType.Create,true)]
    [InlineData(PermissionType.Update,true)]
    [InlineData(PermissionType.Delete,false)]
    [InlineData(PermissionType.Read,false)]
    public async Task<Skill?> SkillAddedTheory(
        PermissionType permission=PermissionType.Update,
        bool shouldSucceed=true)
    {
        var account =await  _uow.CreateTestingAccountAsync(AccessLevel.Group, PermissionType.Update|permission);
        var (accountsuper,individual) = await IndividualAddedFact();
        var templateSkill = ValidEntitiesFactory.CreateValidSkill();
        var addSkillTask = _populationSvc
            .AddSkillAsync(
            account.Id,
            individual!.Id,
            templateSkill.Name,
            templateSkill.Field,
            templateSkill.Description);

        if (!shouldSucceed | account.User !=accountsuper.User)
        {
            await Assert.ThrowsAsync<InsufficientPermissionsException>(async () => await addSkillTask);
            return null;
        }

        var skill = await addSkillTask;
        Assert.True(await _uow.Skills.Query().ContainsAsync(await addSkillTask));
        Assert.True(await _uow.Individuals.Query().AnyAsync(i => i.Skills.Equals(skill)));
        Assert.Equal(individual.Id, skill.BelongsTo.Id);
        Assert.Equal(templateSkill.Name, skill.Name);
        Assert.Equal(templateSkill.Field, skill.Field);
        Assert.Equal(templateSkill.Description, skill.Description);

        return skill;    
    }

    [Theory]
    [InlineData(PermissionType.Create, true)]
    [InlineData(PermissionType.Update, true)]
    [InlineData(PermissionType.Delete, false)]
    [InlineData(PermissionType.Read, false)]
    public async Task<Voluntary?> VoluntaryAddedTheory(
       PermissionType permission = PermissionType.Update,
       bool shouldSucceed = true)
    {
        var account = await _uow.CreateTestingAccountAsync(AccessLevel.Group, PermissionType.Update | permission);
        var (accountsuper, individual) = await IndividualAddedFact();
        var templateVoluntary = ValidEntitiesFactory.CreateValidVoluntary();
        var addVoluntaryTask = _populationSvc
            .AddVoluntaryAsync(
            account.Id,
            individual!.Id,
            templateVoluntary.Name,
            templateVoluntary.Field);

        if (!shouldSucceed | account.User != accountsuper.User)
        {
            await Assert.ThrowsAsync<InsufficientPermissionsException>(async () => await addVoluntaryTask);
            return null;
        }

        var voluntary = await addVoluntaryTask;
        Assert.True(await _uow.Volantaries.Query().ContainsAsync(await addVoluntaryTask));
        Assert.True(await _uow.Individuals.Query().AnyAsync(i => i.Voluntary.Equals(voluntary)));
        Assert.Equal(individual.Id, voluntary.OfferedBy.Id);
        Assert.Equal(templateVoluntary.Name, voluntary.Name);
        Assert.Equal(templateVoluntary.Field, voluntary.Field);

        return voluntary;
    }

    #endregion Tests
    #region IDisposable Members

    /// <inheritdoc/>
    public void Dispose()
        => _uow.Dispose();

    #endregion IDisposable Members


}
