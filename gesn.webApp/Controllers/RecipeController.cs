using Microsoft.AspNetCore.Mvc;

namespace gesn.webApp.Controllers
{
    public class RecipeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
