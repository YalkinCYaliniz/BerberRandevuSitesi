using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BerberRandevuSitesi.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            ViewData["IsAdminPage"] = true; 
            ViewBag.AdminName = "Admin"; 
            return View();
        }
    }
}
