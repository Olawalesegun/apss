using Microsoft.AspNetCore.Mvc;
using APSS.Domain.Services;
using APSS.Web.Dtos;
using APSS.Web.Dtos.Forms;
using APSS.Web.Mvc.Auth;
using APSS.Web.Dtos.Parameters;
using AutoMapper;
using APSS.Web.Mvc.Models;
using APSS.Web.Mvc.Util.Navigation.Routes;

namespace APSS.Web.Mvc.Areas.Populatoin.Controllers;

[Area(Areas.Population)]
public class SkillsController : Controller
{
    private readonly IPopulationService _populationSvc;
    private readonly IMapper _mapper;

    public SkillsController(IPopulationService populationService, IMapper mapper)
    {
        _populationSvc = populationService;
        _mapper = mapper;
    }

    // GET: SkillController/GetSkills/5
    public async Task<IActionResult> Index([FromQuery] FilteringParameters args, long id)
    {
        var ret = await (await _populationSvc.GetSkillOfindividualAsync(User.GetAccountId(), id))
            .Where(s => s.Name.Contains(args.Query))
            .Page(args.Page, args.PageLength)
            .AsAsyncEnumerable()
            .Select(_mapper.Map<SkillDto>)
            .ToListAsync();

        return View(new CrudViewModel<SkillDto>(ret, args));
    }

    // GET: SkillController/AddSkill/5
    public ActionResult Add(long id)
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
    public async Task<ActionResult> Add([FromForm] SkillAddForm skill)
    {
        if (!ModelState.IsValid)
            return View(skill);

        await _populationSvc
            .AddSkillAsync(User.GetAccountId(), skill.IndividualId, skill.Name, skill.Field, skill.Description);

        return LocalRedirect(Routes.Dashboard.Population.Skills.FullPath + $"?id={skill.IndividualId}");
    }

    //GET:SkillController/UpdateSkill/5
    public async Task<ActionResult> Update(long id)
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
        return View(skillform);
    }

    // POST: SkillController/UpdateSkill/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Update([FromForm] SkillEditForm skill)
    {
        if (!ModelState.IsValid)
            return View(skill);

        await _populationSvc
            .UpdateSkillAsync(User.GetAccountId(), skill.Id,
            s =>
            {
                s.Name = skill.Name;
                s.Description = skill.Description;
                s.Field = skill.Field;
            });
        long id = skill.IndividualId;
        return LocalRedirect(Routes.Dashboard.Population.Skills.FullPath + $"?id={skill.IndividualId}");
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
            BelongsTo = skill.BelongsTo
        };

        return View(skilldto);
    }

    // POST: SkillController/DeleteSkill/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Delete(long id)
    {
        var s = await _populationSvc.GetSkillAsync(User.GetAccountId(), id);
        await _populationSvc.RemoveSkillAsync(User.GetAccountId(), id);
        return LocalRedirect(Routes.Dashboard.Population.Skills.FullPath + $"?id={s.BelongsTo.Id}");
    }
}