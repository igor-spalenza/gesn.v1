using gesn.webApp.Interfaces.Services.Offer;
using gesn.webApp.Models.ViewModels.Offer;
using Microsoft.AspNetCore.Mvc;

namespace gesn.webApp.Controllers
{
    public class OfferHierarchyController : Controller
    {
        private readonly IOfferHierarchyService _service;

        public OfferHierarchyController(IOfferHierarchyService service)
        {
            this._service = service;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<OfferHierarchySummaryViewModel> hierarchies = await this._service.GetAllAsync();
            return View(hierarchies);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OfferHierarchyInsertViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["OpenCreatePanel"] = true;
                return View("Index", await _service.GetAllAsync());
            }

            await _service.AddAsync(model);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(OfferHierarchyUpdateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["OpenEditPanel"] = model.Id;
                return View("Index", await _service.GetAllAsync());
            }

            var success = await _service.UpdateAsync(model);

            if (success)
            {
                TempData["Success"] = "Hierarquia atualizada com sucesso!";
                return RedirectToAction(nameof(Index));
            }

            TempData["Error"] = "Erro ao atualizar a hierarquia no banco de dados.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var viewModel = await _service.GetForUpdateAsync(id);

            if (viewModel == null)
            {
                TempData["Error"] = "Hierarquia não encontrada.";
                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _service.DeleteAsync(id);

            if (success)
                TempData["Success"] = "Hierarquia removida com sucesso!";
            else
                TempData["Error"] = "Erro ao tentar remover a hierarquia.";

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> GetDetailsPartial(Guid id)
        {
            var viewModel = await _service.GetAsync(id);

            if (viewModel == null)
            {
                TempData["Error"] = "Hierarquia não encontrada.";
                return RedirectToAction(nameof(Index));
            }

            return PartialView("_DetailsPartial", viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetEditPartial(Guid id)
        {
            var viewModel = await _service.GetForUpdateAsync(id);
            if (viewModel == null)
                return NotFound();
            return PartialView("_EditPartial", viewModel);
        }
    }
}
