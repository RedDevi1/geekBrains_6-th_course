using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.Controllers
{
    public class CategoriesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
