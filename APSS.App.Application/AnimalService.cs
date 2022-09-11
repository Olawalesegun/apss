using APSS.Domain.Entities;
using APSS.Domain.Repositories;
using APSS.Domain.Repositories.Extensions;
using APSS.Domain.Services;
using APSS.Domain.Services.Exceptions;

namespace APSS.Application.App;

public class AnimalService : IAnimalService
{
    #region Fields

    private readonly IPermissionsService _permissionsService;
    private readonly IUnitOfWork _uow;

    #endregion Fields

    #region Public Constructors

    public AnimalService(IUnitOfWork uow, IPermissionsService permissionsService)
    {
        _uow = uow;
        _permissionsService = permissionsService;
    }

    #endregion Public Constructors

    #region Public Methods

    /// <inheritdoc/>
    public async Task<AnimalGroup> AddAnimalGroupAsync(
        long accountId,
        string type,
        string name,
        int quantity,
        AnimalSex animalSex)
    {
        var farmer = await _uow.Accounts.Query()
            .Include(u => u.User)
            .FindWithAccessLevelValidationAsync(accountId, AccessLevel.Farmer, PermissionType.Create);

        AnimalGroup animalGroup = new()
        {
            Type = type,
            Name = name,
            Quantity = quantity,
            Sex = animalSex,
            OwnedBy = farmer.User,
        };
        _uow.AnimalGroups.Add(animalGroup);
        await _uow.CommitAsync();
        return animalGroup;
    }

    /// <inheritdoc/>
    public async Task<AnimalProduct> AddAnimalProductAsync(
        long accountId,
        long animalGroupId,
        long animalProductUnitId,
        string name,
        double quantity,
        TimeSpan periodTaken)
    {
        var farmer = await _uow.Accounts.Query()
            .Include(u => u.User)
            .FindWithAccessLevelValidationAsync(accountId,
            AccessLevel.Farmer,
            PermissionType.Create);

        var animalGroup = await _uow.AnimalGroups.Query()
            .FindWithOwnershipValidationAync(animalGroupId, farmer);

        var unit = await _uow.AnimalProductUnits.Query().FindAsync(animalProductUnitId);

        AnimalProduct animalProduct = new()
        {
            Name = name,
            Quantity = quantity,
            PeriodTaken = periodTaken,
            Unit = unit,
            Producer = animalGroup,
            AddedBy = farmer.User,
        };

        _uow.AnimalProducts.Add(animalProduct);
        await _uow.CommitAsync();

        return animalProduct;
    }

    /// <inheritdoc/>
    public async Task<IQueryBuilder<AnimalGroup>> GetAllAnimalGroupsAsync(long accountId, long userId)
    {
        await _permissionsService.ValidatePermissionsAsync(accountId, userId, PermissionType.Read);

        return _uow.AnimalGroups.Query().Where(i => i.OwnedBy.Id == userId);
    }

    /// <inheritdoc/>
    public async Task<IQueryBuilder<AnimalProduct>> GetAnimalProductAsync(long accountId, long animalProductId)
    {
        var account = await _uow.Accounts.Query()
            .Include(u => u.User)
            .FindWithPermissionsValidationAsync(accountId, PermissionType.Read);
        return _uow.AnimalProducts.Query().Where(p => p.Id == animalProductId && p.AddedBy.Id == account.User.Id);
    }

    public async Task<IQueryBuilder<AnimalGroup>> GetAnimalGroupAsync(long accountId, long animalGroupId)
    {
        var user = await _uow.Accounts.Query().FindWithPermissionsValidationAsync(accountId, PermissionType.Read);

        return _uow.AnimalGroups.Query().Where(g => g.Id == animalGroupId);
    }

    /// <inheritdoc/>

    public async Task<IQueryBuilder<AnimalProduct>> GetAllAnimalProductsAsync(
        long accountId,
        long userId)
    {
        await _permissionsService.ValidatePermissionsAsync(accountId, userId, PermissionType.Read);

        return _uow.AnimalProducts.Query()
            .Where(p => p.Producer.OwnedBy.Id == userId);
    }

    /// <inheritdoc/>
    public async Task RemoveAnimalGroupAsync(long accountId, long animalGroupId)
    {
        var account = await _uow.Accounts.Query()
            .FindWithPermissionsValidationAsync(accountId, PermissionType.Delete);

        var animalgroup = await _uow.AnimalGroups.Query()
            .FindWithOwnershipValidationAync(animalGroupId, account);

        _uow.AnimalGroups.Remove(animalgroup);
        await _uow.CommitAsync();
    }

    /// <inheritdoc/>
    public async Task RemoveAnimalProductAsync(long accountId, long animalProductId)
    {
        var account = await _uow.Accounts.Query().Include(u => u.User)
            .FindWithPermissionsValidationAsync(accountId, PermissionType.Delete);

        var animalProduct = await _uow.AnimalProducts
             .Query()
             .Include(a => a.Producer)
             .Include(a => a.Producer.OwnedBy)
             .FindWithOwnershipValidationAync(animalProductId, a => a.Producer.OwnedBy, account);

        _uow.AnimalProducts.Remove(animalProduct);
        await _uow.CommitAsync();
    }

