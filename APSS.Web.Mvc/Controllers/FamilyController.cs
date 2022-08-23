using APSS.Web.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace APSS.Web.Mvc.Controllers
{
    public class FamilyController : Controller
    {
        // GET: FamilyController/GetFamilies
        public ActionResult GetFamilies()
        {
            var families = new List<FamilyDto>
            {
              new FamilyDto{Id=54,Name="ali",LivingLocation="sana'a",CreatedAt=DateTime.Now,ModifiedAt=DateTime.Now },
              new FamilyDto{Id=53,Name="salih",LivingLocation="sana'a",CreatedAt=DateTime.Now,ModifiedAt=DateTime.Now },
            };
            return View(families);
        }

        // GET: FamilyController/FamilyDetails/5
        public ActionResult FamilyDetails(int id)
        {
            return View();
        }

        // GET: FamilyController/GetFamilyIndividuals/5
        public ActionResult GetFamilyIndividuals(int id)
        {
            return View();
        }

        // GET: FamilyController/AddFamily
        public ActionResult AddFamily()
        {
            return View();
        }

        // POST: FamilyController/AddFamily
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddFamily(FamilyAddDto family)
        {
            return View(family);
        }

        // GET: FamilyController/EditFamily/5
        public ActionResult UpdateFamily(int id)
        {
            return View("Editfamily");
        }

        // POST: FamilyController/EditFamily/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateFamily(int id, FamilyDto family)
        {
            return View("Editfamily");
        }

        // POST: FamilyController/UpdateFamilyIndividuals/5
        public ActionResult UpdateFamilyIndividuals(int id)
        {
            return View("EditeFamilyIndividual");
        }

        // POST: FamilyController/EditFamilyIndividual/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateFamilyIndividuals(int id, FamilyIndividualDto newfamilyIndividual)
        {
            return View("EditFamilyIndividual");
        }

        // GET: FamilyController/DeleteFamily/5
        public ActionResult DeleteFamily(int id)
        {
            return RedirectToAction(nameof(GetFamilies));
        }

        // POST: FamilyController/DeleteFamily/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteFamily(int id, FamilyDto familyDto)
        {
            return View(nameof(GetFamilies));
        }

        // GET: FamilyController/DeleteFamilyIndividual/5
        public ActionResult DeleteFamilyIndividual(int id)
        {
            return View();
        }

        // POST: FamilyController/DeleteFamilyIndividual/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteFamilyIndividual(int id, FamilyIndividualDto familyIndividualDto)
        {
            return RedirectToAction(nameof(GetFamilyIndividuals));
        }
    }
}