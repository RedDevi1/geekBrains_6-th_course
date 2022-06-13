using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.Controllers
{
    public class GoodsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
