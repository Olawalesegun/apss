using Microsoft.AspNetCore.Mvc;
using APSS.Domain.Entities;
using APSS.Domain.Services;
using APSS.Web.Dtos;
using APSS.Web.Dtos.Forms;
using APSS.Web.Dtos.ValueTypes;
using APSS.Web.Mvc.Auth;

namespace APSS.Web.Mvc.Areas.Populatoin.Controllers;

[Area(Areas.Population)]
public class IndividualsController : Controller
{
    private readonly IPopulationService _populationSvc;

    public IndividualsController(IPopulationService populationService)
    {
        _populationSvc = populationService;
    }

    // GET: Individual/GetIndividuals
    public async Task<IActionResult> Index()
    {
        var individuals = await _populationSvc.GetIndividuals(User.GetAccountId()).AsAsyncEnumerable().ToListAsync();
        List<IndividualDto> individualsDto = new List<IndividualDto>();
        foreach (var individual in individuals)
        {
            individualsDto.Add(new IndividualDto
            {
                Id = individual.Id,
                Name = individual.Name,
                Job = individual.Job!,
                Address = individual.Address,
                PhonNumber = individual.PhoneNumber!,
                Sex = (SexDto)individual.Sex
            });
        }
        return View("GetIndividuals", individualsDto);
    }

    // GET: IndividualController/AddIndividual/5
    public IActionResult AddIndividual(long id)
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
    public async Task<IActionResult> AddIndividual([FromForm] IndividualAddForm individual)
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
    public async Task<IActionResult> UpdateIndividual(long id)
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

        return View("EditIndividual", individualForm);
    }

    // POST: IndividualController/UpdateIndividual/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateIndividual([FromForm] IndividualEditForm individual)
    {
        if (!ModelState.IsValid)
            return View("EditIndividual", individual);

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
    public async Task<IActionResult> ConfirmDeleteIndividual(long id)
    {
        var individual = await _populationSvc.GetIndividualAsync(User.GetAccountId(), id);
        var individualDto = new IndividualDto
        {
            Id = individual.Id,
            Name = individual.Name,
            Job = individual.Job!,
            Address = individual.Address,
            PhonNumber = individual.PhoneNumber!,
            Sex = (SexDto)individual.Sex
        };
        return View(individualDto);
    }

    // GET: IndividualController/DeleteIndividual/5
    [HttpPost, ActionName("DeleteIndividual")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteIndividual(long id)
    {
        await _populationSvc.RemoveIndividualAsync(User.GetAccountId(), id);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> IndividualDetails(long id)
    {
        var individual = await _populationSvc.GetIndividualAsync(User.GetAccountId(), id);
        var individualDto = new IndividualDto
        {
            Id = individual.Id,
            Name = individual.Name,
            Job = individual.Job!,
            Address = individual.Address,
            PhonNumber = individual.PhoneNumber!,
            Sex = (SexDto)individual.Sex,
            CreatedAt = individual.CreatedAt,
            DateOfBirth = individual.DateOfBirth,
            ModifiedAt = individual.ModifiedAt,
            NationalId = individual.NationalId,
            SocialStatus = (SocialStatusDto)individual.SocialStatus,
            User = individual.AddedBy.Name
        };
        var familyindividual = await _populationSvc.GetFamilyIndividual(User.GetAccountId(), id);
        individualDto.Family = familyindividual!.Family.Name;
        individualDto.Isparent = familyindividual.IsParent;
        individualDto.Isprovider = familyindividual.IsProvider;

        return View(individualDto);
    }
}