using APSS.Web.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace APSS.Web.Mvc.Controllers
{
    public class SkillController : Controller
    {
        // GET: SkillController/GetSkills/5
        public ActionResult GetSkills(int id)
        {
            return View();
        }

        // GET: SkillController/AddSkill/5
        public ActionResult AddSkill(int id)
        {
            return View();
        }

        // POST: skillController/AddSkill
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddSkill(SkillDto skill)
        {
            return View(skill);
        }

        //GET:SkillController/UpdateSkill/5
        public ActionResult UpdateSkill(int id)
        {
            return View("EditSkill");
        }

        // POST: SkillController/UpdateSkill/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateSkill(int id, SkillDto newskill)
        {
            return View("EditSkill");
        }

        // GET: SkillController/DeleteSkill/5
        public ActionResult DeleteSkill(int id)
        {
            return View(nameof(GetSkills));
        }

        // POST: SkillController/DeleteSkill/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteSkill(int id, SkillDto skilldto)
        {
            return View(nameof(GetSkills));
        }
    }
}