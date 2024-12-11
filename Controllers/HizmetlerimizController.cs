using Microsoft.AspNetCore.Mvc;

namespace BerberRandevuSitesi.Controllers
{
    public class HizmetlerimizController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
