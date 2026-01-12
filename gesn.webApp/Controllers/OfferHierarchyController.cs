using Microsoft.AspNetCore.Mvc;

namespace gesn.webApp.Controllers
{
    public class OfferHierarchyController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
