﻿using Microsoft.AspNetCore.Mvc;

namespace APSS.Web.Mvc.Controllers
{
    public class LandController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        // GET: LandController/Add a new land
        public ActionResult Add()
        {
            return View();
        }

        // POST: LandController/Add a new land
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Details(long landId)
        {
            return View();
        }

        // GET: LandController/Update land
        public ActionResult Update(long landId)
        {
            return View();
        }

        // POST: LandController/Update land
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(long landId, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LandController/Delete  land
        public ActionResult Delete(long landId)
        {
            return View();
        }

        // POST: LandController/Delete land
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(long landId, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LandController/Get land
        public ActionResult GetLand(long landId)
        {
            return View();
        }

        // POST: LandController/Get land
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetLand(long landId, IFormCollection collection)
        {
            return View();
        }

        // GET: LandController/Get lands
        public ActionResult GetLands(long userId)
        {
            return View();
        }

        // POST: LandController/Get lands
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetLands(long userId, IFormCollection collection)
        {
            return View();
        }
    }
}