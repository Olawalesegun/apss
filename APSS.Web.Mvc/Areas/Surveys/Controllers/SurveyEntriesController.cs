using APSS.Domain.Entities;
using APSS.Domain.Services;
using APSS.Web.Dtos;
using APSS.Web.Mvc.Auth;
using Microsoft.AspNetCore.Mvc;

namespace APSS.Web.Mvc.Areas.Surveys.Controllers;

[Area(Areas.Surveys)]
public class SurveyEntriesController : Controller
{
    private readonly ISurveysService _surveysService;

    public SurveyEntriesController(ISurveysService surveysService)
    {
        _surveysService = surveysService;
    }

    //Get:SurveyEntry/GetSurveyEntries
    public async Task<IActionResult> Index()
    {
        var entries = await _surveysService.GetSurveyEntriesAsync(User.GetAccountId());
        List<SurveyEntryDto> entriesdto = new List<SurveyEntryDto>();
        foreach (var entry in await entries
            .Include(e => e.Survey)
            .Include(e => e.MadeBy)
            .Include(e => e.Answers)
            .Include(e => e.Survey.Questions)
            .AsAsyncEnumerable()
            .ToListAsync())
        {
            entriesdto.Add(new SurveyEntryDto
            {
                Id = entry.Id,
                Survey = entry.Survey,
                MadeBy = entry.MadeBy,
                CreatedAt = entry.CreatedAt
            });
        }
        return View(entriesdto);
    }

    //Get:SurveyEntry/SurveyEntryDetails/5
    public IActionResult SurveyEntryDetails(int id)
    {
        return View();
    }

    //GET:SurveyEntry/AddSurveyEntry/id
    public async Task<IActionResult> AddSurveyEntry()
    {
        var entries = await _surveysService.GetSurveyEntriesAsync(User.GetAccountId());
        var entriesdto = new List<SurveyEntryDto>();

        foreach (var entry in await entries.AsAsyncEnumerable().ToListAsync())
        {
            ICollection<MultipleChoiceAnswerItem> items = null!;
            foreach (var answer in entry.Answers)
            {
                if (answer is MultipleChoiceQuestionAnswer)
                {
                    var itemsanswer = await _surveysService
                        .GetItemsAnswer(User.GetAccountId(), answer.Question.Id);
                    items = itemsanswer;
                }
            }

            entriesdto.Add(new SurveyEntryDto
            {
                Id = entry.Id,
                Survey = entry.Survey,
                anwserItems = items
            });
        }
        return View(entriesdto);
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