    /// <inheritdoc/>
    public async Task<AnimalGroup> UpdateAnimalGroupAsync(long accounId, long animalGroupId, Action<AnimalGroup> updater)
    {
        var account = await _uow.Accounts.Query()
            .FindWithAccessLevelValidationAsync(accounId, AccessLevel.Farmer, PermissionType.Update);

        var animalGroup = await _uow.AnimalGroups.Query().FindWithOwnershipValidationAync(animalGroupId, a => a.OwnedBy, account);

        updater(animalGroup);

        _uow.AnimalGroups.Update(animalGroup);
        await _uow.CommitAsync();

        return animalGroup;
    }

    /// <inheritdoc/>
    public async Task<AnimalProduct> UpdateAnimalProductAsync(long accountId, long animalProductId, Action<AnimalProduct> updater)
    {
        var account = await _uow.Accounts.Query()
            .FindWithAccessLevelValidationAsync(accountId, AccessLevel.Farmer, PermissionType.Update);

        var animalProduct = await _uow.AnimalProducts.Query()
            .Include(a => a.Producer.OwnedBy)
            .FindWithOwnershipValidationAync(animalProductId, a => a.Producer.OwnedBy, account);

        updater(animalProduct);

        _uow.AnimalProducts.Update(animalProduct);
        await _uow.CommitAsync();

        return animalProduct;
    }

    public async Task RemoveAnimalProductUnitAsync(long accountId, long productUnitId)
    {
        var account = await _uow.Accounts.Query().FindWithPermissionsValidationAsync(accountId, PermissionType.Delete);
        var unit = await _uow.AnimalProductUnits.Query().FindAsync(productUnitId);

        _uow.AnimalProductUnits.Remove(unit);
        await _uow.CommitAsync();
    }

    public async Task<AnimalProductUnit> UpdateProductUnit(long accountId, long productUnitId, Action<AnimalProductUnit> updater)
    {
        var account = await _uow.Accounts.Query().FindWithAccessLevelValidationAsync(accountId, AccessLevel.Root, PermissionType.Update);

        var unit = await _uow.AnimalProductUnits.Query().FindAsync(productUnitId);
        updater(unit);
        _uow.AnimalProductUnits.Update(unit);
        await _uow.CommitAsync();
        return unit;
    }

    public async Task<AnimalProductUnit> CreateAnimalProductUnitAsync(long accountId, string name)
    {
        var account = await _uow.Accounts.Query()
            .FindWithAccessLevelValidationAsync(accountId, AccessLevel.Root, PermissionType.Create);

        AnimalProductUnit animalProductUnit = new()
        {
            Name = name,
        };
        _uow.AnimalProductUnits.Add(animalProductUnit);
        await _uow.CommitAsync();

        return animalProductUnit;
    }

    public async Task<ProductExpense> CreateProductExpenseAsync(long accountId, long productId, string type, decimal price)
    {
        var account = await _uow.Accounts.Query()
            .Include(u => u.User)
            .FindWithAccessLevelValidationAsync(accountId, AccessLevel.Farmer, PermissionType.Create);

        var product = await _uow.AnimalProducts.Query()
            .Include(u => u.Producer.OwnedBy)
            .FindWithOwnershipValidationAync(productId, u => u.Producer.OwnedBy, account);

        var expenseproduct = new ProductExpense
        {
            Price = price,
            Type = type,
            SpentOn = product
        };

        _uow.ProductExpenses.Add(expenseproduct);
        await _uow.CommitAsync();

        return expenseproduct;
    }

    public async Task<ProductExpense> UpdateProductExpensesAsync(long accountId, long productExpenseId, Action<ProductExpense> updater)
    {
        var account = await _uow.Accounts.Query().FindWithAccessLevelValidationAsync(accountId, AccessLevel.Farmer, PermissionType.Update);
        var expense = await _uow.ProductExpenses.Query().Include(s => s.SpentOn).FindWithOwnershipValidationAync(productExpenseId, u => u.SpentOn.AddedBy, account);
        updater(expense);

        _uow.ProductExpenses.Update(expense);
        await _uow.CommitAsync();
        return expense;
    }

    public async Task<IQueryBuilder<AnimalProductUnit>> GetAnimalProductUnitAsync(long accountId)
    {
        var account = await _uow.Accounts.Query()
            .Include(u => u.User)
            .FindWithPermissionsValidationAsync(accountId, PermissionType.Read);
        return _uow.AnimalProductUnits.Query();
    }

    public async Task<IQueryBuilder<ProductExpense>> GetProductExpenses(long accountId, long userId, long productId)
    {
        await _permissionsService.ValidatePermissionsAsync(accountId, userId, PermissionType.Read);

        return _uow.ProductExpenses.Query().Where(p => p.SpentOn.Id == productId);
    }

    public async Task<IQueryBuilder<ProductExpense>> GetExpense(long accountId, long expenseId)
    {
        var account = await _uow.Accounts.Query()
            .Include(u => u.User)
            .FindWithPermissionsValidationAsync(accountId, PermissionType.Read);

        return _uow.ProductExpenses.Query().Where(p => p.Id == expenseId);
    }

    public async Task RemoveProductExpenseAsync(long accountId, long expenseId)
    {
        var account = await _uow.Accounts.Query()
            .Include(u => u.User)
            .FindWithPermissionsValidationAsync(accountId, PermissionType.Delete);

        var expense = await _uow.ProductExpenses.Query().Find(expenseId).FirstAsync();

        _uow.ProductExpenses.Remove(expense);
        await _uow.CommitAsync();
    }

    #endregion Public Methods
}