using Microsoft.AspNetCore.Mvc;
using APSS.Domain.Entities;
using APSS.Domain.Services;
using APSS.Web.Mvc.Auth;
using AutoMapper;
using APSS.Web.Dtos;
using APSS.Domain.ValueTypes;

namespace APSS.Web.Mvc.Areas.Lands.Controllers
{
    [Area(Areas.Lands)]
    public class LandsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILandService _landSvc;
        private readonly List<LandDto> _landList;
        private readonly AccountDto _account;

        public LandsController(ILandService landService, IMapper mapper)
        {
            _mapper = mapper;
            _landSvc = landService;
            //_account.PermissionType = User.GetPermissions();
            _account.Id = User.GetId();
            //_account.UserId = User.Get
            _account.User.AccessLevel = User.GetAccessLevel();
        }

        public async Task<IActionResult> Index()
        {
            var landList = await _landSvc.GetLandsAsync(_account.Id, _account.User.Id).ToAsyncEnumerable().ToListAsync();
            foreach (var land in landList)
            {
                var item = _mapper.Map<LandDto>(land);
                _landList.Add(new LandDto
                {
                    Name = item.Name,
                    Address = item.Address,
                    Area = item.Area,
                    Latitude = item.Latitude,
                    Longitude = item.Longitude,
                    Id = item.Id,
                    IsConfirmed = item.IsConfirmed,
                    IsUsable = item.IsUsable,
                    IsUsed = item.IsUsed,
                    OwnedBy = item.OwnedBy
                });
            }
            //var LandList = new List<LandDto>
            //    {
            //        new LandDto{Name ="land1",Id=1,Address="djskhao", Area =123},
            //        new LandDto{Name ="land2",Id=2,Address="djskhao2", Area =321},
            //        new LandDto{Name ="land3",Id=3,Address="djskhao3", Area =555},
            //        new LandDto{Name ="land4",Id=4,Address="djskhao3", Area =555},
            //    };

            return View(landList);
        }

        // GET: LandController/Add a new land
        [ApssAuthorized(AccessLevel.Farmer, PermissionType.Create)]
        public ActionResult Add()
        {
            var land = new LandDto();
            return View(land);
        }

