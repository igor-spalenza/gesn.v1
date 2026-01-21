using FluentValidation;
using gesn.webApp.Infrastructure.Repositories.Templates;
using gesn.webApp.Infrastructure.Repositories.Templates.Base;
using gesn.webApp.Interfaces.Services;
using gesn.webApp.Models;
using gesn.webApp.Models.Entities.Offer;
using gesn.webApp.Models.ViewModels.Offer.CompositeProduct;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace gesn.webApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //protected ITestesServices TestesServices { get; set; }
        private IValidator<BasicOfferInsertVM> BasicOfferInsertValidator;
        public HomeController(ILogger<HomeController> logger, IValidator<BasicOfferInsertVM> validator)
        {
            _logger = logger;
            //this.TestesServices = testesServices;
            this.BasicOfferInsertValidator = validator;
        }

        public async Task<IActionResult> Index()
        {
            var teste = new BasicOfferInsertVM($@"TESTE ", new Random().Next(int.MaxValue));
            var validation = await BasicOfferInsertValidator.ValidateAsync(teste);

            if (!validation.IsValid)
                throw new Exception(string.Join("\n", validation.Errors.Select(err => err.ErrorMessage)));

            var teste2 = teste.Adapt<Offer>();
            //Guid obj = await this.TestesServices.AddAsync(teste.Adapt<Offer>());
            //IEnumerable<Offer> foo = await this.TestesServices.ReadAsync(OfferTemplates.TesteTemplate, new List<WhereTemplate> { WhereTemplate.Create("O.UnitPrice < 10") });
            //var offer = await this.TestesServices.GetAsync(obj);
            //offer.Note = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            //bool flagUpdated = await this.TestesServices.UpdateAsync(offer);
            //var all = await this.TestesServices.GetAllAsync();
            //await this.TestesServices.DeleteAsync(obj);
            //Console.WriteLine(foo);
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
