using Microsoft.AspNetCore.Mvc;
using APSS.Domain.Entities;
using APSS.Domain.Services;
using APSS.Web.Dtos;
using APSS.Web.Dtos.Forms;
using APSS.Web.Dtos.ValueTypes;
using APSS.Web.Mvc.Auth;
using APSS.Web.Dtos.Parameters;
using AutoMapper;
using APSS.Web.Mvc.Models;

namespace APSS.Web.Mvc.Areas.Populatoin.Controllers;

[Area(Areas.Population)]
public class IndividualsController : Controller
{
    private readonly IPopulationService _populationSvc;
    private readonly IMapper _mapper;

    public IndividualsController(IPopulationService populationService, IMapper mapper)
    {
        _populationSvc = populationService;
        _mapper = mapper;
    }

    // GET: Individual/GetIndividuals
    public async Task<IActionResult> Index([FromQuery] FilteringParameters args)
    {
        var ret = await (await _populationSvc.GetIndividuals(User.GetAccountId()))
            .Where(u => u.Name.Contains(args.Query))
            .Page(args.Page, args.PageLength)
            .AsAsyncEnumerable()
            .Select(_mapper.Map<IndividualDto>)
            .ToListAsync();

        return View(new CrudViewModel<IndividualDto>(ret, args));
    }

    // GET: IndividualController/AddIndividual/5
    public IActionResult Add(long id)
    {
        var individual = new IndividualAddForm
        {
            FamilyId = id
        };
        return View(individual);
    }

    // POST: IndividualController/AddIndividual
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add([FromForm] IndividualAddForm individual)
    {
        if (!ModelState.IsValid)
        {
            return View(individual);
        }
        await _populationSvc.AddIndividualAsync(
            User.GetAccountId(), individual.FamilyId, individual.Name, individual.Address, (IndividualSex)individual.Sex);

        return RedirectToAction(nameof(Index));
    }

    // Get: IndividualController/UpdateIndividual/5
    public async Task<IActionResult> Update(long id)
    {
        var individual = await _populationSvc.GetIndividualAsync(User.GetAccountId(), id);
        var individualForm = new IndividualEditForm
        {
            Id = individual.Id,
            Name = individual.Name,
            Address = individual.Address,
            Job = individual.Job!,
            PhonNumber = individual.PhoneNumber!,
            NationalId = individual.NationalId!,
            DateOfBirth = individual.DateOfBirth!,
            Sex = (SexDto)individual.Sex,
            SocialStatus = (SocialStatusDto)individual.SocialStatus!,
        };
        var family = await _populationSvc.GetFamilyIndividual(User.GetAccountId(), id);
        individualForm.FamilyId = family!.Family.Id;
        individualForm.FamilyName = family!.Family.Name;
        individualForm.Isparent = family.IsParent;
        individualForm.Isprovider = family.IsProvider;

        return View(individualForm);
    }

    // POST: IndividualController/UpdateIndividual/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update([FromForm] IndividualEditForm individual)
    {
        if (!ModelState.IsValid)
            return View(individual);

        await _populationSvc.UpdateIndividualAsync(
            User.GetAccountId(), individual.Id,
            i =>
            {
                i.Sex = (IndividualSex)individual.Sex;
                i.SocialStatus = (SocialStatus)individual.SocialStatus!;
                i.NationalId = individual.NationalId!;
                i.DateOfBirth = individual.DateOfBirth!;
                i.PhoneNumber = individual.PhonNumber;
                i.Address = individual.Address;
                i.Job = individual.Job;
                i.Name = individual.Name;
            });
        var family = await _populationSvc.GetFamilyAsync(User.GetAccountId(), individual.FamilyId);

        await _populationSvc.UpdateFamilyIndividualAsync(User.GetAccountId(), individual.Id,
            i =>
            {
                i.Family = family;
                i.IsParent = individual.Isparent;
                i.IsProvider = individual.Isprovider;
            });

        return RedirectToAction(nameof(Index));
    }

    // GET: IndividualController/ConfirmDeleteIndividual/5
    public async Task<IActionResult> Delete(long id)
    {
        var individual = await _populationSvc.GetIndividualAsync(User.GetAccountId(), id);
        var individualDto = new IndividualDto
        {
            Id = individual.Id,
            Name = individual.Name,
            Job = individual.Job!,
            Address = individual.Address,
            PhonNumber = individual.PhoneNumber!,
            Sex = individual.Sex
        };
        return View(individualDto);
    }

    // GET: IndividualController/DeleteIndividual/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(long id, IndividualDto individual)
    {
        await _populationSvc.RemoveIndividualAsync(User.GetAccountId(), id);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Details(long id)
    {
        var individual = await _populationSvc.GetIndividualAsync(User.GetAccountId(), id);
        var individualDto = _mapper.Map<IndividualDto>(individual);

        return View(individualDto);
    }
}