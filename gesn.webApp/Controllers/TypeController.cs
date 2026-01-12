using Microsoft.AspNetCore.Mvc;

namespace gesn.webApp.Controllers
{
    public class TypeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
