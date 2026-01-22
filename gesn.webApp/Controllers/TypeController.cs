using gesn.webApp.Interfaces.Services.Global;
using gesn.webApp.Models.ViewModels.Global;
using Microsoft.AspNetCore.Mvc;

namespace gesn.webApp.Controllers
{
    public class TypeController : Controller
    {
        private readonly ITypeService _typeService;
        public TypeController(ITypeService typeService)
        {
            this._typeService = typeService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<TypeSummaryViewModel> categories = await this._typeService.GetAllAsync();
            return View(categories);
        }

        public IActionResult Create() =>
            View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TypeInsertViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["OpenCreatePanel"] = true;
                return View("Index", await _typeService.GetAllAsync()); // Retorna para a lista
            }

            await _typeService.AddAsync(model);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TypeUpdateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["OpenEditPanel"] = model.Id;
                return View("Index", await _typeService.GetAllAsync());
            }

            var success = await _typeService.UpdateAsync(model);

            if (success)
            {
                TempData["Success"] = "Categoria atualizada com sucesso!";
                return RedirectToAction(nameof(Index));
            }

            TempData["Error"] = "Erro ao atualizar a categoria no banco de dados.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var viewModel = await _typeService.GetForUpdateAsync(id);

            if (viewModel == null)
            {
                TempData["Error"] = "Tipo não encontrado.";
                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _typeService.DeleteAsync(id);

            if (success)
                TempData["Success"] = "Categoria removida com sucesso!";
            else
                TempData["Error"] = "Erro ao tentar remover a categoria.";

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> GetDetailsPartial(Guid id)
        {
            var viewModel = await _typeService.GetAsync(id);

            if (viewModel == null)
            {
                TempData["Error"] = "Categoria não encontrada.";
                return RedirectToAction(nameof(Index));
            }

            return PartialView("_DetailsPartial", viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetEditPartial(Guid id)
        {
            var viewModel = await _typeService.GetForUpdateAsync(id);
            if (viewModel == null)
                return NotFound();
            return PartialView("_EditPartial", viewModel);
        }
    }
}