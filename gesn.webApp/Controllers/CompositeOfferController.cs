using Microsoft.AspNetCore.Mvc;

namespace gesn.webApp.Controllers
{
    public class CompositeOfferController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
