using Microsoft.AspNetCore.Mvc;
using MarketPlace.Models;
using MarketPlace.Interfaces;

namespace MarketPlace.Controllers
{
    public class GoodsCatalogsController : Controller
    {
        private readonly ILogger<GoodsCatalogsController> _logger;
        private readonly IGoodsCatalog catalog;
        private readonly object _syncObj_1 = new();
        public GoodsCatalogsController(ILogger<GoodsCatalogsController> logger, IGoodsCatalog catalog)
        {
            _logger = logger;
            this.catalog = catalog;
        }
        [HttpPost]
        public IActionResult GoodsCreation(Good model)
        {
            lock (_syncObj_1)
            {
                try
                {
                    catalog.Create(model);                   
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
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
        [HttpPost]
        public IActionResult GoodsRemoving(long article)
        {
            lock (_syncObj_1)
            {
                try
                {
                    catalog.Delete(article);
                }
                catch(Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
                return View();
            }               
        }
        [HttpGet]
        public IActionResult GoodsRemoving()
        {
            return View();
        }
    }
}
