using Microsoft.AspNetCore.Mvc;

namespace gesn.webApp.Controllers
{
    public class CategoryController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
