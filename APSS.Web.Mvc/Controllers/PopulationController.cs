using APSS.Domain.Entities;
using APSS.Domain.Services;
using APSS.Web.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APSS.Web.Mvc.Controllers
{
    public class PopulationController : Controller
    {
        private readonly IPopulationService _populationSvc;
        private readonly UserDto userDto = new() { Name = "yemen", AccessLevel = AccessLevel.Root, userStatus = UserStatus.Active, SupervisedBy = null };
        private List<FamilyDto> families = null!;
        private List<IndividualDto> individuals = null!;
        private List<SkillDto> skills = null!;
        private List<VoluntaryDto> voluntaries = null!;
        private List<FamilyIndividualDto> familyIndividuals;

        private readonly SocialStatusDto status = new SocialStatusDto
        {
            _socialStatuses = Enum.GetValues(typeof(SocialStatus))
 .Cast<SocialStatus>()
 .Select(v => v).ToList()
        };

        public PopulationController(IPopulationService populationSvc)
        {
            _populationSvc = populationSvc;

            //families List
            families = new List<FamilyDto>
           {
               new FamilyDto{Id=0,Name="ali",CreatedAt=DateTime.Now, LivingLocation="sana'a",User=userDto},
               new FamilyDto{Id=1,Name="ali",CreatedAt=DateTime.Now, LivingLocation="sana'a",User=userDto},
               new FamilyDto{Id=2,Name="ali",CreatedAt=DateTime.Now, LivingLocation="sana'a",User=userDto},
               new FamilyDto{Id=3,Name="ali",CreatedAt=DateTime.Now, LivingLocation="sana'a",User=userDto},
               new FamilyDto{Id=4,Name="ali",CreatedAt=DateTime.Now, LivingLocation="sana'a",User=userDto}
           };

            //Individual
            individuals = new List<IndividualDto>
            {
                new IndividualDto{Id=0,Name="ali",Job="programmer",Address="sana",PhonNumber="777777777777",
                    Sex=IndividualSex.Male,SocialStatus=status,NationalId="5453535",
                    CreatedAt=DateTime.Now,DateOfBirth=DateTime.Today,User=userDto ,Family=families[0]},
                new IndividualDto{Id=1,Name="ali",Job="programmer",Address="sana",PhonNumber="777777777777",
                    Sex=IndividualSex.Male,SocialStatus=status,NationalId="5453535",
                    CreatedAt=DateTime.Now,DateOfBirth=DateTime.Today,User=userDto,Family=families[3]},
                new IndividualDto{Id=2,Name="ali",Job="programmer",Address="sana",PhonNumber="777777777777",
                    Sex=IndividualSex.Male,SocialStatus=status,NationalId="5453535",
                    CreatedAt=DateTime.Now,DateOfBirth=DateTime.Today,User=userDto, Family = families[1]},
                new IndividualDto{Id=3,Name="ali",Job="programmer",Address="sana",PhonNumber="777777777777",
                    Sex=IndividualSex.Male,SocialStatus=status,NationalId="5453535",
                    CreatedAt=DateTime.Now,DateOfBirth=DateTime.Today,User=userDto, Family = families[2]},
                new IndividualDto{Id=4,Name="ali",Job="programmer",Address="sana",PhonNumber="777777777777",
                    Sex=IndividualSex.Male,SocialStatus=status,NationalId="5453535",
                    CreatedAt=DateTime.Now,DateOfBirth=DateTime.Today,User=userDto, Family = families[0]}
            };

            familyIndividuals = new List<FamilyIndividualDto>
            {
                new FamilyIndividualDto{Id=42,Individual=individuals[0],Family=families[0],IsParent=true,IsProvider=true},
                new FamilyIndividualDto{Id=2,Individual=individuals[1],Family=families[1],IsParent=true,IsProvider=true},
                new FamilyIndividualDto{Id=54,Individual=individuals[2],Family=families[2],IsParent=true,IsProvider=true},
                new FamilyIndividualDto{Id=64,Individual=individuals[3],Family=families[3],IsParent=true,IsProvider=true},
                new FamilyIndividualDto{Id=34,Individual=individuals[4],Family=families[4],IsParent=true,IsProvider=true}
            };

            skills = new List<SkillDto>
            {
                new SkillDto{Id=43,Name="skill1",Description="skill1d1",Field="eduction",Individual=individuals[0]},
                new SkillDto{Id=445,Name="skill2",Description="skill1d1",Field="eduction",Individual=individuals[1]},
                new SkillDto{Id=425,Name="skill3",Description="skill1d1",Field="eduction",Individual=individuals[2]},
                new SkillDto{Id=63,Name="skill4",Description="skill1d1",Field="eduction",Individual=individuals[3]},
                new SkillDto{Id=25,Name="skill5",Description="skill1d1",Field="eduction",Individual=individuals[4]}
            };

            voluntaries = new List<VoluntaryDto>
            {
                new VoluntaryDto{Id=23,Name="voluntary1",Field="Aden",NameIndividual=individuals[0]},
                new VoluntaryDto{Id=3,Name="voluntary2",Field="Aden",NameIndividual=individuals[1]},
                new VoluntaryDto{Id=2533,Name="voluntary3",Field="Aden",NameIndividual=individuals[2]},
                new VoluntaryDto{Id=35,Name="voluntary4",Field="Aden",NameIndividual=individuals[3]},
                new VoluntaryDto{Id=536,Name="voluntary5",Field="Aden",NameIndividual=individuals[4]}
            };
        }

        public ActionResult Index()
        {
            return View();
        }

        // GET: FamilyController
        public ActionResult GetFamilies()
        {
            return View(families.ToList());
        }

        public ActionResult FamilyDetails(int id)
        {
            var family = families.Find(f => f.Id == id);

            return View(family!);
        }

        // GET: PopulationController/GetIndividuals
        public ActionResult GetIndividuals()
        {
            return View(individuals.ToList());
        }

        // GET: PopulationController/GetSkills/5
        public ActionResult GetSkills(int id)
        {
            var skill = skills.Where(s => s.Individual.Id == id);
            return View(skill);
        }

        // GET: PopulationController/GeetVoluntaries/5
        public ActionResult GetVoluntaries(int id)
        {
            var voluntary = voluntaries.Where(s => s.NameIndividual.Id == id);
            return View(voluntary);
        }

        // GET: PopulationController/GetSkills/5
        public ActionResult GetFamilyIndividuals(int id)
        {
            var familyindividuals = individuals.Where(s => s.Family.Id == id);
            if (familyindividuals != null)
                return View(familyindividuals);
            else return RedirectToAction(nameof(FamilyDetails), id);
        }

        public ActionResult IndividualDetails(int id)
        {
            var individual = individuals.Find(i => i.Id == id);
            return View(individual);
        }

        // GET: FamilyController/Create
        public ActionResult AddFamily()
        {
            return View();
        }

        // POST: FamilyController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddFamily(FamilyAddDto family)
        {
            family.User = userDto;

            var newfamily =
                new FamilyDto { Id = 234, Name = family.Name, LivingLocation = family.LivingLocation, User = userDto };

            families.Add(newfamily);
            if (families.Count >= 6)
                return RedirectToAction(nameof(GetFamilies), families.ToList());
            else
                return View(family);
        }

        // GET: PuplationController/Create
        public ActionResult AddIndividual(int id)
        {
            var family = families.Find(f => f.Id == id);

            var individual = new IndividualAddDto
            {
                Family = family!,
            };
            return View(individual);
        }

        // POST: FamilyController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddIndividual(IndividualAddDto individual)
        {
            if (ModelState.IsValid)
            {
                var newIndividual = new IndividualAddDto
                {
                    Id = 53453,
                    Name = individual.Name,
                    Address = individual.Address,
                    Job = individual.Job,
                    Sex = individual.Sex,
                    Family = individual.Family,
                    PhonNumber = individual.PhonNumber,
                    CreatedAt = DateTime.Now
                };
                RedirectToAction(nameof(GetIndividuals));
            }
            return View(individual);
        }

        // GET: PuplationController/Create
        public ActionResult AddSkill(int id)
        {
            var individual = individuals.Find(i => i.Id == id);
            var skill = new SkillDto
            {
                Individual = individual!
            };
            return View(skill);
        }

        // POST: FamilyController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddSkill(SkillDto skill)
        {
            if (ModelState.IsValid)
            {
                var newskill = new SkillDto
                {
                    Id = 53453,
                    Name = skill.Name,
                    Description = skill.Description,
                    Field = skill.Field,
                    Individual = skill.Individual,
                    CreatedAt = DateTime.Now
                };
                skills.Add(newskill);
                RedirectToAction(nameof(GetSkills), skill.Individual.Id);
            }
            return View(skill);
        }

        // GET: PuplationController/Create
        public ActionResult AddVoluntary(int id)
        {
            var individual = individuals.Find(i => i.Id == id);
            var skill = new SkillDto
            {
                Individual = individual!
            };
            return View(skill);
        }

        // POST: FamilyController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddVoluntary(VoluntaryDto voluntary)
        {
            if (ModelState.IsValid)
            {
                var newVoluntary = new VoluntaryDto
                {
                    Id = 53453,
                    Name = voluntary.Name,
                    Field = voluntary.Field,
                    NameIndividual = voluntary.NameIndividual,
                    CreatedAt = DateTime.Now
                };
                voluntaries.Add(newVoluntary);
                RedirectToAction(nameof(GetVoluntaries), voluntary.NameIndividual.Id);
            }
            return View(voluntary);
        }

        // GET: FamilyController/Edit/5
        public ActionResult UpdateFamily(int id)
        {
            var family = families.Find(f => f.Id == id);

            return View("Editfamily", family);
        }

        // POST: FamilyController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateFamily(int id, FamilyDto family)
        {
            if (family != null)
            {
                var oldfamily = families.Find(f => f.Id == id);
                if (oldfamily != null)
                {
                    oldfamily.Name = family.Name;
                    oldfamily.LivingLocation = family.LivingLocation;
                }
                return RedirectToAction(nameof(GetFamilies));
            }
            return View("Editfamily", family);
        }

        public ActionResult UpdateIndividual(int id)
        {
            var individual = individuals.Find(i => i.Id == id);

            return View("EditIndividual", individual);
        }

        // POST: FamilyController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateIndividual(int id, IndividualDto individual)
        {
            if (ModelState.IsValid)
            {
                var oldIndividual = individuals.Find(f => f.Id == id);
                if (oldIndividual != null)
                {
                    oldIndividual.Name = individual.Name;
                    oldIndividual.Job = individual.Job;
                    oldIndividual.Address = individual.Address;
                    oldIndividual.Family = individual.Family;
                }
                return RedirectToAction(nameof(GetFamilies));
            }
            return View("EditIndividual", individual);
        }

        public ActionResult UpdateSkill(int id)
        {
            var skill = skills.Find(s => s.Id == id);

            return View("EditSkill", skill);
        }

        // POST: FamilyController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateSkill(int id, SkillDto newskill)
        {
            if (ModelState.IsValid)
            {
                var oldskill = skills.Find(s => s.Id == id);
                if (oldskill != null)
                {
                    oldskill.Name = newskill.Name;
                    oldskill.Description = newskill.Description;
                    oldskill.Field = newskill.Field;
                    oldskill.ModifiedAt = DateTime.Now;
                    return RedirectToAction(nameof(GetSkills), oldskill!.Individual.Id);
                }
            }
            return View("EditSkill", newskill);
        }

        public ActionResult UpdateVoluntary(int id)
        {
            var voluntary = voluntaries.Find(v => v.Id == id);

            return View("EditVoluntary", voluntary);
        }

        // POST: FamilyController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateVoluntary(int id, VoluntaryDto newvoluntary)
        {
            if (ModelState.IsValid)
            {
                var oldVoluntary = voluntaries.Find(v => v.Id == id);
                if (oldVoluntary != null)
                {
                    oldVoluntary.Name = newvoluntary.Name;
                    oldVoluntary.Field = newvoluntary.Field;
                    oldVoluntary.ModifiedAt = DateTime.Now;
                }
                return RedirectToAction(nameof(GetVoluntaries), newvoluntary.NameIndividual.Id);
            }
            return View("EditVoluntary", newvoluntary);
        }

        public ActionResult UpdateFamilyIndividuals(int id)
        {
            var familyindividual = familyIndividuals.Find(f => f.Id == id);

            return View("EditeFamilyIndividual", familyindividual);
        }

        // POST: FamilyController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateFamilyIndividuals(int id, FamilyIndividualDto newfamilyIndividual)
        {
            if (ModelState.IsValid)
            {
                var oldFamilyIndividual = familyIndividuals.Find(f => f.Id == id);
                if (oldFamilyIndividual != null)
                {
                    oldFamilyIndividual.IsParent = newfamilyIndividual.IsParent;
                    oldFamilyIndividual.IsProvider = newfamilyIndividual.IsProvider;
                    oldFamilyIndividual.ModifiedAt = DateTime.Now;
                }
                return RedirectToAction(nameof(GetFamilyIndividuals), newfamilyIndividual.Family.Id);
            }
            return View("EditFamilyIndividual", newfamilyIndividual);
        }

        // GET: FamilyController/Delete/5
        public ActionResult DeleteFamily(int id)
        {
            return RedirectToAction(nameof(GetFamilies));
        }

        // POST: FamilyController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteFamily(int id, FamilyDto familyDto)
        {
            var family = families.Find(f => f.Id == id);
            if (family != null)
            {
                families.Remove(familyDto);
            }
            return View(nameof(GetFamilies));
        }

        public ActionResult DeleteIndividual(int id)
        {
            return View(nameof(GetIndividuals));
        }

        // POST: FamilyController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteIndividual(int id, IndividualDto individualDto)
        {
            var individual = individuals.Find(f => f.Id == id);
            if (individual != null)
            {
                individuals.Remove(individual);
                return RedirectToAction(nameof(GetIndividuals));
            }
            return View(nameof(GetIndividuals));
        }

        // GET: FamilyController/Delete/5
        public ActionResult DeleteSkill(int id)
        {
            var individualId = skills.Select(s => s.Individual.Id);
            return View(nameof(GetSkills), individualId);
        }

        // POST: FamilyController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteSkill(int id, SkillDto skilldto)
        {
            var skill = skills.Find(s => s.Id == skilldto.Id);
            if (skilldto != null)
            {
                skills.Remove(skilldto);
                return RedirectToAction(nameof(GetSkills), skill!.Individual.Id);
            }
            return View(nameof(GetSkills));
        }

        // GET: FamilyController/Delete/5
        public ActionResult DeleteVoluntary(int id)
        {
            return View();
        }

        // POST: FamilyController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteVoluntary(int id, VoluntaryDto voluntaryDto)
        {
            var voluntary = voluntaries.Find(v => v.Id == id);
            if (voluntary != null)
            {
                voluntaries.Remove(voluntaryDto);
            }
            return RedirectToAction(nameof(GetVoluntaries), voluntaryDto.NameIndividual.Id);
        }

        // GET: FamilyController/Delete/5
        public ActionResult DeleteFamilyIndividual(int id)
        {
            return View();
        }

        // POST: FamilyController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteFamilyIndividual(int id, FamilyIndividualDto familyIndividualDto)
        {
            var familyIndividual = familyIndividuals.Find(f => f.Id == id);
            if (familyIndividual != null)
            {
                familyIndividuals.Remove(familyIndividual);
            }
            return RedirectToAction(nameof(GetFamilyIndividuals), familyIndividualDto.Family.Id);
        }
    }
}