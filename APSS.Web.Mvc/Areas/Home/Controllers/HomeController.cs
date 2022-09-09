using APSS.Domain.Repositories;
using APSS.Domain.Services;
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
            var accountnum = await _uow.Accounts.Query().CountAsync();
            ViewBag.Account = accountnum;
            ViewBag.User = await _uow.Users.Query().CountAsync();
            ViewBag.animals = await _uow.AnimalGroups.Query().CountAsync();
            ViewBag.farmer = await _uow.Users.Query().CountAsync(A => A.AccessLevel == Domain.Entities.AccessLevel.Farmer);

            return View();
        }
    }
}