using APSS.Domain.Entities;
using APSS.Domain.Services;
using APSS.Web.Dtos;
using APSS.Web.Dtos.Forms;
using APSS.Web.Mvc.Auth;
using APSS.Web.Mvc.Util.Navigation.Routes;
using Microsoft.AspNetCore.Mvc;

namespace APSS.Web.Mvc.Areas.Surveys.Controllers;

[Area(Areas.Surveys)]
public class QuestionsController : Controller
{
    private readonly ISurveysService _surveysService;

    public QuestionsController(ISurveysService surveysService)
    {
        _surveysService = surveysService;
    }

    public IActionResult Add(long id)
    {
        var questiondto = new QuestionAddForm
        {
            SurveyId = id
        };

        return View(questiondto);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add([FromForm] QuestionAddForm questionDto)
    {
        if (!ModelState.IsValid)
        {
            return View(questionDto);
        }
        if (questionDto.QuestionType == QuestionTypeDto.TextQuestion)
        {
            await _surveysService
                .AddTextQuestionAsync(User.GetAccountId(), questionDto.SurveyId,
                questionDto.Text, questionDto.IsRequired);
        }
        else if (questionDto.QuestionType == QuestionTypeDto.LogicalQuestion)
        {
            await _surveysService
                .AddLogicalQuestionAsync(User.GetAccountId(), questionDto.SurveyId,
                questionDto.Text, questionDto.IsRequired);
        }
        else if (questionDto.QuestionType == QuestionTypeDto.MultipleChoiceQuestion)
        {
            var awnsers = questionDto.CandidateAnswers!.ToList();
            foreach (var awnser in awnsers)
            {
                if (awnser == null)
                {
                    questionDto.CandidateAnswers!.Remove(awnser!);
                }
            }
            await _surveysService
                .AddMultipleChoiceQuestionAsync(User.GetAccountId(), questionDto.SurveyId,
                questionDto.Text, questionDto.IsRequired,
                questionDto.CanMultiSelect!, questionDto.CandidateAnswers!.ToList());
        }
        return View("byUser", questionDto.SurveyId);
    }

    public async Task<IActionResult> GetAll(long id)
    {
        var questions = await _surveysService.GetQuestionsSurveysAsync(User.GetAccountId(), id);
        List<QuestionDto> questionsdto = new List<QuestionDto>();
        foreach (var question in await questions.AsAsyncEnumerable().ToListAsync())
        {
            questionsdto.Add(new QuestionDto
            {
                Id = question.Id,
                Index = question.Index,
                Text = question.Text,
                IsRequired = question.IsRequired,
                Survey = question.Survey,
                QuestionType =
                (QuestionTypeDto)Enum.Parse(typeof(QuestionTypeDto),
                question.GetType().ToString().Split('.').Last(), true)
            });
        }
        return View(questionsdto);
    }

    public IActionResult Update()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Update(IFormCollection form)
    {
        return View();
    }

    [HttpPost, ActionName("DeleteQuestion")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(long id)
    {
        await _surveysService.Removequestion(User.GetAccountId(), id);
        return LocalRedirect(Routes.Dashboard.Surveys.Surveys.byUser.FullPath);
    }
}