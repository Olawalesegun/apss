using APSS.Domain.Entities;
using APSS.Domain.Services;
using APSS.Web.Dtos;
using APSS.Web.Dtos.Forms;
using APSS.Web.Dtos.Parameters;
using APSS.Web.Mvc.Auth;
using APSS.Web.Mvc.Models;
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

    [HttpGet]
    public async Task<IActionResult> Index(long id, [FromQuery] FilteringParameters args)
    {
        var ret = await (await _surveysService.GetQuestionsSurveysAsync(User.GetAccountId(), id))
            .Where(q => q.Text.Contains(args.Query ?? string.Empty))
            .Page(args.Page, args.PageLength)
            .AsAsyncEnumerable()
            .Select(q => new QuestionDto
            {
                Id = q.Id,
                Index = q.Index,
                Text = q.Text,
                IsRequired = q.IsRequired,
                Survey = q.Survey,
                QuestionType =
                (QuestionTypeDto)Enum.Parse(typeof(QuestionTypeDto),
                q.GetType().ToString().Split('.').Last(), true)
            }).ToListAsync();

        return View(new CrudViewModel<QuestionDto>(ret, args));
    }

    [HttpGet]
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
        TempData["success"] = "Question Added successfully";

        return LocalRedirect(Routes.Dashboard.Surveys.Questions.FullPath + $"?id={questionDto.SurveyId}");
    }

    [HttpGet]
    public async Task<IActionResult> Update(long id)
    {
        var question = await _surveysService.GetQuestionAsync(User.GetAccountId(), id);
        var questionDto = new QuestionEditForm
        {
            Id = question.Id,
            Index = question.Index,
            Text = question.Text,
            IsRequired = question.IsRequired,
            SurveyId = question.Survey.Id,
            QuestionType =
                (QuestionTypeDto)Enum.Parse(typeof(QuestionTypeDto),
                question.GetType().ToString().Split('.').Last(), true),
        };

        if (question is MultipleChoiceQuestion)
        {
            var items = await _surveysService.GetItemsAnswer(User.GetAccountId(), question.Id);
            questionDto.CandidateAnswers = items;
        }

        return View(questionDto);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update([FromForm] QuestionEditForm question)
    {
        await _surveysService.UpdateQuestionAsync(User.GetAccountId(), question.Id, q =>
         {
             q.Text = question.Text;
             q.IsRequired = question.IsRequired;
         });

        TempData["success"] = "Question Updated successfully";

        return LocalRedirect(Routes.Dashboard.Surveys.Questions.FullPath + $"?id={question.SurveyId}");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(long id)
    {
        await _surveysService.RemoveQuestion(User.GetAccountId(), id);
        return LocalRedirect(Routes.Dashboard.Surveys.Surveys.byUser.FullPath);
    }
}