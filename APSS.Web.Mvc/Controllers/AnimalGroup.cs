using APSS.Domain.Entities;
using APSS.Domain.Repositories;
using APSS.Domain.Services;
using APSS.Web.Dtos;
using APSS.Web.Mvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace APSS.Web.Mvc.Controllers
{
    public class AnimalGroup : Controller
    {
        private IEnumerable<AnimalGroupDto> animal;
        private readonly IAnimalService _service;

        public AnimalGroup()
        {
            animal = new List<AnimalGroupDto>
            {
                new AnimalGroupDto{Id=1,Type="types 1",Quantity=100,CreatedAt=new DateTime(),Sex=Domain.Entities.AnimalSex.Female},
                new AnimalGroupDto{Id=2,Type="one 2",Quantity=100,CreatedAt=new DateTime(),Sex=Domain.Entities.AnimalSex.Female},
                new AnimalGroupDto{Id=3,Type="one2 3",Quantity=100,CreatedAt=new DateTime(),Sex=Domain.Entities.AnimalSex.Female},
                new AnimalGroupDto{Id=4,Type="tow 4",Quantity=100,CreatedAt=new DateTime(),Sex=Domain.Entities.AnimalSex.Female},
                new AnimalGroupDto{Id=4,Type="tow2 4",Quantity=100,CreatedAt=new DateTime(),Sex=Domain.Entities.AnimalSex.Female},
                new AnimalGroupDto{Id=5,Type="type 5",Quantity=100,CreatedAt=new DateTime(),Sex=Domain.Entities.AnimalSex.Female},
            };
        }

        public async Task<IActionResult> Index()
        {
            // var resultAnimal =await _service.GetAllAnimalGroupsAsync(1, 1);
            var total = new AnimalGroupAndProductDto
            {
                AnimalGroupDtos = animal.ToList(),
                AnimalProducts = new AnimalProductDto(),
            };
            return View(total);
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
                        result.AnimalGroupDtos = animal.Where(name => name.Sex == sex).ToList();
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
            ViewBag.Gender = new AnimalSex();
            return View(animalGroup);
        }

        [HttpPost]
        public async Task<IActionResult> AddAnimalGroup(AnimalGroupDto animal)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Problem("the Animal Group Is Null");
                }
                else
                {
                    var resultAdd = _service.AddAnimalGroupAsync(1, animal.Type, "", animal.Quantity, (AnimalSex)animal.Sex);
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
                        A.Sex = animalGroupDto.Sex;
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