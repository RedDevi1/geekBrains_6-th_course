using Microsoft.AspNetCore.Mvc;
using MarketPlace.Models;
using MarketPlace.Interfaces;

namespace MarketPlace.Controllers
{
    public class GoodsCatalogsController : Controller
    {
        private readonly IGoodsCatalog catalog;
        private readonly object _syncObj_1 = new();
        public GoodsCatalogsController(IGoodsCatalog catalog)
        {
            this.catalog = catalog;
        }
        [HttpPost]
        public IActionResult GoodsCreation(Good model)
        {
            lock (_syncObj_1)
            {
                catalog.Create(model);
                return View();
            }              
        }

        [HttpGet]
        public IActionResult GoodsCreation()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Products()
        {
            lock (_syncObj_1)
            {
                return View(catalog);
            }              
        }
    }
}