        // POST: LandController/Add a new land
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ApssAuthorized(AccessLevel.Farmer, PermissionType.Create)]
        public ActionResult Add(LandDto newLand)
        {
            if (!ModelState.IsValid || newLand == null)
            {
                return View(newLand);
            }
            Coordinates coordinates = new Coordinates(newLand.Latitude, newLand.Longitude);
            _landSvc.AddLandAsync(_account.Id,
                newLand.Area,
                coordinates,
                newLand.Address,
                newLand.Name,
                newLand.IsUsable,
                newLand.IsUsed);

            return RedirectToAction(nameof(Index));
        }

        [ApssAuthorized(AccessLevel.Root | AccessLevel.Presedint | AccessLevel.Directorate | AccessLevel.District
                       | AccessLevel.Village | AccessLevel.Governorate | AccessLevel.Group | AccessLevel.Farmer, PermissionType.Read)]
        public async Task<ActionResult> Details(long Id)
        {
            //var LandList = new List<LandDto>
            //    {
            //        new LandDto{Name ="land1",Id=1,Address="djskhao", Area =123},
            //        new LandDto{Name ="land2",Id=2,Address="djskhao2", Area =321},
            //        new LandDto{Name ="land3",Id=3,Address="djskhao3", Area =555},
            //        new LandDto{Name ="land4",Id=4,Address="djskhao3", Area =555},
            //    };

            //LandDto land = LandList.Where(i => i.Id == Id).First();
            //land.OwnedBy = new UserDto
            //{
            //    Name = "Farouq"
            //};
            if (Id <= 0)
            {
            }
            var land = await _landSvc.GetLandAsync(_account.Id, Id);
            var landDto = _mapper.Map<LandDto>(land);

            return View(landDto);
        }

        // GET: LandController/Update land
        [ApssAuthorized(AccessLevel.Farmer, PermissionType.Update)]
        public async Task<ActionResult> Update(long Id)
        {
            //var LandList = new List<LandDto>
            //    {
            //        new LandDto{Name ="land1",Id=1,Address="djskhao", Area =123},
            //        new LandDto{Name ="land2",Id=2,Address="djskhao2", Area =321},
            //        new LandDto{Name ="land3",Id=3,Address="djskhao3", Area =555},
            //        new LandDto{Name ="land4",Id=4,Address="djskhao3", Area =555},
            //    };

            //LandDto land = LandList.Where(i => i.Id == Id).First();
            //land.OwnedBy = new UserDto
            //{
            //    Name = "Farouq"
            //};
            if (Id <= 0)
            {
            }
            var land = await _landSvc.GetLandAsync(_account.Id, Id);
            var landDto = _mapper.Map<LandDto>(land);

            return View(landDto);
        }

        // POST: LandController/Update land
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ApssAuthorized(AccessLevel.Farmer, PermissionType.Update)]
        public async Task<ActionResult> Update(LandDto landDto)
        {
            if (!ModelState.IsValid || landDto == null)
            {
            }
            await _landSvc.UpdateLandAsync(
                _account.Id,
                landDto!.Id,
                l =>
                {
                    l.Name = landDto.Name;
                    l.Longitude = landDto.Longitude;
                    l.IsUsed = landDto.IsUsed;
                    l.Latitude = landDto.Latitude;
                    l.Area = landDto.Area;
                    l.Address = landDto.Address;
                    l.IsUsable = landDto.IsUsable;
                });

            return RedirectToAction(nameof(Index));
        }

        // GET: LandController/Delete land
        [ApssAuthorized(AccessLevel.Farmer, PermissionType.Delete)]
        public async Task<ActionResult> Delete(long Id)
        {
            //var LandList = new List<LandDto>
            //    {
            //        new LandDto{Name ="land1",Id=1,Address="djskhao", Area =123},
            //        new LandDto{Name ="land2",Id=2,Address="djskhao2", Area =321},
            //        new LandDto{Name ="land3",Id=3,Address="djskhao3", Area =555},
            //        new LandDto{Name ="land4",Id=4,Address="djskhao4", Area =555},
            //    };

            //LandDto land = LandList.Where(i => i.Id == Id).First();
            //land.OwnedBy = new UserDto
            //{
            //    Name = "Farouq"
            //};

            //return View(land);
            if (Id <= 0)
            {
            }
            var land = await _landSvc.GetLandAsync(_account.Id, Id);
            var landDto = _mapper.Map<LandDto>(land);

            return View(landDto);
        }

        // POST: LandController/Delete land
        [HttpPost]
        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeletePost(long Id)
        {
            if (Id <= 0)
            {
            }
            await _landSvc.RemoveLandAsync(_account.Id, Id);

            return RedirectToAction(nameof(Index));
        }

        // GET: LandController/Get land
        [ApssAuthorized(AccessLevel.Farmer, PermissionType.Read)]
        public async Task<ActionResult> GetLand(long Id)
        {
            //var LandList = new List<LandDto>
            //    {
            //        new LandDto{Name ="land1",Id=1,Address="djskhao", Area =123},
            //        new LandDto{Name ="land2",Id=2,Address="djskhao2", Area =321},
            //        new LandDto{Name ="land3",Id=3,Address="djskhao3", Area =555},
            //    };

            //LandDto land = LandList.Where(i => i.Id == Id).First();
            //return View(land);
            var land = await _landSvc.GetLandAsync(_account.Id, Id);
            var landDto = _mapper.Map<LandDto>(land);

            return View(landDto);
        }

        // GET: LandController/Get lands
        public ActionResult GetLands(long userId)
        {
            return View();
        }
    }
}