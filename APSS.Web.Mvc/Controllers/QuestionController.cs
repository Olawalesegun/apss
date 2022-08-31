using Microsoft.AspNetCore.Mvc;

namespace APSS.Web.Mvc.Controllers;

public class QuestionController : Controller
{
    public IActionResult AddQuestion(long id)
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult AddQuestion()
    {
        return View();
    }

    public IActionResult GetQuestionsSurvey(long id)
    {
        return View();
    }

    public IActionResult EditQuestion()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult EditQuestion(IFormCollection form)
    {
        return View();
    }

    public IActionResult ConfirmDeleteQuestion(long id)
    {
        return View();
    }

    public IActionResult DeleteQuestion(long id)
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteQuestion(long id, IFormCollection form)
    {
        return View();
    }
}