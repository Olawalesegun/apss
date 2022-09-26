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
using APSS.Web.Mvc.Util.Navigation.Routes;

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
    [ApssAuthorized(AccessLevel.All ^ AccessLevel.Farmer, PermissionType.Read)]
    public async Task<IActionResult> Index([FromQuery] FilteringParameters args)
    {
        var ret = await (await _populationSvc.GetIndividuals(User.GetAccountId()))
            .Where(u => u.Name.Contains(args.Query ?? string.Empty))
            .Page(args.Page, args.PageLength)
            .AsAsyncEnumerable()
            .Select(_mapper.Map<IndividualDto>)
            .ToListAsync();

        return View(new CrudViewModel<IndividualDto>(ret, args));
    }

    // GET: IndividualController/AddIndividual/5
    [ApssAuthorized(AccessLevel.Group, PermissionType.Create)]
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
    [ApssAuthorized(AccessLevel.Group, PermissionType.Create)]
    public async Task<IActionResult> Add([FromForm] IndividualAddForm individual)
    {
        if (!ModelState.IsValid)
        {
            return View(individual);
        }
        await _populationSvc.AddIndividualAsync(
            User.GetAccountId(), individual.FamilyId, individual.Name, individual.Address, (IndividualSex)individual.Sex);
        TempData["success"] = "Individual Added successfully";

        return LocalRedirect(Routes.Dashboard.Population.Individuals.FullPath);
    }

    // Get: IndividualController/UpdateIndividual/5
    [ApssAuthorized(AccessLevel.Group, PermissionType.Update)]
    public async Task<IActionResult> Update(long id)
    {
        var individual = await (await _populationSvc.GetIndividualAsync(User.GetAccountId(), id)).FirstAsync();
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
        var family = await (await _populationSvc.GetFamilyIndividual(User.GetAccountId(), id)).FirstOrNullAsync();
        if (family != null)
        {
            individualForm.FamilyId = family!.Family.Id;
            individualForm.FamilyName = family!.Family.Name;
            individualForm.Isparent = family.IsParent;
            individualForm.Isprovider = family.IsProvider;
        }

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
        if (individual.FamilyId != 0)
        {
            var family = await _populationSvc.GetFamilyAsync(User.GetAccountId(), individual.FamilyId);

            await _populationSvc.UpdateFamilyIndividualAsync(User.GetAccountId(), individual.Id,
                i =>
                {
                    i.Family = family;
                    i.IsParent = individual.Isparent;
                    i.IsProvider = individual.Isprovider;
                });
        }

        TempData["success"] = "Individual Updated successfully";

        return LocalRedirect(Routes.Dashboard.Population.Individuals.FullPath);
    }

    // POST: IndividualController/DeleteIndividual/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    [ApssAuthorized(AccessLevel.Group, PermissionType.Delete)]
    public async Task<IActionResult> Delete(long id)
    {
        await _populationSvc.RemoveIndividualAsync(User.GetAccountId(), id);

        TempData["success"] = "Individual deleted successfully";

        return LocalRedirect(Routes.Dashboard.Population.Individuals.FullPath);
    }

    [ApssAuthorized(AccessLevel.Group, PermissionType.Read)]
    public async Task<IActionResult> Details(long id)
    {
        var individual = await (await _populationSvc.GetIndividualAsync(User.GetAccountId(), id)).FirstAsync();

        var individualDto = _mapper.Map<IndividualDto>(individual);

        var family = await (await _populationSvc.GetFamilyIndividual(User.GetAccountId(), id)).FirstOrNullAsync();
        if (family != null)
            individualDto.Family = _mapper.Map<FamilyDto>(family!.Family);

        return View(individualDto);
    }
}