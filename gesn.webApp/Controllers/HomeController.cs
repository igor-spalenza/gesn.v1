using System.Diagnostics;
using gesn.webApp.Infrastructure.Repositories.Templates;
using gesn.webApp.Infrastructure.Repositories.Templates.Base;
using gesn.webApp.Interfaces.Services;
using gesn.webApp.Models;
using gesn.webApp.Models.Entities.Offer;
using Microsoft.AspNetCore.Mvc;

namespace gesn.webApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        protected ITestesServices TestesServices { get; set; }

        public HomeController(ILogger<HomeController> logger, ITestesServices testesServices)
        {
            _logger = logger;
            this.TestesServices = testesServices;
        }

        public async Task<IActionResult> Index()
        {
            Guid obj = await this.TestesServices.AddAsync(new Models.Entities.Offer.Offer($@"TESTE {new Random().Next(int.MaxValue)}", new Random().Next(int.MaxValue)));
            IEnumerable<Offer> foo = await this.TestesServices.ReadAsync(OfferTemplates.TesteTemplate, new List<WhereTemplate> { WhereTemplate.Create("O.UnitPrice < 10") });
            //var offer = await this.TestesServices.GetAsync(obj);
            //var bar = await this.TestesServices.UpdateAsync(offer);

            Console.WriteLine(foo);
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
