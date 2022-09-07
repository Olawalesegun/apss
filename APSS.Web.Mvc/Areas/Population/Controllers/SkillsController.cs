using Microsoft.AspNetCore.Mvc;
using APSS.Domain.Services;
using APSS.Web.Dtos;
using APSS.Web.Dtos.Forms;
using APSS.Web.Mvc.Auth;

namespace APSS.Web.Mvc.Areas.Populatoin.Controllers;

[Area(Areas.Population)]
public class SkillsController : Controller
{
    private readonly IPopulationService _populationSvc;

    public SkillsController(IPopulationService populationService)
    {
        _populationSvc = populationService;
    }

    // GET: SkillController/GetSkills/5
    public async Task<ActionResult> Index(long id)
    {
        var skills = await _populationSvc.GetSkillOfindividualAsync(User.GetAccountId(), id);
        var skillsDto = new List<SkillDto>();

        foreach (var skill in await skills.AsAsyncEnumerable().ToListAsync())
        {
            skillsDto.Add(new SkillDto
            {
                Id = skill.Id,
                Name = skill.Name,
                Description = skill.Description,
                IndividualName = skill.BelongsTo.Name,
                Field = skill.Field
            });
        }
        return View("GetSkills", skillsDto.ToList());
    }

    // GET: SkillController/AddSkill/5
    public ActionResult AddSkill(long id)
    {
        var skill = new SkillAddForm
        {
            IndividualId = id
        };

        return View(skill);
    }

    // POST: skillController/AddSkill
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> AddSkill([FromForm] SkillAddForm skill)
    {
        if (!ModelState.IsValid)
            return View(skill);

        await _populationSvc
            .AddSkillAsync(User.GetAccountId(), skill.IndividualId, skill.Name, skill.Field, skill.Description);

        return RedirectToAction(nameof(Index), new { id = skill.IndividualId });
    }

    //GET:SkillController/UpdateSkill/5
    public async Task<ActionResult> UpdateSkill(long id)
    {
        var skill = await _populationSvc.GetSkillAsync(User.GetAccountId(), id);
        var skillform = new SkillEditForm
        {
            Id = skill.Id,
            IndividualId = skill.BelongsTo.Id,
            Name = skill.Name,
            Description = skill.Description,
            Field = skill.Field
        };
        return View("EditSkill", skillform);
    }

    // POST: SkillController/UpdateSkill/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> UpdateSkill([FromForm] SkillEditForm skill)
    {
        if (!ModelState.IsValid)
            return View("EditSkill", skill);

        await _populationSvc
            .UpdateSkillAsync(User.GetAccountId(), skill.Id,
            s =>
            {
                s.Name = skill.Name;
                s.Description = skill.Description;
                s.Field = skill.Field;
            });
        long id = skill.IndividualId;
        return RedirectToAction(nameof(Index), new { id = skill.IndividualId });
    }

    public async Task<IActionResult> ConfirmDeleteSkill(long id)
    {
        var skill = await _populationSvc.GetSkillAsync(User.GetAccountId(), id);
        var skilldto = new SkillDto
        {
            Id = skill.Id,
            Name = skill.Name,
            Description = skill.Field,
            Field = skill.Field,
            IndividualName = skill.BelongsTo.Name
        };

        return View(skilldto);
    }

    // POST: SkillController/DeleteSkill/5
    [HttpPost, ActionName("DeleteSkill")]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> DeleteSkill(long id)
    {
        var s = await _populationSvc.GetSkillAsync(User.GetAccountId(), id);
        await _populationSvc.RemoveSkillAsync(User.GetAccountId(), id);
        return RedirectToAction(nameof(Index), new { id = s.BelongsTo.Id });
    }
}