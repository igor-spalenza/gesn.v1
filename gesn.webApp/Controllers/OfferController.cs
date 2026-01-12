using Microsoft.AspNetCore.Mvc;

namespace gesn.webApp.Controllers
{
    public class OfferController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
