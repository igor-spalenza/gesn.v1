using gesn.webApp.Interfaces.Services.Offer;
using gesn.webApp.Models.ViewModels.Offer.Category;
using Microsoft.AspNetCore.Mvc;

namespace gesn.webApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryServices _categoryService;
        public CategoryController(ICategoryServices categoryService)
        {
            this._categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<CategorySummaryViewModel> categories = await this._categoryService.GetAllAsync();
            return View(categories);
        }

        public IActionResult Create() =>
            View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryInsertViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _categoryService.AddAsync(model);

            TempData["Success"] = "Categoria criada com sucesso!";

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CategoryUpdateViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var success = await _categoryService.UpdateAsync(model);

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
            var viewModel = await _categoryService.GetForUpdateAsync(id);

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
            var success = await _categoryService.DeleteAsync(id);

            if (success)
                TempData["Success"] = "Categoria removida com sucesso!";
            else
                TempData["Error"] = "Erro ao tentar remover a categoria.";

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var viewModel = await _categoryService.GetAsync(id);

            if (viewModel == null)
            {
                TempData["Error"] = "Categoria não encontrada.";
                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }
    }
}