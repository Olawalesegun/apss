﻿using Microsoft.AspNetCore.Mvc;

namespace APSS.Web.Mvc.Controllers
{
    public class LandProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        // GET: LandProductController/Add a new landProduct
        public ActionResult Add(long landId)
        {
            return View();
        }

        // POST: LandController/Add a new landProduct
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(long landId, IFormCollection collection)
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

        public ActionResult Details(long landProductId)
        {
            return View();
        }

        // GET: LandController/Update landProduct
        public ActionResult Update(long lanProductId)
        {
            return View();
        }

        // POST: LandController/Update landProduct
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(long landProductId, IFormCollection collection)
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

        // GET: LandController/Delete  landProduct
        public ActionResult Delete(long landProductId)
        {
            return View();
        }

        // POST: LandController/Delete landProduct
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(long landProductId, IFormCollection collection)
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

        // GET: LandController/Get landProduct
        public ActionResult GetLandProduct(long landProductId)
        {
            return View();
        }

        // POST: LandController/Get landProduct
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetLandProduct(long landProductId, IFormCollection collection)
        {
            return View();
        }

        // GET: LandController/Get landProducts
        public ActionResult GetLandProducts(long landId)
        {
            return View();
        }

        // POST: LandController/Get landProducts
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetLandProducts(long landId, IFormCollection collection)
        {
            return View();
        }
    }
}