using Microsoft.AspNetCore.Mvc;

namespace gesn.webApp.Controllers
{
    public class OfferComponentController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
