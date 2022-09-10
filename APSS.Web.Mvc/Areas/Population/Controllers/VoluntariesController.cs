using Microsoft.AspNetCore.Mvc;
using APSS.Domain.Entities;
using APSS.Domain.Services;
using APSS.Web.Dtos;
using APSS.Web.Dtos.Forms;
using APSS.Web.Dtos.ValueTypes;

using APSS.Web.Mvc.Auth;

namespace APSS.Web.Mvc.Areas.Populatoin.Controllers;

[Area(Areas.Population)]
public class VoluntariesController : Controller
{
    private readonly IPopulationService _populationSvc;

    public VoluntariesController(IPopulationService populationService)
    {
        _populationSvc = populationService;
    }

    // GET: VoluntaryController/GetVoluntaries/5
    public async Task<ActionResult> Index(long id)
    {
        var Voluntaries = await _populationSvc.GetVoluntaryOfindividualAsync(User.GetAccountId(), id);
        var VoluntariesDto = new List<VoluntaryDto>();

        foreach (var Voluntary in await Voluntaries.AsAsyncEnumerable().ToListAsync())
        {
            VoluntariesDto.Add(new VoluntaryDto
            {
                Id = Voluntary.Id,
                Name = Voluntary.Name,
                Field = Voluntary.Field,
                IndividualName = Voluntary.OfferedBy.Name
            });
        }
        return View("GetVoluntaries", VoluntariesDto.ToList());
    }

    // GET: VoluntaryController/AddVoluntary/5
    public ActionResult AddVoluntary(long id)
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
    public async Task<ActionResult> AddVoluntary([FromForm] VoluntaryAddForm Voluntary)
    {
        if (!ModelState.IsValid)
            return View(Voluntary);

        await _populationSvc
            .AddVoluntaryAsync(User.GetAccountId(), Voluntary.IndividualId, Voluntary.Name, Voluntary.Field);

        return RedirectToAction(nameof(Index), new { id = Voluntary.IndividualId });
    }

    //GET:VoluntaryController/UpdateVoluntary/5
    public async Task<ActionResult> UpdateVoluntary(long id)
    {
        var Voluntary = await _populationSvc.GetVoluntaryAsync(User.GetAccountId(), id);
        var Voluntaryform = new VoluntaryEditForm
        {
            Id = Voluntary.Id,
            IndividualId = Voluntary.OfferedBy.Id,
            Name = Voluntary.Name,
            Field = Voluntary.Field
        };
        return View("EditVoluntary", Voluntaryform);
    }

    // POST: VoluntaryController/UpdateVoluntary/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> UpdateVoluntary([FromForm] VoluntaryEditForm Voluntary)
    {
        if (!ModelState.IsValid)
            return View("EditVoluntary", Voluntary);

        await _populationSvc
            .UpdateVoluntaryAsync(User.GetAccountId(), Voluntary.Id,
            v =>
            {
                v.Name = Voluntary.Name;
                v.Field = Voluntary.Field;
            });
        return RedirectToAction(nameof(Index), new { id = Voluntary.IndividualId });
    }

    public async Task<IActionResult> ConfirmDeleteVoluntary(long id)
    {
        var Voluntary = await _populationSvc.GetVoluntaryAsync(User.GetAccountId(), id);
        var Voluntarydto = new VoluntaryDto
        {
            Id = Voluntary.Id,
            Name = Voluntary.Name,
            Field = Voluntary.Field,
            IndividualName = Voluntary.OfferedBy.Name
        };

        return View(Voluntarydto);
    }

    // POST: VoluntaryController/DeleteVoluntary/5
    [HttpPost, ActionName("DeleteVoluntary")]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> DeleteVoluntary(long id)
    {
        var v = await _populationSvc.GetVoluntaryAsync(User.GetAccountId(), id);
        await _populationSvc.RemoveVoluntaryAsync(User.GetAccountId(), id);
        return RedirectToAction(nameof(Index), new { id = v.OfferedBy.Id });
    }
}