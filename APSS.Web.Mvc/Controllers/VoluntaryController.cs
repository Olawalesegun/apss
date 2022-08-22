using APSS.Web.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace APSS.Web.Mvc.Controllers
{
    public class VoluntaryController : Controller
    {
        // GET: VoluntaryController/GetVoluntaries/5
        public ActionResult GetVoluntaries(int id)
        {
            return View();
        }

        // GET: VoluntaryController/AddVoluntary/5
        public ActionResult AddVoluntary(int id)
        {
            return View();
        }

        // POST:VoluntaryController/AddVoluntary
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddVoluntary(VoluntaryDto voluntary)
        {
            return View(voluntary);
        }

        //GET:VoluntaryController/UpdateVoluntary/5
        public ActionResult UpdateVoluntary(int id)
        {
            return View("EditVoluntary");
        }

        // POST: VoluntaryController/UpdateVoluntary/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateVoluntary(int id, VoluntaryDto newvoluntary)
        {
            return View("EditVoluntary");
        }

        // GET: VoluntaryController/DeleteVoluntary/5
        public ActionResult DeleteVoluntary(int id)
        {
            return View();
        }

        // POST: VoluntaryController/DeleteVoluntary/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteVoluntary(int id, VoluntaryDto voluntaryDto)
        {
            return RedirectToAction(nameof(GetVoluntaries));
        }
    }
}