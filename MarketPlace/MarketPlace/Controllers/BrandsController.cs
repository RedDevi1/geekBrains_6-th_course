using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.Controllers
{
    public class BrandsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
