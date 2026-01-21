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
            {
                TempData["OpenCreatePanel"] = true;
                return View("Index", await _categoryService.GetAllAsync()); // Retorna para a lista
            }

            await _categoryService.AddAsync(model);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CategoryUpdateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["OpenEditPanel"] = model.Id;
                return View("Index", await _categoryService.GetAllAsync());
            }

            var success = await _categoryService.UpdateAsync(model);

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
        public async Task<IActionResult> GetDetailsPartial(Guid id)
        {
            var viewModel = await _categoryService.GetAsync(id);

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
            var viewModel = await _categoryService.GetForUpdateAsync(id);
            if (viewModel == null)
                return NotFound();
            return PartialView("_EditPartial", viewModel);
        }
    }
}