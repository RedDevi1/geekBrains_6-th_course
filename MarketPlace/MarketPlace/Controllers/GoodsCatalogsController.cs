using Microsoft.AspNetCore.Mvc;
using MarketPlace.Models;
using MarketPlace.Interfaces;

namespace MarketPlace.Controllers
{
    public class GoodsCatalogsController : Controller
    {
        private readonly IGoodsCatalog catalog;
        public GoodsCatalogsController(IGoodsCatalog catalog)
        {
            this.catalog = catalog;
        }
        [HttpPost]
        public IActionResult GoodsCreation(Good model)
        {
            catalog.Create(model);
            return View();
        }

        [HttpGet]
        public IActionResult GoodsCreation()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Products()
        {
            return View(catalog);
        }
    }
}
