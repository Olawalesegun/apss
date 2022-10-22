using Microsoft.AspNetCore.Mvc;
using APSS.Domain.Services;
using APSS.Web.Dtos;
using APSS.Web.Dtos.Forms;
using APSS.Web.Mvc.Auth;
using APSS.Web.Mvc.Util.Navigation.Routes;
using APSS.Web.Mvc.Models;
using APSS.Web.Dtos.Parameters;
using AutoMapper;

namespace APSS.Web.Mvc.Areas.Surveys.Controllers;

[Area(Areas.Surveys)]
public class SurveysController : Controller
{
    private readonly ISurveysService _surveySvc;
    private readonly IMapper _mapper;

    public SurveysController(ISurveysService surveySvc, IMapper mapper)
    {
        _surveySvc = surveySvc;
        _mapper = mapper;
    }

    // GET: Survey/GetSurveys
    [HttpGet]
    public async Task<ActionResult> Index([FromQuery] FilteringParameters args)
    {
        var ret = await (await _surveySvc.GetSurveysAsync(User.GetAccountId()))
            .Where(s => s.Name.Contains(args.Query ?? string.Empty))
            .Page(args.Page, args.PageLength)
            .AsAsyncEnumerable()
            .Select(_mapper.Map<SurveyDto>)
            .ToListAsync();

        return View(new CrudViewModel<SurveyDto>(ret, args));
    }

    // GET: Survey/SurveyDetails/5
    [HttpGet]
    public async Task<ActionResult> Details(long id)
    {
        return View(_mapper.Map<SurveyDto>(
               await _surveySvc.GetSurveyAsync(User.GetAccountId(), id)));
    }

    // GET: Survey/Add Survey
    [HttpGet]
    public ActionResult Add() => View();

    // POST: Survey/AddSurvey
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Add([FromForm] SurveyAddForm form)
    {
        if (!ModelState.IsValid)
            return View(form);

        await _surveySvc.CreateSurveyAsync(User.GetAccountId(), form.Name, form.ExpirationDate);

        return LocalRedirect(Routes.Dashboard.Surveys.Surveys.FullPath);
    }

    [HttpPost, ActionName("ActiveSurvey")]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> ActiveSurvey(long id, bool active)
    {
        await _surveySvc.SetSurveyActiveStatusAsync(User.GetAccountId(), id, active);

        return RedirectToAction(nameof(Index));
    }

    // GET: Survey/EditSurvey/5
    [HttpGet]
    public async Task<ActionResult> Update(long id)
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
    public async Task<ActionResult> Update([FromForm] SurveyEditForm form)
    {
        if (!ModelState.IsValid)
            return View(form);

        await _surveySvc.UpdateSurveyAsync(User.GetAccountId(), form.Id, s =>
        {
            s.Name = form.Name;
            s.IsActive = form.IsActive;
            s.ExpirationDate = form.ExpirationDate;
        });

        return LocalRedirect(Routes.Dashboard.Surveys.Surveys.FullPath);
    }

    // POST: Survey/SurveyDelete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Delete(long id)
    {
        await _surveySvc.RemoveSurveyAsync(User.GetAccountId(), id);

        return LocalRedirect(Routes.Dashboard.Surveys.Surveys.FullPath);
    }
}