using Microsoft.AspNetCore.Mvc;

namespace APSS.Web.Mvc.Areas.Surveys.Controllers;

[Area(Areas.Surveys)]
public class SurveyEntriesController : Controller
{
    //Get:SurveyEntry/GetSurveyEntries
    public IActionResult GetSurveyEntries()
    {
        return View();
    }

    //Get:SurveyEntry/SurveyEntryDetails/5
    public IActionResult SurveyEntryDetails(int id)
    {
        return View();
    }

    //GET:SurveyEntry/AddSurveyEntry/id
    public IActionResult AddSurveyEntry()
    {
        return View();
    }

    //POST:SurveyEntry/AddSurveyEntry/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult AddSurveyEntry(IFormCollection entry)
    {
        return View();
    }

    public IActionResult ConfirmDeleteSurveyEntry(int id)
    {
        return View();
    }

    public IActionResult DeleteSurveyEntry(int id)
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteSurveyEntry()
    {
        return View();
    }
}