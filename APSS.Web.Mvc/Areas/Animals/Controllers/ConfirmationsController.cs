using Microsoft.AspNetCore.Mvc;
using APSS.Web.Dtos;
using APSS.Domain.Services;
using APSS.Web.Dtos.ValueTypes;

namespace APSS.Web.Mvc.Areas.Controllers
{
    [Area(Areas.Animals)]
    public class ConfirmationsController : Controller
    {
        private readonly IAnimalService _confirm;

        public ConfirmationsController(IAnimalService confirm)
        {
            _confirm = confirm;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> ConfirmAnimalGroup(long id)
        {
            try
            {
                var animal = await (await _confirm.GetAnimalGroupAsync(3, id)).AsAsyncEnumerable().ToListAsync();
                var single = animal.FirstOrDefault();
                if (animal == null) return NotFound();
                var animalDto = new AnimalGroupDto
                {
                    Id = single!.Id,
                    Name = single.Name,
                    Type = single.Type,
                    Quantity = single!.Quantity,
                    Sex = (SexDto)single!.Sex,
                    CreatedAt = single!.CreatedAt,
                    ModifiedAt = single!.ModifiedAt,
                    IsConfirmed = single!.IsConfirmed,
                };
                return View(animalDto);
            }
            catch (Exception) { }
            return View();
        }

        public async Task<IActionResult> ConfirmAnimal(long id, bool value)
        {
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ConfirmAnimalProduct(long id)
        {
            return View();
        }

        public async Task<IActionResult> ConfirmProduct(long id, bool value)
        {
            return RedirectToAction("Index");
        }
    }
}