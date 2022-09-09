using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using APSS.Domain.Entities;
using APSS.Domain.Repositories;
using APSS.Domain.Repositories.Extensions.Exceptions;
using APSS.Domain.Services;
using APSS.Domain.Services.Exceptions;
using APSS.Web.Dtos;
using APSS.Web.Mvc.Filters;
using APSS.Web.Mvc.Models;
using APSS.Web.Mvc.Auth;
using APSS.Web.Dtos.ValueTypes;
using APSS.Web.Mvc.Util.Navigation.Routes;
using AutoMapper;

namespace APSS.Web.Mvc.Areas.Controllers
{
    [Area(Areas.Animals)]
    public class GroupsController : Controller
    {
        private readonly IAnimalService _service;
        private readonly IMapper _mapper;
        private readonly IAccountsService _accounSev;

        public GroupsController(IAnimalService service, IMapper mapper, IAccountsService accountsService)
        {
            _service = service;
            _mapper = mapper;
            _accounSev = accountsService;
        }

        public async Task<IActionResult> Index()
        {
            var animalDto = new List<AnimalGroupListDto>();
            var accountId = User.GetAccountId();
            var animals = await (await _service.GetAllAnimalGroupsAsync(
                User.GetAccountId(),
                User.GetUserId()))
                .AsAsyncEnumerable()
                .ToListAsync();
            return View(animals.Select(_mapper.Map<AnimalGroupDto>));
        }

        /* [HttpPost]
         public async Task<IActionResult> Index(string searchString = "", string searchBy = "")
         {
             try
             {
                 var accountId = (long)User.GetAccountId();
                 var account = await _uow.Accounts.Query().Include(u => u.User)
                     .Where(i => i.Id == accountId)
                     .FirstOrNullAsync();
                 var searchResult = _service.GetAllAnimalGroupsAsync(account!.Id, account.User.Id);
                 if (!string.IsNullOrEmpty(searchString))
                 {
                     var result = new AnimalGroupAndProductDto();

                     searchString = searchString.Trim();

                     if (searchBy == "2")
                     {
                         long id = Convert.ToInt64(searchString);
                         result.AnimalGroupDtos = animal.Where(name => name.Id == id).ToList();
                         return View(result);
                     }
                     else if (searchBy == "3")
                     {
                         long q = Convert.ToInt64(searchString);
                         result.AnimalGroupDtos = animal.Where(name => name.Quantity == q).ToList();
                         return View(result);
                     }
                     else if (searchBy == "4")
                     {
                         searchString = searchString.Trim().ToLower();
                         if (searchString.Equals("male"))
                         {
                             searchString = "Male";
                         }
                         else if (searchString.Equals("female"))
                         {
                             searchString = "Female";
                         }
                         else
                         {
                             var animalResult = new AnimalGroupAndProductDto();
                             ViewBag.SearchResult = "ليس هناك نتائج عن " + searchString;
                             return View(animalResult);
                         }
                         AnimalSex sex = (AnimalSex)Enum.Parse(typeof(AnimalSex), searchString);
                         // result.AnimalGroupDtos = animal.Where(name => name.Sex == sex).ToList();
                         return View(result);
                     }
                     else
                     {
                         result.AnimalGroupDtos = animal.Where(name => name.Type.Contains(searchString)).ToList();
                         return View(result);
                     }
                 }
             }
             catch (Exception ex)
             {
                 throw new Exception(message: "their are some thing error", ex);
             }
             ViewBag.SearchResult = searchString;
             var total = new AnimalGroupAndProductDto
             {
                 AnimalGroupDtos = animal.ToList(),
                 AnimalProducts = new AnimalProductDto(),
             };

             return View(total);
         }*/

        [HttpGet]
        public IActionResult Add()
        {
            var animalGroup = new AnimalGroupDto();
            return View(animalGroup);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(AnimalGroupDto animal)
        {
            if (ModelState.IsValid)
            {
                return View(animal);
            }
            else
            {
                var resultAdd = await _service.AddAnimalGroupAsync(
                    User.GetAccountId(),
                    animal.Type,
                    animal.Name!,
                    animal.Quantity,
                    (AnimalSex)animal.Sex);
                if (resultAdd == null) return View(animal);
                return LocalRedirect(Routes.Dashboard.Animals.Groups.FullPath);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var animal = await (await _service.GetAnimalGroupAsync(User.GetAccountId(), id))
                .AsAsyncEnumerable()
                .FirstAsync();

            return View(_mapper.Map<AnimalGroupDto>(await (await _service.GetAnimalGroupAsync(User.GetAccountId(), id))
                .AsAsyncEnumerable()
                .FirstAsync()));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var animal = await (await _service.GetAnimalGroupAsync(User.GetAccountId(), id))
                .AsAsyncEnumerable()
                .ToListAsync();
            var item = animal.FirstOrDefault();
            var animalGroupDto = new AnimalGroupListDto
            {
                Id = item!.Id,
                Name = item!.Name,
                Type = item.Type,
                Quantity = item!.Quantity,
                Sex = item!.Sex,
                CreatedAt = item!.CreatedAt,
                ModifiedAt = item!.ModifiedAt,
            };
            return View(animalGroupDto);
        }

        public async Task<IActionResult> DeleteConfirm(int id)
        {
            await _service.RemoveAnimalGroupAsync(User.GetAccountId(), id);
            return LocalRedirect(Routes.Dashboard.Animals.Groups.FullPath);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var animal = await (await _service.GetAnimalGroupAsync(User.GetAccountId(), id))
                .AsAsyncEnumerable()
                .ToListAsync();
            var delete = animal.FirstOrDefault();
            var animalGroupDto = new AnimalGroupListDto
            {
                Name = delete!.Name,
                Type = delete.Type,
                Quantity = delete.Quantity,
                Sex = delete.Sex,
                CreatedAt = delete.CreatedAt,
                ModifiedAt = delete.ModifiedAt,
            };
            return View(animalGroupDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AnimalGroupListDto animalGroupDto)
        {
            var resultEdit = await _service.UpdateAnimalGroupAsync(User.GetAccountId(), animalGroupDto.Id, A =>
         {
             A.Type = animalGroupDto.Type;
             A.Quantity = animalGroupDto.Quantity;
             A.Sex = (AnimalSex)animalGroupDto.Sex;
             A.Name = animalGroupDto.Name;
             A.IsConfirmed = null;
         });
            if (resultEdit == null) return View(animalGroupDto);
            return LocalRedirect(Routes.Dashboard.Animals.Groups.FullPath);
        }
    }
}