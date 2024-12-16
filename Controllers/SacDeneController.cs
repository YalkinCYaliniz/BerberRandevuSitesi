using Microsoft.AspNetCore.Mvc;

namespace BerberRandevuSitesi.Controllers
{
    public class SacDeneController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}