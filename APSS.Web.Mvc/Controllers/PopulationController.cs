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
        private UserDto _userDto = new UserDto { Name = "yemen", AccessLevel = AccessLevel.Root, UserStatus = UserStatus.Active, SupervisedBy = null };
        private List<FamilyDto> families = null!;
        private List<IndividualDto> individuals = null!;

        public PopulationController(IPopulationService populationSvc)
        {
            _populationSvc = populationSvc;

            //families List
            families = new List<FamilyDto>
           {
               new FamilyDto{Id=0,Name="ali",CreatedAt=DateTime.Now, LivingLocation="sana'a",User=_userDto},
               new FamilyDto{Id=1,Name="ali",CreatedAt=DateTime.Now, LivingLocation="sana'a",User=_userDto},
               new FamilyDto{Id=2,Name="ali",CreatedAt=DateTime.Now, LivingLocation="sana'a",User=_userDto},
               new FamilyDto{Id=3,Name="ali",CreatedAt=DateTime.Now, LivingLocation="sana'a",User=_userDto},
               new FamilyDto{Id=4,Name="ali",CreatedAt=DateTime.Now, LivingLocation="sana'a",User=_userDto}
           };

            //Individual
            individuals = new List<IndividualDto>
            {
                new IndividualDto{Id=0,Name="ali",Job="programmer",Address="sana",PhonNumber="777777777777",
                    Sex=IndividualSex.Male,SocialStatus=SocialStatus.Unmarried,NationalId="5453535",
                    CreatedAt=DateTime.Now,DateOfBirth=DateTime.Today,User=_userDto},
                new IndividualDto{Id=1,Name="ali",Job="programmer",Address="sana",PhonNumber="777777777777",
                    Sex=IndividualSex.Male,SocialStatus=SocialStatus.Unmarried,NationalId="5453535",
                    CreatedAt=DateTime.Now,DateOfBirth=DateTime.Today,User=_userDto},
                new IndividualDto{Id=2,Name="ali",Job="programmer",Address="sana",PhonNumber="777777777777",
                    Sex=IndividualSex.Male,SocialStatus=SocialStatus.Unmarried,NationalId="5453535",
                    CreatedAt=DateTime.Now,DateOfBirth=DateTime.Today,User=_userDto},
                new IndividualDto{Id=3,Name="ali",Job="programmer",Address="sana",PhonNumber="777777777777",
                    Sex=IndividualSex.Male,SocialStatus=SocialStatus.Unmarried,NationalId="5453535",
                    CreatedAt=DateTime.Now,DateOfBirth=DateTime.Today,User=_userDto},
                new IndividualDto{Id=4,Name="ali",Job="programmer",Address="sana",PhonNumber="777777777777",
                    Sex=IndividualSex.Male,SocialStatus=SocialStatus.Unmarried,NationalId="5453535",
                    CreatedAt=DateTime.Now,DateOfBirth=DateTime.Today,User=_userDto},
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

        // GET: FamilyController/Details/5
        public ActionResult GetIndividuals()
        {
            return View(individuals.ToList());
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
        public ActionResult AddFamily(FamilyDto family)
        {
            if (ModelState.IsValid)
            {
                var newfamily = new FamilyDto
                {
                    Id = 434,
                    Name = family.Name,
                    LivingLocation = family.LivingLocation,
                    User = _userDto,
                    CreatedAt = DateTime.Now
                };
                families.Add(family);
                RedirectToAction(nameof(GetFamilies), families);
            }
            return View(family!);
        }

        // GET: PuplationController/Create
        public ActionResult AddIndividual(int id)
        {
            var family = families.Find(f => f.Id == id);
            var individual = new IndividualDto
            {
                Family = family!
            };
            return View(individual);
        }

        // POST: FamilyController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddIndividual(IndividualDto individual)
        {
            if (ModelState.IsValid)
            {
                var newIndividual = new IndividualDto
                {
                    Id = 53453,
                    Name = individual.Name,
                    Address = individual.Address,
                    Job = individual.Job,
                    DateOfBirth = individual.DateOfBirth,
                    Sex = individual.Sex,
                    SocialStatus = individual.SocialStatus,
                    Family = individual.Family,
                    NationalId = individual.NationalId,
                    PhonNumber = individual.PhonNumber,
                    CreatedAt = DateTime.Now
                };
                individuals.Add(newIndividual);
                RedirectToAction(nameof(GetIndividuals));
            }
            return View(individual!);
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

        // GET: FamilyController/Delete/5
        public ActionResult DeleteFamily(int id)
        {
            return View(nameof(GetFamilies));
        }

        // POST: FamilyController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteFamily(int id, FamilyDto familyDto)
        {
            var family = families.Find(f => f.Id == id);
            if (family != null)
            {
                families.Remove(family);
                return RedirectToAction(nameof(GetFamilies));
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
    }
}