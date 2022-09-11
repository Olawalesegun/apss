using APSS.Domain.Entities;
using APSS.Domain.Repositories;
using APSS.Domain.Repositories.Extensions;
using APSS.Domain.Services;
using APSS.Domain.Services.Exceptions;

namespace APSS.Application.App;

public sealed class PopulationService : IPopulationService
{
    #region Fields

    private readonly IPermissionsService _permissionsSvc;
    private readonly IUnitOfWork _uow;
    private readonly IUsersService _usersSvc;

    #endregion Fields

    #region Public Constructors

    public PopulationService(IUnitOfWork uow, IPermissionsService permissions, IUsersService usersSvc)
    {
        _uow = uow;
        _permissionsSvc = permissions;
        _usersSvc = usersSvc;
    }

    #endregion Public Constructors

    #region Public Methods

    ///<inheritdoc/>
    public async Task<Family> AddFamilyAsync(long accountId, string name, string livingLocation)
    {
        var account = await GetAuthorizedGroupAccountAsync(accountId, PermissionType.Create);

        var family = new Family
        {
            Name = name,
            LivingLocation = livingLocation,
            AddedBy = account.User,
        };

        _uow.Families.Add(family);
        await _uow.CommitAsync();

        return family;
    }

    ///<inheritdoc/>
    public async Task<Individual> AddIndividualAsync(
        long accountId,
        long familyId,
        string name,
        string address,
        IndividualSex sex,
        bool isParent = false,
        bool isProvider = false)
    {
        var account = await GetAuthorizedGroupAccountAsync(accountId, PermissionType.Create);

        var individual = new Individual
        {
            Name = name,
            Address = address,
            Sex = sex,
            AddedBy = account.User,
        };

        var family = await _uow.Families
            .Query()
           .Include(f => f.AddedBy)
           .FindWithOwnershipValidationAync(familyId, f => f.AddedBy, account);

        var familyIndividual = new FamilyIndividual
        {
            Individual = individual,
            Family = family,
            IsParent = isParent,
            IsProvider = isProvider,
        };

        _uow.Individuals.Add(individual);
        _uow.FamilyIndividuals.Add(familyIndividual);
        await _uow.CommitAsync();

        return individual;
    }

    ///<inheritdoc/>
    public async Task<Skill> AddSkillAsync(
        long accountId,
        long individualId,
        string name,
        string field,
        string? description = null)
    {
        var account = await GetAuthorizedGroupAccountAsync(
            accountId,
            PermissionType.Create | PermissionType.Update);

        var individual = await _uow.Individuals.Query()
            .Include(i => i.AddedBy)
            .FindWithOwnershipValidationAync(individualId, i => i.AddedBy, account);

        var skill = new Skill
        {
            Name = name,
            Field = field,
            Description = description,
            BelongsTo = individual
        };

        _uow.Skills.Add(skill);
        await _uow.CommitAsync();

        return skill;
    }

    ///<inheritdoc/>
    public async Task<Voluntary> AddVoluntaryAsync(long accountId, long individualId, string name, string field)
    {
        var account = await GetAuthorizedGroupAccountAsync(accountId, PermissionType.Create | PermissionType.Update);

        var individual = await _uow.Individuals.Query()
            .Include(i => i.AddedBy)
            .FindWithOwnershipValidationAync(individualId, i => i.AddedBy, account);

        var voluntary = new Voluntary
        {
            Name = name,
            Field = field,
            OfferedBy = individual
        };

        _uow.Volantaries.Add(voluntary);
        await _uow.CommitAsync();

        return voluntary;
    }

    ///<inheritdoc/>
    public async Task RemoveFamilyAsync(long accountId, long familyId)
    {
        var account = await GetAuthorizedGroupAccountAsync(accountId, PermissionType.Delete);

        var family = await _uow.Families.Query()
            .Include(f => f.AddedBy)
            .FindWithOwnershipValidationAync(familyId, f => f.AddedBy, account);
        _uow.Families.Remove(family);
        await _uow.CommitAsync();
    }

