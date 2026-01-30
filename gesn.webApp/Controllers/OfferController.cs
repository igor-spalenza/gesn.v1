using gesn.webApp.Infrastructure.Repositories.Templates.Offer;
using gesn.webApp.Interfaces.Services.Global;
using gesn.webApp.Interfaces.Services.Offer;
using gesn.webApp.Models.ViewModels.Offer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;

namespace gesn.webApp.Controllers
{
    public class OfferController : Controller
    {
        private readonly IOfferService _offerService;
        private readonly ICategoryServices _categoryServices;
        private readonly IStringLocalizer<OfferInsertViewModel> _insertLocalizer;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        public OfferController(IOfferService offerService, IStringLocalizer<OfferInsertViewModel> insertLocalizer, IStringLocalizer<SharedResource> sharedLocalizer, ICategoryServices categoryServices)
        {
            this._offerService = offerService;
            this._insertLocalizer = insertLocalizer;
            this._sharedLocalizer = sharedLocalizer;
            this._categoryServices = categoryServices;
        }

        public async Task<IActionResult> Index()
        {
            OfferInsertViewModel model = new OfferInsertViewModel
            {
                Categories = await GetCategorySelectList()
            };

            ViewBag.InsertModel = model;

            IEnumerable<OfferSummaryViewModel> offers = await this._offerService.ReadAsync(OfferTemplates.SummaryTemplate);
            return View(offers);
        }

        public async Task<IActionResult> Create() =>
            View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OfferInsertViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model = new OfferInsertViewModel
                {
                    Categories = await GetCategorySelectList()
                };

                ViewBag.InsertModel = model;

                TempData["OpenCreatePanel"] = true;
                TempData["Error"] = string.Format(this._sharedLocalizer["Insert_Offer_Error"].Value, model.Name);
                return View("Index", await this._offerService.ReadAsync(OfferTemplates.SummaryTemplate)); // Retorna para a lista
            }

            await _offerService.AddAsync(model);
            TempData["Success"] = string.Format(this._sharedLocalizer["Insert_Offer_Success"].Value, model.Name);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(OfferUpdateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["OpenEditPanel"] = model.Id;
                return View("Index", await this._offerService.ReadAsync(OfferTemplates.SummaryTemplate));
            }

            bool success = await _offerService.UpdateAsync(model);

            if (success)
            {
                TempData["Success"] = "Oferta atualizada com sucesso!";
                return RedirectToAction(nameof(Index));
            }

            TempData["Error"] = "Erro ao atualizar a Oferta no banco de dados.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _offerService.DeleteAsync(id);

            if (success)
                TempData["Success"] = "Oferta removida com sucesso!";
            else
                TempData["Error"] = "Erro ao tentar remover a oferta.";

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> GetDetailsPartial(Guid id)
        {
            var viewModel = await _offerService.GetAsync(OfferTemplates.GetByIdTemplate, null, new { id = id.ToString() });

            if (viewModel == null)
            {
                TempData["Error"] = "Oferta não encontrada.";
                return RedirectToAction(nameof(Index));
            }

            return PartialView("_DetailsPartial", viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetEditPartial(Guid id)
        {
            var viewModel = await _offerService.GetForUpdateAsync(OfferTemplates.GetByIdTemplate, null, new { id = id.ToString() });
            if (viewModel == null)
                return NotFound();

            viewModel.Categories = await GetCategorySelectList();
            return PartialView("_EditPartial", viewModel);
        }

        private async Task<IEnumerable<SelectListItem>> GetCategorySelectList()
        {
            var categories = await _categoryServices.GetAllAsync();
            return categories.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            });
        }
    }
}
