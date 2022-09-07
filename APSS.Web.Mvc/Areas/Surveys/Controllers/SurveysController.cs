using Microsoft.AspNetCore.Mvc;
using APSS.Domain.Services;
using APSS.Web.Dtos;
using APSS.Web.Dtos.Forms;
using APSS.Web.Mvc.Auth;

namespace APSS.Web.Mvc.Areas.Surveys.Controllers;

[Area(Areas.Surveys)]
public class SurveysController : Controller
{
    private readonly ISurveysService _surveySvc;

    public SurveysController(ISurveysService surveySvc)
    {
        _surveySvc = surveySvc;
    }

    // GET: Survey/GetSurveys
    public async Task<ActionResult> Index()
    {
        var surveys = await _surveySvc.GetAvailableSurveysAsync(User.GetAccountId());
        List<SurveyDto> surveysDto = new List<SurveyDto>();
        foreach (var survey in await surveys.AsAsyncEnumerable().ToListAsync())
        {
            surveysDto.Add(
                new SurveyDto
                {
                    Id = survey.Id,
                    Name = survey.Name,
                    ExpirationDate = survey.ExpirationDate,
                    UserName = survey.CreatedBy.Name
                });
        }
        return View("GetSurveys", surveysDto);
    }

    // GET: Survey/SurveyDetails/5
    public async Task<ActionResult> SurveyDetails(long id)
    {
        var survey = await _surveySvc.GetSurveyAsync(User.GetAccountId(), id);

        var surveydto = new SurveyDto
        {
            Id = survey.Id,
            Name = survey.Name,
            UserName = survey.CreatedBy.Name,
            ExpirationDate = survey.ExpirationDate
        };

        return View(surveydto);
    }

    // GET: Survey/Add Survey
    public ActionResult AddSurvey()
    {
        return View();
    }

    // POST: Survey/AddSurvey
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> AddSurvey([FromForm] SurveyAddForm survey)
    {
        if (!ModelState.IsValid)
            return View(survey);
        await _surveySvc.CreateSurveyAsync(User.GetAccountId(), survey.Name, survey.ExpirationDate);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ActionName("ActiveSurvey")]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> ActiveSurvey(long id, bool active)
    {
        await _surveySvc.SetSurveyActiveStatusAsync(User.GetAccountId(), id, active);

        return RedirectToAction(nameof(Index));
    }

    // GET: Survey/EditSurvey/5
    public async Task<ActionResult> EditSurvey(long id)
    {
        var survey = await _surveySvc.GetSurveyAsync(User.GetAccountId(), id);
        var surveyform = new SurveyEditForm
        {
            Id = survey.Id,
            Name = survey.Name,
            IsActive = survey.IsActive,
            ExpirationDate = survey.ExpirationDate
        };
        return View(surveyform);
    }

    // POST: Survey/EditSurvey/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> EditSurvey([FromForm] SurveyEditForm survey)
    {
        if (!ModelState.IsValid)
        {
            return View(survey);
        }

        await _surveySvc.UpdateSurveyAsync(User.GetAccountId(), survey.Id, s =>
        {
            s.Name = survey.Name;
            s.IsActive = survey.IsActive;
            s.ExpirationDate = survey.ExpirationDate;
        });
        return RedirectToAction(nameof(Index));
    }

    //GET:Survey/confirmDeleteSurvey/5
    public async Task<IActionResult> ConfirmDeleteSurvey(long id)
    {
        var survey = await _surveySvc.GetSurveyAsync(User.GetAccountId(), id);
        var surveydto = new SurveyDto
        {
            Id = survey.Id,
            Name = survey.Name,
            UserName = survey.CreatedBy.Name,
            IsActive = survey.IsActive,
            CreatedAt = survey.CreatedAt,
            ModifiedAt = survey.ModifiedAt
        };
        return View(surveydto);
    }

    // POST: Survey/SurveyDelete/5
    [HttpPost, ActionName("DeleteSurvey")]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> DeleteSurvey(long id)
    {
        await _surveySvc.RemoveSurveyAsync(User.GetAccountId(), id);
        return RedirectToAction(nameof(Index));
    }
}