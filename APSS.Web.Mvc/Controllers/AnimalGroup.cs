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

        public IActionResult Index()
        {
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
            searchString = searchString.Trim();
            if (!string.IsNullOrEmpty(searchString))
            {
                if (searchBy == "1")
                {
                    var result = new AnimalGroupAndProductDto();
                    result.AnimalGroupDtos = animal.Where(name => name.Type.Contains(searchString)).ToList();
                    return View(result);
                }
                else if (searchBy == "2")
                {
                    var result = new AnimalGroupAndProductDto();
                    long id = Convert.ToInt64(searchString);
                    result.AnimalGroupDtos = animal.Where(name => name.Id == id).ToList();
                    return View(result);
                }
                else if (searchBy == "3")
                {
                    var result = new AnimalGroupAndProductDto();
                    long q = Convert.ToInt64(searchString);
                    result.AnimalGroupDtos = animal.Where(name => name.Quantity == q).ToList();
                    return View(result);
                }
                else if (searchBy == "4")
                {
                    var result = new AnimalGroupAndProductDto();
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
            }
            ViewBag.SearchResult = "ليس هناك نتائج عن " + searchString;
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
            ViewBag.Gender = new AnimalSexDto();
            return View(animalGroup);
        }

        [HttpPost]
        public async Task<IActionResult> AddAnimalGroup(AnimalGroupDto animal)
        {
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> AddUnit(long Id)
        {
            var animalUnit = new AnimalProductUnitDto();

            return View("Index");
        }

        [HttpPost]
        public async Task<IActionResult> AddUnit(AnimalProductUnitDto unit)
        {
            var animalUnit = new AnimalProductUnitDto();
            if (ModelState.IsValid)
            {
                BadRequest("Index");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> CreateAnimalProduct(AnimalProductDto animalProductDto)
        {
            if (ModelState.IsValid)
            {
                return View(animalProductDto);
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> AnimalDetails(int id)
        {
            //AnimalGroupDto animalGroupDto = new AnimalGroupDto();
            var animalGroupDto = animal.Where(a => a.Id == id).FirstOrDefault();
            return View(animalGroupDto);
        }

        public async Task<IActionResult> DeleteAnimalGroup(int id)
        {
            AnimalGroupDto animalGroupDto = new AnimalGroupDto();
            return View(animalGroupDto);
        }

        public async Task<IActionResult> ConfirmDeleteAnimalGroup(int id)
        {
            AnimalGroupDto animalGroupDto = new AnimalGroupDto();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> EditAnimalGroup(int id)
        {
            AnimalGroupDto animalGroupDto = new AnimalGroupDto();
            return View(animalGroupDto);
        }

        [HttpPost]
        public async Task<IActionResult> EditAnimalGroup(AnimalGroupDto animalGroupDto)
        {
            return RedirectToAction("Index");
        }
    }
}