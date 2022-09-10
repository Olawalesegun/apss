using Microsoft.AspNetCore.Mvc;
using APSS.Web.Dtos;
using APSS.Domain.Services;
using APSS.Web.Dtos.ValueTypes;
using AutoMapper;
using APSS.Domain.Entities;
using APSS.Domain.Repositories;
using APSS.Web.Mvc.Auth;

namespace APSS.Web.Mvc.Areas.Controllers
{
    [Area(Areas.Animals)]
    public class ConfirmationsController : Controller
    {
        private readonly IAnimalService _confirm;
        private readonly IMapper _mapper;

        public ConfirmationsController(IAnimalService confirm, IMapper mapper)
        {
            _confirm = confirm;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(long id)
        {
            var animal = await (await _confirm.GetAllAnimalGroupsAsync(User.GetAccountId(), 8)).Where(c => c.IsConfirmed == null).Include(f => f.OwnedBy).Include(a => a.OwnedBy.Accounts).AsAsyncEnumerable().ToListAsync();
            var animalProduct = await (await _confirm.GetAllAnimalProductsAsync(User.GetAccountId(), 8)).Where(c => c.IsConfirmed == null).Include(f => f.AddedBy).Include(u => u.Unit).Include(p => p.Producer.OwnedBy).AsAsyncEnumerable().ToListAsync();
            var animalDto = new List<AnimalGroupConfirmDto>();
            foreach (var a in animal)
            {
                animalDto.Add(new AnimalGroupConfirmDto
                {
                    Type = a.Type,
                    Id = a.Id,
                    Name = a.Name,
                    Quantity = a.Quantity,
                    CreatedAt = a.CreatedAt,
                    ModifiedAt = a.ModifiedAt,
                    Sex = a.Sex,
                    UserName = a.OwnedBy.Name,
                    UserID = a.OwnedBy.Id,
                });
            }

            var product = new List<AnimalProductDetailsDto>();
            foreach (var products in animalProduct)
            {
                product.Add(new AnimalProductDetailsDto
                {
                    Id = products.Id,
                    Name = products.Name,
                    Quantity = products.Quantity,
                    CreatedAt = products.CreatedAt,
                    ModifiedAt = products.ModifiedAt,
                    PeriodTaken = products.PeriodTaken,
                    SingleUnit = products.Unit,
                    Producer = products.Producer,
                    Ownerby = products.Producer.OwnedBy.Name,
                });
            }
            var confirm = new ConfirmationDto();

            confirm.AnimalProducts = product;
            confirm.AnimalGroups = animalDto;

            return View(confirm);
        }

        public async Task<IActionResult> ConfirmAnimalGroup(long id)
        {
            var animal = await (await _confirm.GetAllAnimalGroupsAsync(User.GetAccountId(), id)).Where(u => u.IsConfirmed == null).Include(o => o.OwnedBy).AsAsyncEnumerable().ToListAsync();
            if (animal == null) return NotFound();
            var animalDto = new List<AnimalGroupDto>();
            foreach (var single in animal)
            {
                animalDto.Add(new AnimalGroupDto
                {
                    Id = single.Id,
                    Name = single.Name,
                    Type = single.Type,
                    Quantity = single.Quantity,
                    Sex = single.Sex,
                    CreatedAt = single.CreatedAt,
                    ModifiedAt = single.ModifiedAt,
                    IsConfirmed = single.IsConfirmed,
                    OwnedBy = _mapper.Map<UserDto>(single.OwnedBy),
                });
            }
            return View(animalDto);
        }

        public async Task<IActionResult> ConfirmAnimalProduct(long id)
        {
            var animalProduct = await (await _confirm.GetAllAnimalProductsAsync(User.GetAccountId(), id)).Where(c => c.IsConfirmed == null).Include(f => f.AddedBy).Include(u => u.Unit).Include(p => p.Producer.OwnedBy).AsAsyncEnumerable().ToListAsync();
            var product = new List<AnimalProductDetailsDto>();
            foreach (var products in animalProduct)
            {
                product.Add(new AnimalProductDetailsDto
                {
                    Id = products.Id,
                    Name = products.Name,
                    Quantity = products.Quantity,
                    CreatedAt = products.CreatedAt,
                    ModifiedAt = products.ModifiedAt,
                    PeriodTaken = products.PeriodTaken,
                    SingleUnit = products.Unit,
                    Producer = products.Producer,
                    Ownerby = products.Producer.OwnedBy.Name,
                });
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ConfirmAnimal(long id, bool value)
        {
            var animal = await _confirm.ConfirmAnimalGroup(User.GetAccountId(), id, value);

            TempData["Action"] = "Add Erea";
            TempData["success"] = $"{id } null";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ConfirmProduct(long id, bool value)
        {
            var confrom = await _confirm.ConfirmAnimalProduct(User.GetAccountId(), id, value);
            return RedirectToAction("Index");
        }
    }
}