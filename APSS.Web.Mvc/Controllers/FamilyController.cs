using APSS.Application.App;
using APSS.Domain.Entities;
using APSS.Web.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace APSS.Web.Mvc.Controllers
{
    public class FamilyController : Controller
    {
        private readonly UserDto _userDto;
        private readonly List<FamilyDto> families;
        private readonly List<IndividualDto> individuals;
        private readonly List<FamilyIndividualDto> familyIndividuals;

        public FamilyController()
        {
            _userDto = new UserDto
            {
                Id = 1,
                Name = "aden",
                AccessLevel = AccessLevel.Root,
                userStatus = UserStatus.Active,
            };

            families = new List<FamilyDto>
            {
              new FamilyDto{Id=54,Name="ali",LivingLocation="sana'a",CreatedAt=DateTime.Now,ModifiedAt=DateTime.Now,User=_userDto },
              new FamilyDto{Id=53,Name="salih",LivingLocation="sana'a",CreatedAt=DateTime.Now,ModifiedAt=DateTime.Now,User=_userDto },
            };

            individuals = new List<IndividualDto>
            {
                new IndividualDto{Id=54, Name="ali",Address="mareb",Family=families.First(),User=_userDto},
                new IndividualDto{Id=534, Name="salih",Address="mareb",Family=families.Last(),User=_userDto}
            };

            familyIndividuals = new List<FamilyIndividualDto>
            {
                new FamilyIndividualDto{Id=54,Individual=individuals.First(),Family=families.First(),IsParent=true,IsProvider=true},
                new FamilyIndividualDto{Id=54,Individual=individuals.Last(),Family=families.Last(),IsParent=true,IsProvider=true},
            };
        }

        // GET: FamilyController/GetFamilies
        public IActionResult GetFamilies()
        {
            return View(families);
        }

        // GET: FamilyController/FamilyDetails/5
        public IActionResult FamilyDetails(int id)
        {
            var family = families.Find(f => f.Id == id);
            return View(family);
        }

        // GET: FamilyController/GetFamilyIndividuals/5
        public IActionResult GetFamilyIndividuals(int id)
        {
            var individualoffamily = familyIndividuals.Where(f => f.Family.Id == id);
            return View(individualoffamily.ToList());
        }

        // GET: FamilyController/AddFamily
        public IActionResult AddFamily()
        {
            return View();
        }

        // POST: FamilyController/AddFamily
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddFamily(FamilyAddDto family)
        {
            return View(family);
        }

        // GET: FamilyController/EditFamily/5
        public IActionResult UpdateFamily(int id)
        {
            var family = families.Find(f => f.Id == id);
            return View("Editfamily", family);
        }

        // POST: FamilyController/EditFamily/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateFamily(int id, FamilyDto family)
        {
            return View("Editfamily", family);
        }

        // POST: FamilyController/UpdateFamilyIndividuals/5
        public IActionResult UpdateFamilyIndividual(int id)
        {
            var newfamilyindividual = familyIndividuals.Find(f => f.Id == id);
            return View(newfamilyindividual);
        }

        // POST: FamilyController/EditFamilyIndividual/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateFamilyIndividuals(int id, FamilyIndividualDto newfamilyIndividual)
        {
            return View("EditFamilyIndividual", newfamilyIndividual);
        }

        // GET: FamilyController/DeleteFamily/5
        public IActionResult ConfirmDeleteFamily(int id)
        {
            var family = families.Find(f => f.Id == id);
            return View(family!);
        }

        public IActionResult ConfirmDeleteFamilyIndividual(int id)
        {
            var individualoffamily = familyIndividuals.Find(f => f.Id == id);
            return View(individualoffamily!);
        }

        // GET: FamilyController/DeleteFamily/5
        public IActionResult DeleteFamily(int id)
        {
            return RedirectToAction(nameof(GetFamilies));
        }

        // POST: FamilyController/DeleteFamily/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteFamily(int id, FamilyDto familyDto)
        {
            return RedirectToAction(nameof(GetFamilies));
        }

        // GET: FamilyController/DeleteFamilyIndividual/5
        public IActionResult DeleteFamilyIndividual(int id)
        {
            return RedirectToAction(nameof(GetFamilyIndividuals));
        }

        // POST: FamilyController/DeleteFamilyIndividual/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteFamilyIndividual(int id, FamilyIndividualDto familyIndividualDto)
        {
            return RedirectToAction(nameof(GetFamilyIndividuals));
        }
    }
}