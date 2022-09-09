﻿using Microsoft.AspNetCore.Mvc;
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

        public ConfirmationsController(IAnimalService confirm, IMapper mapper)
        {
            _confirm = confirm;
        }

        public async Task<IActionResult> Index(long id)
        {
            var animal = await (await _confirm.GetAllAnimalGroupsAsync(User.GetAccountId(), 8)).Where(c => c.IsConfirmed == null | c.IsConfirmed == false).Include(f => f.OwnedBy).Include(a => a.OwnedBy.Accounts).AsAsyncEnumerable().ToListAsync();
            var animalProduct = await (await _confirm.GetAllAnimalProductsAsync(User.GetAccountId(), 8)).Where(c => c.IsConfirmed == null | c.IsConfirmed == false).Include(f => f.AddedBy).Include(u => u.Unit).Include(p => p.Producer.OwnedBy).AsAsyncEnumerable().ToListAsync();
            var animalDto = new List<AnimalGroupConfirmDto>();
            var v = animal.FirstOrDefault();
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
            try
            {
                var animal = await (await _confirm.GetAnimalGroupAsync(User.GetAccountId(), id)).AsAsyncEnumerable().ToListAsync();
                var single = animal.FirstOrDefault();
                if (animal == null) return NotFound();
                var animalDto = new AnimalGroupDto
                {
                    Id = single!.Id,
                    Name = single.Name,
                    Type = single.Type,
                    Quantity = single!.Quantity,
                    Sex = single!.Sex,
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
            try
            {
                if (value)
                {
                    var animal = await _confirm.ConfirmAnimalGroup(User.GetAccountId(), id, value);
                    if (animal == null) return NotFound();
                }
            }
            catch (Exception) { }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> AnimalConfirm(long id, bool value)
        {
            var animal = await _confirm.ConfirmAnimalGroup(User.GetAccountId(), id, value);

            TempData["Action"] = "Add Erea";
            TempData["success"] = $"{id } null";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ConfirmAnimalProduct(long id)
        {
            return View();
        }

        public async Task<IActionResult> ConfirmProduct(long id, bool value)
        {
            var confrom = await _confirm.ConfirmAnimalProduct(User.GetAccountId(), id, value);
            return RedirectToAction("Index");
        }
    }
}