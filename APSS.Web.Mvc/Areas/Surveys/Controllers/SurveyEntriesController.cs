using APSS.Domain.Entities;
using APSS.Domain.Services;
using APSS.Web.Dtos;
using APSS.Web.Dtos.Parameters;
using APSS.Web.Mvc.Auth;
using APSS.Web.Mvc.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace APSS.Web.Mvc.Areas.Surveys.Controllers;

[Area(Areas.Surveys)]
public class SurveyEntriesController : Controller
{
    private readonly ISurveysService _surveysService;
    private readonly IMapper _mapper;

    public SurveyEntriesController(ISurveysService surveysService, IMapper mapper)
    {
        _surveysService = surveysService;
        _mapper = mapper;
    }

    //Get:SurveyEntry/GetSurveyEntries
    public async Task<IActionResult> Index([FromQuery] FilteringParameters args)
    {
        var ret = await (await _surveysService.GetSurveyEntriesAsync(User.GetAccountId()))
            .Page(args.Page, args.PageLength)
            .AsAsyncEnumerable()
            .Select(_mapper.Map<SurveyEntryDto>).ToListAsync();

        return View(new CrudViewModel<SurveyEntryDto>(ret, args));
    }

    //Get:SurveyEntry/SurveyEntryDetails/5
    public IActionResult SurveyEntryDetails(int id)
    {
        return View();
    }

    //GET:SurveyEntry/AddSurveyEntry/id
    public async Task<IActionResult> AddSurveyEntry(long id)
    {
        var entry = await _surveysService.GetSurveyEntryAsync(User.GetAccountId(), id);
        var entrydto = _mapper.Map<SurveyEntryDto>(entry);

        return View(entrydto);
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