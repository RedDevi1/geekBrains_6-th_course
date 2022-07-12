using MarketPlace.Middleware;
using MarketPlace.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MarketPlace.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpPagesTraversesCountMiddleware _httpPagesTraversesCountMiddleware;
        public HomeController(ILogger<HomeController> logger, HttpPagesTraversesCountMiddleware httpPagesTraversesCountMiddleware)
        {
            _logger = logger;
            _httpPagesTraversesCountMiddleware = httpPagesTraversesCountMiddleware;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        
        public IActionResult Metrics()
        {
            return View(_httpPagesTraversesCountMiddleware);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}