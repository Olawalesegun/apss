using APSS.Domain.Entities;
using APSS.Web.Dtos;
using APSS.Web.Dtos.ValueTypes;
using Microsoft.AspNetCore.Mvc;

namespace APSS.Web.Mvc.Controllers;

public class SkillsController : Controller
{
    private readonly UserDto _userDto;
    private readonly List<FamilyDto> families;
    private readonly List<IndividualDto> individuals;

    private List<SkillDto> skills;

    public SkillsController()
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
        skills = new List<SkillDto>
        {
            new SkillDto{Id=543,Individual=individuals.First(),Name="skill1",Description="anyskill",
                Field="anyfiald",CreatedAt=DateTime.Now,ModifiedAt=DateTime.Now},
            new SkillDto{Id=85,Individual=individuals.Last(),Name="skill2",Description="anyskill",
                Field="anyfiald",CreatedAt=DateTime.Now,ModifiedAt=DateTime.Now},
        };
    }

    // GET: SkillController/GetSkills/5
    public ActionResult Index(int id)
    {
        var skill = skills.Where(s => s.Individual.Id == id);
        return View("GetSkills", skill);
    }

    // GET: SkillController/AddSkill/5
    public ActionResult AddSkill(int id)
    {
        var individual = individuals.Find(individual => individual.Id == id);
        var skill = new SkillDto
        {
            Individual = individual!
        };
        return View(skill);
    }

    // POST: skillController/AddSkill
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult AddSkill(int id, SkillDto skill)
    {
        skill.Individual = individuals.Find(i => i.Id == id)!;

        return View(skill);
    }

    //GET:SkillController/UpdateSkill/5
    public ActionResult UpdateSkill(int id)
    {
        var skill = skills.Find(s => s.Id == id);
        var individual = individuals.Find(i => i.Id == skill!.Individual.Id);
        skill!.Individual = individual!;
        return View("EditSkill", skill!);
    }

    // POST: SkillController/UpdateSkill/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult UpdateSkill(int id, SkillDto newskill)
    {
        newskill.Individual = individuals.Find(i => i.Id == id)!;
        return View("EditSkill", newskill);
    }

    public IActionResult ConfirmDeleteSkill(int id)
    {
        var skill = skills.Find(s => s.Id == id);
        return View(skill);
    }

    // GET: SkillController/DeleteSkill/5
    public ActionResult DeleteSkill(int id)
    {
        return View(nameof(Index), skills);
    }

    // POST: SkillController/DeleteSkill/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteSkill(int id, SkillDto skilldto)
    {
        return View(nameof(Index), skills);
    }
}