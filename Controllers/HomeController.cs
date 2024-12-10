using System.Diagnostics;
using BerberRandevuSitesi.Models;
using Microsoft.AspNetCore.Mvc;

namespace BerberRandevuSitesi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Randevu()
        {
            return View();
        }

        public IActionResult Hizmetler()
        {
            return View();
        }

        public IActionResult SacDene()
        {
            return View();
        }

        public IActionResult Hakkimizda()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
