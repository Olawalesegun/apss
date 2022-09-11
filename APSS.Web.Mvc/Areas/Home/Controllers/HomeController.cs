using APSS.Domain.Repositories;
using APSS.Domain.Entities;
using APSS.Domain.Services;
using APSS.Web.Mvc.Auth;
using Microsoft.AspNetCore.Mvc;

namespace APSS.Web.Mvc.Areas.Home.Controllers
{
    [Area(Areas.Home)]
    public class HomeController : Controller
    {
        private readonly IAccountsService _account;
        private readonly IUsersService _user;
        private readonly IAnimalService _animal;
        private readonly IUnitOfWork _uow;

        public HomeController(IAccountsService accounts, IUnitOfWork uow, IUsersService user, IAnimalService animal)
        {
            _account = accounts;
            _user = user;
            _animal = animal;
            _uow = uow;
        }

        public async Task<IActionResult> Index()
        {
            if (User.Claims.Count() != 0)
                switch (User.GetAccessLevel())
                {
                    case AccessLevel.Root:
                    case AccessLevel.Presedint:
                        var allUsersCount = await _uow.Users.Query().CountAsync();
                        var allActiveUsersCount = await _uow.Users.Query().Where(u => u.UserStatus == UserStatus.Active).CountAsync();
                        var allInactiveUsersCount = await _uow.Users.Query().Where(u => u.UserStatus == UserStatus.Inactive).CountAsync();
                        var allAccountsCount = await _uow.Accounts.Query().CountAsync();
                        var allActiveAccountsCount = await _uow.Accounts.Query().Where(a => a.IsActive == true).CountAsync();
                        var allInactiveAccountsCount = await _uow.Accounts.Query().Where(a => a.IsActive == false).CountAsync();
                        var allLandsCount = await _uow.Lands.Query().CountAsync();
                        var allUsableLandsCount = await _uow.Lands.Query().Where(l => l.IsUsable == true).CountAsync();
                        var allUnusableLandsCount = await _uow.Lands.Query().Where(l => l.IsUsable == false).CountAsync();
                        var allUsedLandsCount = await _uow.Lands.Query().Where(l => l.IsUsed == true).CountAsync();
                        var allUnusedLandsCount = await _uow.Lands.Query().Where(l => l.IsUsed == false).CountAsync();


                        var allFamiliesCount = await _uow.Families.Query().CountAsync();
                        var allIndividualsCount = await _uow.Individuals.Query().CountAsync();
                        var allVolunteersCount = await _uow.Individuals.Query().Include(i => i.Voluntary).CountAsync();

                        var allGovernoratesCount = await _uow.Users.Query().Where(u => u.AccessLevel == AccessLevel.Governorate).CountAsync();
                        var allDirectoratesCount = await _uow.Users.Query().Where(u => u.AccessLevel == AccessLevel.Directorate).CountAsync();
                        var allDistrictsCount = await _uow.Users.Query().Where(u => u.AccessLevel == AccessLevel.District).CountAsync();
                        var allVillagesCount = await _uow.Users.Query().Where(u => u.AccessLevel == AccessLevel.Village).CountAsync();
                        var allGroupsCount = await _uow.Users.Query().Where(u => u.AccessLevel == AccessLevel.Group).CountAsync();
                        var allFarmersCount = await _uow.Users.Query().Where(u => u.AccessLevel == AccessLevel.Farmer).CountAsync();

                        ViewBag.Users = allUsersCount;
                        ViewBag.ActiveUsers = allActiveUsersCount;
                        ViewBag.InactiveUsers = allInactiveUsersCount;
                        ViewBag.Accounts = allAccountsCount;
                        ViewBag.ActiveAccounts = allActiveAccountsCount;
                        ViewBag.InactiveAccounts = allInactiveAccountsCount;

                        ViewBag.Lands = allLandsCount;
                        ViewBag.UsableLands = allUsableLandsCount;
                        ViewBag.UnusableLands = allUnusableLandsCount;
                        ViewBag.UsedLands = allUsedLandsCount;
                        ViewBag.UnusedLands = allUnusedLandsCount;

                        ViewBag.Families = allFamiliesCount;
                        ViewBag.Individuals = allIndividualsCount;
                        ViewBag.Volunteers = allVolunteersCount;

                        ViewBag.Governorates = allGovernoratesCount;
                        ViewBag.Directorates = allDirectoratesCount;
                        ViewBag.Districts = allDistrictsCount;
                        ViewBag.Villages = allVillagesCount;
                        ViewBag.Groups = allGroupsCount;
                        ViewBag.Farmers = allFarmersCount;

                        return View("Root");

                    case AccessLevel.Farmer:
                        var farmerLands = await _uow.Lands.Query().Where(i => i.OwnedBy.Id == User.GetUserId()).AsAsyncEnumerable().ToListAsync();
                        var farmerAccounts = await _uow.Accounts.Query().Where(i => i.User.Id == User.GetUserId()).AsAsyncEnumerable().ToListAsync();
                        var farmerAnimalsCount = await _uow.AnimalGroups.Query().Where(i => i.OwnedBy.Id == User.GetUserId()).CountAsync();

                        var farmerLandsProducts = await _uow.LandProducts.Query().Where(l => l.AddedBy.Id == User.GetUserId()).AsAsyncEnumerable().ToListAsync();
                        decimal landsProductsExpenses = 0;
                        decimal animalsProductsExpenses = 0;
                        foreach (var product in farmerLandsProducts)
                        {
                            product.Expenses.Select(e => landsProductsExpenses += e.Price);
                        }

                        var farmerAnimalsProducts = await _uow.AnimalProducts.Query().Where(l => l.AddedBy.Id == User.GetUserId()).AsAsyncEnumerable().ToListAsync();

                        foreach (var product in farmerAnimalsProducts)
                        {
                            product.Expenses.Select(e => animalsProductsExpenses += e.Price);
                        }

                        ViewBag.LandsProductsExpenses = landsProductsExpenses;
                        ViewBag.AnimalsProductsExpenses = animalsProductsExpenses;
                        ViewBag.FarmerLands = farmerLands;
                        ViewBag.FarmerAccounts = farmerAccounts;
                        ViewBag.Animals = farmerAnimalsCount;
                        return View("Farmer");
                }
            return View();
        }
    }
}