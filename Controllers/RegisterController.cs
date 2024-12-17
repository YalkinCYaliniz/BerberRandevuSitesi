using Microsoft.AspNetCore.Mvc;

namespace BerberRandevuSitesi.Controllers
{
    public class RegisterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}