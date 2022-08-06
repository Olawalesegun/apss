using Microsoft.AspNetCore.Mvc;

namespace APSS.Web.Mvc.Controllers
{
    public class LandProductExpenseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Add(long landProductId)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(long landProductId, IFormCollection collection)
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

        public ActionResult Details(long landProductExpenseId)
        {
            return View();
        }

        public IActionResult DeleteProductExpense()
        {
            return View();
        }

        // GET: landProductExpense/Update landProductExpense
        public ActionResult Update(long lanProductExpenseId)
        {
            return View();
        }

        // POST: landProductExpense/Update landProductExpense
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(long landProductExpenseId, IFormCollection collection)
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

        // GET: landProductExpense/Delete  landProductExpense
        public ActionResult Delete(long landProductExpenseId)
        {
            return View();
        }

        // POST: landProductExpense/Delete landProductExpense
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(long landProductExpenseId, IFormCollection collection)
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

        // GET: landProductExpense/Get landProductExpense
        public ActionResult GetLandProduct(long landProductExpenseId)
        {
            return View();
        }

        // POST: landProductExpense/Get landProductExpense
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetLandProductExpense(long landProductExpenseId, IFormCollection collection)
        {
            return View();
        }

        // GET: landProductExpense/Get landProductExpenses
        public ActionResult GetLandProductExpenses(long landProductId)
        {
            return View();
        }

        // POST: landProductExpense/Get landProductExpenses
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetLandProductExpenses(long landProductId, IFormCollection collection)
        {
            return View();
        }
    }
}