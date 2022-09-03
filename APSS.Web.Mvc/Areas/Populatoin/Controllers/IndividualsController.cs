using APSS.Domain.Entities;
using APSS.Web.Dtos;
using APSS.Web.Dtos.ValueTypes;
using Microsoft.AspNetCore.Mvc;

namespace APSS.Web.Mvc.Areas.Populatoin.Controllers;

[Area(Areas.Population)]
public class IndividualsController : Controller
{
    private readonly UserDto _userDto;
    private readonly List<FamilyDto> families;
    private readonly List<IndividualDto> individuals;
    private readonly List<FamilyIndividualDto> familyIndividuals;

    public IndividualsController()
    {
        _userDto = new UserDto
        {
            Id = 1,
            Name = "aden",
            AccessLevel = AccessLevel.Root,
            userStatus = UserStatus.Active,
        };

        families = new List<FamilyDto>
        {
          new FamilyDto{Id=54,Name="ali",LivingLocation="sana'a",CreatedAt=DateTime.Now,ModifiedAt=DateTime.Now,User=_userDto },
          new FamilyDto{Id=53,Name="salih",LivingLocation="sana'a",CreatedAt=DateTime.Now,ModifiedAt=DateTime.Now,User=_userDto },
        };

        individuals = new List<IndividualDto>
        {
            new IndividualDto{Id=53, Name="ali",Address="mareb",Family=families.First(),User=_userDto,
                Sex=SexDto.Male,SocialStatus=SocialStatusDto.Unspecified,NationalId="57994",
                PhonNumber="895499",CreatedAt=DateTime.Today,DateOfBirth=DateTime.Today,ModifiedAt=DateTime.Now,Job="programmer"},
            new IndividualDto{Id=43,  Name="ali",Address="mareb",Family=families.First(),User=_userDto,
                Sex=SexDto.Female,SocialStatus=SocialStatusDto.Unspecified,NationalId="57994",
                PhonNumber="895499",CreatedAt=DateTime.Today,DateOfBirth=DateTime.Today,ModifiedAt=DateTime.Now,Job="programmer"},
        };

        familyIndividuals = new List<FamilyIndividualDto>
        {
            new FamilyIndividualDto{Id=54,Individual=individuals.First(),Family=families.First(),IsParent=true,IsProvider=true},
            new FamilyIndividualDto{Id=54,Individual=individuals.Last(),Family=families.Last(),IsParent=true,IsProvider=true},
        };
    }

    // GET: Individual/GetIndividuals
    public IActionResult Index()
    {
        return View(individuals);
    }

    // GET: IndividualController/AddIndividual/5
    public IActionResult AddIndividual(int id)
    {
        var family = families.Find(f => f.Id == id);
        var individual = new IndividualAddDto
        {
            Family = family!
        };
        return View("GetIndividuals", individual);
    }

    // POST: IndividualController/AddIndividual
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult AddIndividual(long id, IndividualAddDto individual)
    {
        var family = families.Find(f => f.Id == id);
        individual.Family = family!;
        if (!ModelState.IsValid)
        {
            return View(individual);
        }
        return View(individual);
    }

    // Get: IndividualController/UpdateIndividual/5
    public IActionResult UpdateIndividual(int id)
    {
        var individual = individuals.Find(i => i.Id == id);

        return View("EditIndividual", individual);
    }

    // POST: IndividualController/UpdateIndividual/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult UpdateIndividual(int id, IndividualDto individual)
    {
        var newindividual = individuals.Find(i => i.Id == id);
        individual.Family = newindividual!.Family;

        return View("EditIndividual", individual);
    }

    // GET: IndividualController/ConfirmDeleteIndividual/5
    public IActionResult ConfirmDeleteIndividual(int id)
    {
        var individual = individuals.Find(i => i.Id == id);
        return View(individual);
    }

    // GET: IndividualController/DeleteIndividual/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteIndividual(int id, IndividualDto individualDto)
    {
        return View(nameof(Index), individuals);
    }

    public IActionResult IndividualDetails(int id)
    {
        var individual = individuals.Find(i => i.Id == id);
        return View(individual);
    }
}