using Microsoft.AspNetCore.Mvc;
using APSS.Domain.Entities;
using APSS.Domain.Services;
using APSS.Web.Dtos;
using APSS.Web.Dtos.Forms;
using APSS.Web.Dtos.ValueTypes;

using APSS.Web.Mvc.Auth;
using APSS.Web.Dtos.Parameters;
using APSS.Web.Mvc.Models;
using AutoMapper;
using APSS.Web.Mvc.Util.Navigation.Routes;

namespace APSS.Web.Mvc.Areas.Populatoin.Controllers;

[Area(Areas.Population)]
public class VoluntariesController : Controller
{
    private readonly IPopulationService _populationSvc;
    private readonly IMapper _mapper;

    public VoluntariesController(IPopulationService populationService, IMapper mapper)
    {
        _populationSvc = populationService;
        _mapper = mapper;
    }

    // GET: VoluntaryController/GetVoluntaries/5
    public async Task<IActionResult> Index([FromQuery] FilteringParameters args, long id)
    {
        var ret = await (await _populationSvc.GetVoluntaryOfindividualAsync(User.GetAccountId(), id))
            .Where(s => s.Name.Contains(args.Query))
            .Page(args.Page, args.PageLength)
            .AsAsyncEnumerable()
            .Select(_mapper.Map<VoluntaryDto>)
            .ToListAsync();

        return View(new CrudViewModel<VoluntaryDto>(ret, args));
    }

    // GET: VoluntaryController/AddVoluntary/5
    public ActionResult Add(long id)
    {
        var Voluntary = new VoluntaryAddForm
        {
            IndividualId = id
        };

        return View(Voluntary);
    }

    // POST: VoluntaryController/AddVoluntary
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Add([FromForm] VoluntaryAddForm Voluntary)
    {
        if (!ModelState.IsValid)
            return View(Voluntary);

        await _populationSvc
            .AddVoluntaryAsync(User.GetAccountId(), Voluntary.IndividualId, Voluntary.Name, Voluntary.Field);

        return LocalRedirect(Routes.Dashboard.Population.Voluntaries.FullPath + $"?id={Voluntary.IndividualId}");
    }

    //GET:VoluntaryController/UpdateVoluntary/5
    public async Task<ActionResult> Update(long id)
    {
        var Voluntary = await _populationSvc.GetVoluntaryAsync(User.GetAccountId(), id);
        var Voluntaryform = new VoluntaryEditForm
        {
            Id = Voluntary.Id,
            IndividualId = Voluntary.OfferedBy.Id,
            Name = Voluntary.Name,
            Field = Voluntary.Field
        };
        return View(Voluntaryform);
    }

    // POST: VoluntaryController/UpdateVoluntary/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Update([FromForm] VoluntaryEditForm Voluntary)
    {
        if (!ModelState.IsValid)
            return View(Voluntary);

        await _populationSvc
            .UpdateVoluntaryAsync(User.GetAccountId(), Voluntary.Id,
            v =>
            {
                v.Name = Voluntary.Name;
                v.Field = Voluntary.Field;
            });
        return LocalRedirect(Routes.Dashboard.Population.Voluntaries.FullPath + $"?id={Voluntary.IndividualId}");
    }

    public async Task<IActionResult> ConfirmDeleteVoluntary(long id)
    {
        var Voluntary = await _populationSvc.GetVoluntaryAsync(User.GetAccountId(), id);
        var Voluntarydto = new VoluntaryDto
        {
            Id = Voluntary.Id,
            Name = Voluntary.Name,
            Field = Voluntary.Field,
            OfferedBy = Voluntary.OfferedBy
        };

        return View(Voluntarydto);
    }

    // POST: VoluntaryController/DeleteVoluntary/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Delete(long id)
    {
        var v = await _populationSvc.GetVoluntaryAsync(User.GetAccountId(), id);
        await _populationSvc.RemoveVoluntaryAsync(User.GetAccountId(), id);
        return LocalRedirect(Routes.Dashboard.Population.Voluntaries.FullPath + $"?id={v.OfferedBy.Id}");
    }
}