    ///<inheritdoc/>
    public async Task RemoveIndividualAsync(long accountId, long individualId)
    {
        await using var tx = await _uow.BeginTransactionAsync();

        var account = await GetAuthorizedGroupAccountAsync(accountId, PermissionType.Delete);

        var individual = await _uow.Individuals.Query()
            .Include(i => i.AddedBy)
            .FindWithOwnershipValidationAync(individualId, i => i.AddedBy, account);

        var relationships = await _uow.FamilyIndividuals.Query()
            .Where(f => f.Individual.Id == individualId)
            .AsAsyncEnumerable()
            .ToListAsync();

        relationships.ForEach(_uow.FamilyIndividuals.Remove);
        _uow.Individuals.Remove(individual);

        await _uow.CommitAsync(tx);
    }

    ///<inheritdoc/>
    public async Task RemoveSkillAsync(long accountId, long skillId)
    {
        var account = await GetAuthorizedGroupAccountAsync(
            accountId,
            PermissionType.Delete | PermissionType.Update);

        var skill = await _uow.Skills.Query()
            .Include(s => s.BelongsTo.AddedBy)
            .FindWithOwnershipValidationAync(skillId, s => s.BelongsTo.AddedBy, account);

        _uow.Skills.Remove(skill);
        await _uow.CommitAsync();
    }

    ///<inheritdoc/>
    public async Task RemoveVoluntaryAsync(long accountId, long voluntaryId)
    {
        var account = await GetAuthorizedGroupAccountAsync(accountId, PermissionType.Delete);

        var voluntary = await _uow.Volantaries.Query()
            .Include(v => v.OfferedBy.AddedBy)
            .FindWithOwnershipValidationAync(voluntaryId, v => v.OfferedBy.AddedBy, account);

        _uow.Volantaries.Remove(voluntary);
        await _uow.CommitAsync();
    }

    ///<inheritdoc/>
    public async Task<IQueryBuilder<Family>> GetFamilies(long accountId)
    {
        var account = await _uow.Accounts.Query().Include(a => a.User).FindAsync(accountId);
        var family = _uow.Families
            .Query()
            .Include(f => f.AddedBy)
            .Where(f => f.AddedBy.Id == account.User.Id);

        return family;
    }

    ///<inheritdoc/>
    public async Task<IQueryBuilder<Individual>> GetIndividuals(long accountId)
    {
        var account = await _uow.Accounts.Query().Include(a => a.User).FindAsync(accountId);
        var individuals = _uow.Individuals
            .Query()
            .Include(i => i.AddedBy)
            .Where(i => i.AddedBy.Id == account.User.Id);

        return individuals;
    }

    ///<inheritdoc/>
    public async Task<Family> GetFamilyAsync(long accountId, long familyId)
    {
        var account = await GetAuthorizedGroupAccountAsync(accountId, PermissionType.Update);

        var family = await _uow.Families.Query()
            .Include(f => f.AddedBy)
            .FindWithOwnershipValidationAync(familyId, f => f.AddedBy, account);
        return family;
    }

    ///<inheritdoc/>
    public async Task<Individual> GetIndividualAsync(long accountId, long IndividualId)
    {
        var account = await GetAuthorizedGroupAccountAsync(accountId, PermissionType.Update);

        var individual = await _uow.Individuals.Query()
            .Include(i => i.AddedBy)
            .FindWithOwnershipValidationAync(IndividualId, f => f.AddedBy, account);
        return individual;
    }

    ///<inheritdoc/>
    public async Task<IQueryBuilder<FamilyIndividual>> GetIndividualsOfFamilyAsync(long accountId, long familyId)
    {
        var family = await _uow.Families.Query()
            .Include(f => f.AddedBy)
            .FindAsync(familyId);

        await _permissionsSvc
            .ValidatePermissionsAsync(accountId, family.AddedBy.Id, PermissionType.Read);

        var account = await _uow.Accounts.Query().FindAsync(accountId);

        if (account.User.AccessLevel == AccessLevel.Farmer)
            throw new InsufficientPermissionsException(
                accountId, $"farmer #{account.User.Id} with account #{accountId} cannot read indviduals of Family{family.Name} ");

        return _uow.FamilyIndividuals.Query().Include(f => f.Family).Include(f => f.Individual).Where(f => f.Family.Id == familyId);
    }

