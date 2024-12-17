using Microsoft.AspNetCore.Mvc;

namespace BerberRandevuSitesi.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}