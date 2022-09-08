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

namespace APSS.Web.Mvc.Areas.Controllers
{
    [Area(Areas.Animals)]
    public class GroupsController : Controller
    {
        private IEnumerable<AnimalGroupDto> animal;
        private readonly IAnimalService _service;
        private readonly IUnitOfWork _uow;

        public GroupsController(IAnimalService service, IUnitOfWork uow)
        {
            _service = service;
            _uow = uow;
            animal = new List<AnimalGroupDto>
            {
            };
        }

        public async Task<IActionResult> Index()
        {
            var animalDto = new List<AnimalGroupListDto>();
            var accountId = (long)User.GetAccountId();
            var account = await _uow.Accounts.Query().
                Include(u => u.User).
                Where(i => (int)i.Id == User.GetAccountId())
                .FirstAsync();
            // var Animals = await _uow.AnimalGroups.Query().Include(a => a.Products).AsAsyncEnumerable().ToListAsync();
            var Animals = await (await _service.GetAllAnimalGroupsAsync(
                User.GetAccountId(),
                account!.User.Id))
                .AsAsyncEnumerable()
                .ToListAsync();
            foreach (var item in Animals)
            {
                animalDto.Add(new AnimalGroupListDto
                {
                    Id = item.Id,
                    Type = item.Type,
                    Quantity = item.Quantity,
                    CreatedAt = item.CreatedAt,
                    Sex = item.Sex,
                    Name = item.Name,
                });
            }

            return View(animalDto);
        }

        [HttpPost]
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
        }

        [ApssAuthorized(AccessLevel.Farmer, PermissionType.Create)]
        public async Task<IActionResult> Add()
        {
            var animalGroup = new AnimalGroupDto();
            return View(animalGroup);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(AnimalGroupDto animal)
        {
            try
            {
                if (!ModelState.IsValid)
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
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception)
            {
                return Problem("some thing error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                if (id > 0)
                {
                    var animal = await (await _service.GetAnimalGroupAsync(User.GetAccountId(), id))
                        .Include(a => a.Name).
                        Include(t => t.Type)
                        .AsAsyncEnumerable()
                        .ToListAsync();
                    var item = animal.FirstOrDefault();
                    var animalGroupDto = new AnimalGroupListDto
                    {
                        Name = item!.Name,
                        Type = item.Type,
                        Quantity = item!.Quantity,
                        Sex = item!.Sex,
                        Confirm = (bool)item.IsConfirmed!,
                        CreatedAt = item!.CreatedAt,
                        ModifiedAt = item!.ModifiedAt,
                    };
                    return View(animalGroupDto);
                }
            }
            catch (Exception) { }
            var animals = new AnimalGroupListDto();
            return View(animals);
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (id > 0)
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
                else return RedirectToAction(nameof(Index));
            }
            catch (Exception) { }
            return View();
        }

        public async Task<IActionResult> DeleteConfirm(int id)
        {
            try
            {
                if (id > 0)
                {
                    await _service.RemoveAnimalGroupAsync(User.GetAccountId(), id);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception) { }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                if (id > 0)
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
            }
            catch (Exception) { }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AnimalGroupListDto animalGroupDto)
        {
            try
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
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return Problem("error");
            }
            TempData["Action"] = "Model is not valid";
            TempData["success"] = "Edit Password is not successed";
            return View(animalGroupDto);
        }
    }
}