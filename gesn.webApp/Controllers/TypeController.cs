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
                return View(model);

            await _typeService.AddAsync(model);

            TempData["Success"] = "Categoria criada com sucesso!";

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TypeUpdateViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var success = await _typeService.UpdateAsync(model);

            if (success)
            {
                TempData["Success"] = "Categoria atualizada!";
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Erro ao atualizar.");
            return View(model);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var viewModel = await _typeService.GetForUpdateAsync(id);

            if (viewModel == null)
            {
                TempData["Error"] = "Categoria não encontrada.";
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
        public async Task<IActionResult> Details(Guid id)
        {
            var viewModel = await _typeService.GetAsync(id);

            if (viewModel == null)
            {
                TempData["Error"] = "Categoria não encontrada.";
                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }
    }
}