    public async Task<FamilyIndividual?> GetFamilyIndividual(long accountId, long individualId)
    {
        var indvidual = await _uow.Individuals.Query()
           .Include(f => f.AddedBy)
           .FindAsync(individualId);

        await _permissionsSvc
            .ValidatePermissionsAsync(accountId, indvidual.AddedBy.Id, PermissionType.Read);

        var account = await _uow.Accounts.Query().FindAsync(accountId);

        if (account.User.AccessLevel == AccessLevel.Farmer)
            throw new InsufficientPermissionsException(
                accountId,
                $"farmer #{account.User.Id} with account #{accountId} cannot read date individual{indvidual.Name}");

        return await _uow.FamilyIndividuals.Query().Include(i => i.Family).Where(f => f.Individual.Id == individualId).FirstOrNullAsync();
    }

    public async Task<Skill> GetSkillAsync(long accountId, long SkillId)
    {
        var skill = await _uow.Skills.Query()
            .Include(s => s.BelongsTo)
           .Include(s => s.BelongsTo.AddedBy)
           .FindAsync(SkillId);

        await _permissionsSvc
            .ValidatePermissionsAsync(accountId, skill.BelongsTo.AddedBy.Id, PermissionType.Read);

        var account = await _uow.Accounts.Query().FindAsync(accountId);

        if (account.User.AccessLevel == AccessLevel.Farmer)
            throw new InsufficientPermissionsException(
                accountId,
                $"farmer #{account.User.Id} with account #{accountId} cannot read skill individual{skill.BelongsTo.Name}");

        return skill;
    }

    public async Task<Voluntary> GetVoluntaryAsync(long accountId, long VoluntaryId)
    {
        var voluntary = await _uow.Volantaries.Query()
           .Include(v => v.OfferedBy.AddedBy)
           .FindAsync(VoluntaryId);

        await _permissionsSvc
            .ValidatePermissionsAsync(accountId, voluntary.OfferedBy.AddedBy.Id, PermissionType.Read);

        var account = await _uow.Accounts.Query().FindAsync(accountId);

        if (account.User.AccessLevel == AccessLevel.Farmer)
            throw new InsufficientPermissionsException(
                accountId,
                $"farmer #{account.User.Id} with account #{accountId} cannot read voluntary individual{voluntary.OfferedBy.Name}");

        return voluntary;
    }

    ///<inheritdoc/>
    public async Task<IQueryBuilder<Skill>> GetSkillOfindividualAsync(long accountId, long individualId)
    {
        var individual = await _uow.Individuals.Query()
            .Include(i => i.AddedBy)
            .FindAsync(individualId);

        await _permissionsSvc
            .ValidatePermissionsAsync(accountId, individual.AddedBy.Id, PermissionType.Read);

        var account = await _uow.Accounts.Query().FindAsync(accountId);

        if (account.User.AccessLevel == AccessLevel.Farmer)
            throw new InsufficientPermissionsException(
                accountId, $"farmer #{account.User.Id} with account #{accountId} cannot add surveys");

        return _uow.Skills.Query().Include(s => s.BelongsTo).Where(s => s.BelongsTo.Id == individualId);
    }

    ///<inheritdoc/>
    public async Task<IQueryBuilder<Voluntary>> GetVoluntaryOfindividualAsync(long accountId, long individualId)
    {
        var individual = await _uow.Individuals.Query()
           .Include(i => i.AddedBy)
           .FindAsync(individualId);

        await _permissionsSvc
            .ValidatePermissionsAsync(accountId, individual.AddedBy.Id, PermissionType.Read);

        var account = await _uow.Accounts.Query().FindAsync(accountId);

        if (account.User.AccessLevel == AccessLevel.Farmer)
            throw new InsufficientPermissionsException(
                accountId, $"farmer #{account.User.Id} with account #{accountId} have not read permission on individual{individualId}");

        return _uow.Volantaries.Query().Include(v => v.OfferedBy).Where(v => v.OfferedBy.Id == individualId);
    }

