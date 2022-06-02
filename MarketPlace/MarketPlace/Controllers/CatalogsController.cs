using Microsoft.AspNetCore.Mvc;
using MarketPlace.Models;

namespace MarketPlace.Controllers
{
    public class CatalogsController : Controller
    {
        private Catalog catalog;
        public CatalogsController(Catalog catalog)
        {
            this.catalog = catalog;
        }
        [HttpPost]
        public IActionResult CreateGoods(Good model)
        {
            catalog.Goods.Add(model);
            return View();
        }

        [HttpGet]
        public IActionResult CreateGoods()
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
