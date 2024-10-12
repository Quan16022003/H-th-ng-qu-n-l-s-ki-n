using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Persistence;
using Services.Abtractions;
using Constracts;
using Mapster;
using Web.Areas.Admin.Models.Owners;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OwnersController : Controller
    {
        private readonly IServiceManager _serviceManager;

        public OwnersController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        // GET: Owners
        public async Task<IActionResult> Index()
        {
            var owners = await _serviceManager.OwnerService.GetAllAsync();
            IEnumerable<OwnerListViewModel> ownerListViewModel = owners.Adapt<IEnumerable<OwnerListViewModel>>();
            return View(ownerListViewModel);
        }

        // GET: Owners/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var owner = await _serviceManager.OwnerService.GetByIdAsync((Guid)id);

            if (owner == null)
            {
                return NotFound();
            }

            OwnerDetailViewModel viewModel = owner.Adapt<OwnerDetailViewModel>();

            return View(viewModel);
        }

        // GET: Owners/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OwnerForCreationInputModel inputModel)
        {
            if (ModelState.IsValid)
            {
                OwnerForCreationDto dto = inputModel.Adapt<OwnerForCreationDto>();
                await _serviceManager.OwnerService.CreateAsync(dto);

                return RedirectToAction(nameof(Index));
            }
            return View(inputModel);
        }

        // GET: Owners/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            OwnerDto ownerDto = await _serviceManager.OwnerService.GetByIdAsync((Guid)id);
            if (ownerDto == null)
            {
                return NotFound();
            }
            var _id = ownerDto.Id;
            OwnerForUpdateInputModel ownerInput = ownerDto.Adapt<OwnerForUpdateInputModel>();
            return View(ownerInput);
        }

        // POST: Owners/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, OwnerForUpdateInputModel ownerInput)
        {
            if (id != ownerInput.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    OwnerForUpdateDto dto = ownerInput.Adapt<OwnerForUpdateDto>();
                    await _serviceManager.OwnerService.UpdateAsync(id, dto);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await _serviceManager.OwnerService.isExist(id) is false)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(ownerInput);
        }

        // GET: Owners/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var owner = await _serviceManager.OwnerService.GetByIdAsync((Guid)id);
            if (owner == null)
            {
                return NotFound();
            }

            return View(owner);
        }

        // POST: Owners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var owner = await _serviceManager.OwnerService.GetByIdAsync(id);
            if (owner != null)
            {
                await _serviceManager.OwnerService.DeleteAsync(id);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
