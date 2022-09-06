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

namespace APSS.Web.Mvc.Controllers
{
    public class AnimalGroupController : Controller
    {
        private IEnumerable<AnimalGroupDto> animal;
        private readonly IAnimalService _service;

        public AnimalGroupController(IAnimalService service)
        {
            _service = service;
            animal = new List<AnimalGroupDto>
            {
            };
        }

        public async Task<IActionResult> Index()
        {
            var animalDto = new List<AnimalGroupListDto>();
            var animals = await (await _service.GetAllAnimalGroupsAsync(1, User.GetAccountId())).Include(i => i.IsConfirmed!).AsAsyncEnumerable().ToListAsync();
            foreach (var item in animals)
            {
                animalDto.Add(new AnimalGroupListDto
                {
                    Type = item.Type,
                    Quantity = item.Quantity,
                    CreatedAt = item.CreatedAt,
                    Sex = item.Sex,
                    Name = item.Name,
                    Confirm = (bool)item.IsConfirmed!,
                });
            }

            return View(animalDto);
        }

        [HttpPost]
        public async Task<IActionResult> Index(string searchString = "", string searchBy = "")
        {
            try
            {
                var searchResult = _service.GetAllAnimalGroupsAsync(1, 0);
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

        public async Task<IActionResult> AddAnimalGroup()
        {
            var animalGroup = new AnimalGroupDto();
            return View(animalGroup);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddAnimalGroup(AnimalGroupDto animal)
        {
            var resultAdd = await _service.AddAnimalGroupAsync(1, "type 1", "name 1", animal.Quantity, AnimalSex.Female);

            try
            {
                if (!ModelState.IsValid)
                {
                    return Problem("the Animal Group Is Null");
                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                return Problem("some thing error");
            }
            return View(animal);
        }

        public async Task<IActionResult> AddUnit(long Id)
        {
            var animalUnit = new AnimalProductUnitDto();

            return View("Index");
        }

        [HttpGet]
        public async Task<IActionResult> AnimalDetails(int id)
        {
            //AnimalGroupDto animalGroupDto = new AnimalGroupDto();
            var animalGroupDto = animal.Where(a => a.Id == id).FirstOrDefault();
            var animalDetails = _service.GetAnimalGroupAsync(1, id);
            return View(animalGroupDto);
        }

        public async Task<IActionResult> DeleteAnimalGroup(int id)
        {
            AnimalGroupDto animalGroupDto = new AnimalGroupDto();
            var animalDelete = _service.GetAnimalGroupAsync(1, id);
            return View(animalGroupDto);
        }

        public async Task<IActionResult> ConfirmDeleteAnimalGroup(int id)
        {
            AnimalGroupDto animalGroupDto = new AnimalGroupDto();
            var animalDelete = _service.RemoveAnimalGroupAsync(1, id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> EditAnimalGroup(int id)
        {
            AnimalGroupDto animalGroupDto = new AnimalGroupDto();
            var animalEdit = _service.GetAnimalGroupAsync(1, id);
            return View(animalGroupDto);
        }

        [HttpPost]
        public async Task<IActionResult> EditAnimalGroup(AnimalGroupDto animalGroupDto)
        {
            if (!animalGroupDto.Equals(""))
            {
                try
                {
                    var resultEdit = _service.UpdateAnimalGroupAsync(1, animalGroupDto.Id, A =>
                    {
                        A.Id = animalGroupDto.Id;
                        A.Type = animalGroupDto.Type;
                        A.Quantity = animalGroupDto.Quantity;
                        A.Sex = (AnimalSex)animalGroupDto.Sex;
                    });
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    return Problem("error");
                }
            }
            return View(animalGroupDto);
        }
    }
}