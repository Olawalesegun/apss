using APSS.Web.Dtos;
using Microsoft.AspNetCore.Mvc;
using APSS.Domain.Entities;
using APSS.Domain.Services;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace APSS.Web.Mvc.Controllers
{
    public class Users : Controller
    {
        private List<UserDto> _userDtos;
        private readonly IAccountsService _accountsService;

        public Users(IAccountsService accountsService)
        {
            _userDtos = new List<UserDto>
            {
                new UserDto{Name ="user 1",Id=1,CreatedAt=DateTime.Now,AccessLevel=Domain.Entities.AccessLevel.Directorate},
                new UserDto{Name ="user 2",Id=2,CreatedAt=DateTime.Now,AccessLevel=Domain.Entities.AccessLevel.Directorate},
                new UserDto{Name ="user 3",Id=3,CreatedAt=DateTime.Now,AccessLevel=Domain.Entities.AccessLevel.Directorate},
                new UserDto{Name ="user 4",Id=4,CreatedAt=DateTime.Now,AccessLevel=Domain.Entities.AccessLevel.Directorate},
                new UserDto{Name ="user 5",Id=5,CreatedAt=DateTime.Now,AccessLevel=Domain.Entities.AccessLevel.Directorate},
                new UserDto{Name ="user 6",Id=6,CreatedAt=DateTime.Now,AccessLevel=Domain.Entities.AccessLevel.Directorate},
            };
            this._accountsService = accountsService;
        }

        public IActionResult Index()
        {
            List<UserDto> userDto = new List<UserDto>();
            userDto.Add(new UserDto { Name = "user 1", Id = 1, CreatedAt = DateTime.Now, AccessLevel = Domain.Entities.AccessLevel.Directorate });
            userDto.Add(new UserDto { Name = "user 2", Id = 2, CreatedAt = DateTime.Now, AccessLevel = Domain.Entities.AccessLevel.Directorate });
            userDto.Add(new UserDto { Name = "user 3", Id = 3, CreatedAt = DateTime.Now, AccessLevel = Domain.Entities.AccessLevel.Directorate });
            userDto.Add(new UserDto { Name = "user 4", Id = 4, CreatedAt = DateTime.Now, AccessLevel = Domain.Entities.AccessLevel.Directorate });
            userDto.Add(new UserDto { Name = "user 5", Id = 5, CreatedAt = DateTime.Now, AccessLevel = Domain.Entities.AccessLevel.Directorate });
            return View(userDto);
        }

        public async Task<IActionResult> AddUser()
        {
            var userDto = new UserDto();
            ViewBag.status = userDto.userStatus;
            return View(userDto);
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(UserDto user)
        {
            var userDto = new UserDto();
            return View(userDto);
        }

        public async Task<IActionResult> Accounts()
        {
            List<AccountDto> accountDto = new List<AccountDto>
            {
                new AccountDto{Id=1,HolderName="account 1",PhoneNumber="7777777777",NationalId="06754653"},
                new AccountDto{Id=2,HolderName="account 2",PhoneNumber="7777777777",NationalId="06754653"},
                new AccountDto{Id=3,HolderName="account 3",PhoneNumber="7777777777",NationalId="06754653"},
                new AccountDto{Id=4,HolderName="account 4",PhoneNumber="7777777777",NationalId="06754653"},
                new AccountDto{Id=5,HolderName="account 5",PhoneNumber="7777777777",NationalId="06754653"}
            };
            return View(accountDto);
        }

        public async Task<IActionResult> UserDetials(int id)
        {
            return View();
        }

        public async Task<IActionResult> UsersIndex()
        {
            return View();
        }

        public async Task<IActionResult> DeleteUser(long id)
        {
            UserDto userDto = new UserDto();
            userDto.Id = 10;
            userDto.Name = "BBB";
            userDto.AccessLevel = Domain.Entities.AccessLevel.Directorate;
            userDto.ModifiedAt = DateTime.Now;
            userDto.CreatedAt = DateTime.Now;
            return View(userDto);
        }

        public async Task<IActionResult> ConfirmDeleteUser(long id)
        {
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> EditUser(long id)
        {
            UserDto userDto = new UserDto();
            return View(userDto);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(UserDto userDto)
        {
            return View(userDto);
        }
    }
}

internal class perm
{
    public int Id { get; set; }
    public string Name { get; set; }
}