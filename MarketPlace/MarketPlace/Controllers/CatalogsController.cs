using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.Controllers
{
    public class CatalogsController : Controller
    {
        public IActionResult Goods()
        {
            return View();
        }
    }
}