    ///<inheritdoc/>
    public async Task<Family> UpdateFamilyAsync(long accountId, long familyId, Action<Family> updater)
    {
        var account = await GetAuthorizedGroupAccountAsync(accountId, PermissionType.Update);

        var family = await _uow.Families.Query()
            .Include(f => f.AddedBy)
            .FindWithOwnershipValidationAync(familyId, f => f.AddedBy, account);

        updater(family);

        _uow.Families.Update(family);
        await _uow.CommitAsync();

        return family;
    }

    ///<inheritdoc/>
    public async Task<Individual> UpdateIndividualAsync(long accountId, long individualId, Action<Individual> updater)
    {
        var account = await GetAuthorizedGroupAccountAsync(accountId, PermissionType.Update);

        var individual = await _uow.Individuals.Query()
            .Include(i => i.AddedBy)
            .FindWithOwnershipValidationAync(individualId, i => i.AddedBy, account);

        updater(individual);

        _uow.Individuals.Update(individual);
        await _uow.CommitAsync();

        return individual;
    }

    public async Task<FamilyIndividual> UpdateFamilyIndividualAsync(long accountId, long individualId, Action<FamilyIndividual> updater)
    {
        var account = await GetAuthorizedGroupAccountAsync(accountId, PermissionType.Update);

        await _uow.Individuals.Query()
           .Include(i => i.AddedBy)
           .FindWithOwnershipValidationAync(individualId, i => i.AddedBy, account);

        var familyindividual = await _uow.FamilyIndividuals.Query()
            .Include(i => i.Individual)
            .Include(i => i.Family)
            .Where(i => i.Individual.Id == individualId).FirstAsync();
        updater(familyindividual);

        _uow.FamilyIndividuals.Update(familyindividual);
        await _uow.CommitAsync();

        return familyindividual;
    }

    public async Task<FamilyIndividual> UpdateSkillAsync(
            long accountId,
            long familyIndividualId,
            Action<FamilyIndividual> updater)
    {
        var account = await GetAuthorizedGroupAccountAsync(accountId, PermissionType.Update);

        var familyindividuals = await _uow.FamilyIndividuals.Query()
            .Include(f => f.Family)
            .Include(f => f.Individual)
            .FindAsync(familyIndividualId);

        await _uow.Families.Query()
            .FindWithOwnershipValidationAync(familyindividuals.Family.Id, f => f.AddedBy, account);

        updater(familyindividuals);

        _uow.FamilyIndividuals.Update(familyindividuals);
        await _uow.CommitAsync();

        return familyindividuals;
    }

    ///<inheritdoc/>
    public async Task<Skill> UpdateSkillAsync(long accountId, long skillId, Action<Skill> updater)
    {
        var account = await GetAuthorizedGroupAccountAsync(accountId, PermissionType.Update);

        var skill = await _uow.Skills.Query()
            .Include(s => s.BelongsTo.AddedBy)
            .FindWithOwnershipValidationAync(skillId, s => s.BelongsTo.AddedBy, account);

        updater(skill);

        _uow.Skills.Update(skill);
        await _uow.CommitAsync();

        return skill;
    }

    ///<inheritdoc/>
    public async Task<Voluntary> UpdateVoluntaryAsync(long accountId, long voluntaryId, Action<Voluntary> updater)
    {
        var account = await GetAuthorizedGroupAccountAsync(accountId, PermissionType.Update);

        var voluntary = await _uow.Volantaries.Query()
            .Include(v => v.OfferedBy.AddedBy)
            .FindWithOwnershipValidationAync(voluntaryId, v => v.OfferedBy.AddedBy, account);

        updater(voluntary);

        _uow.Volantaries.Update(voluntary);
        await _uow.CommitAsync();

        return voluntary;
    }

    #endregion Public Methods

    #region Private Methods

    private Task<Account> GetAuthorizedGroupAccountAsync(long accountId, PermissionType permissions)
        => _uow.Accounts.Query()
                .FindWithAccessLevelValidationAsync(accountId, AccessLevel.Group, permissions);

    #endregion Private Methods
}