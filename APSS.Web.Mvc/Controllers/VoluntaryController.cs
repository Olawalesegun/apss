using APSS.Domain.Entities;
using APSS.Web.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace APSS.Web.Mvc.Controllers;

public class VoluntaryController : Controller
{
    private readonly UserDto _userDto;
    private readonly List<FamilyDto> families;
    private readonly List<IndividualDto> individuals;
    private readonly List<VoluntaryDto> voluntaries;

    public VoluntaryController()
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
                    Sex=IndividualDto.SexDto.Male,SocialStatus=IndividualDto.SocialStatusDto.Unspecified,NationalId="57994",
                    PhonNumber="895499",CreatedAt=DateTime.Today,DateOfBirth=DateTime.Today,ModifiedAt=DateTime.Now,Job="programmer"},
                new IndividualDto{Id=43,  Name="ali",Address="mareb",Family=families.First(),User=_userDto,
                    Sex=IndividualDto.SexDto.Female,SocialStatus=IndividualDto.SocialStatusDto.Unspecified,NationalId="57994",
                    PhonNumber="895499",CreatedAt=DateTime.Today,DateOfBirth=DateTime.Today,ModifiedAt=DateTime.Now,Job="programmer"},
            };

        voluntaries = new List<VoluntaryDto>
        {
            new VoluntaryDto{Id=543,Individual=individuals.First(),Name="skill1",
                Field="anyfiald",CreatedAt=DateTime.Now,ModifiedAt=DateTime.Now},
            new VoluntaryDto{Id=85,Individual=individuals.Last(),Name="skill2",
                Field="anyfiald",CreatedAt=DateTime.Now,ModifiedAt=DateTime.Now},
        };
    }

    // GET: VoluntaryController/GetVoluntaries/5
    public ActionResult GetVoluntaries(int id)
    {
        var voluntary = voluntaries.Where(v => v.Individual.Id == id);
        return View(voluntary);
    }

    // GET: VoluntaryController/AddVoluntary/5
    public ActionResult AddVoluntary(int id)
    {
        var voluntary = new VoluntaryDto();
        var individual = individuals.Find(i => i.Id == id);
        voluntary.Individual = individual!;
        return View(voluntary);
    }

    // POST:VoluntaryController/AddVoluntary
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult AddVoluntary(VoluntaryDto voluntary)
    {
        return View(voluntary);
    }

    //GET:VoluntaryController/UpdateVoluntary/5
    public ActionResult UpdateVoluntary(int id)
    {
        var voluntary = voluntaries.Find(v => v.Id == id);
        return View("EditVoluntary", voluntary);
    }

    // POST: VoluntaryController/UpdateVoluntary/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult UpdateVoluntary(int id, VoluntaryDto newvoluntary)
    {
        return View("EditVoluntary", newvoluntary);
    }

    public IActionResult ConfirmDeleteVoluntary(int id)
    {
        var voluntary = voluntaries.Find(v => v.Id == id);
        return View(voluntary);
    }

    // GET: VoluntaryController/DeleteVoluntary/5
    public ActionResult DeleteVoluntary(int id)
    {
        return RedirectToAction(nameof(GetVoluntaries), voluntaries);
    }

    // POST: VoluntaryController/DeleteVoluntary/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteVoluntary(int id, VoluntaryDto voluntaryDto)
    {
        return RedirectToAction(nameof(GetVoluntaries), voluntaries);
    }
}