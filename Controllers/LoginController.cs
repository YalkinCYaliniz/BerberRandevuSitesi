using Microsoft.AspNetCore.Mvc;
using BerberRandevuSitesi.Data;
using BerberRandevuSitesi.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BerberRandevuSitesi.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public LoginController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Admin kontrolü
                var admin = _dbContext.admin.FirstOrDefault(a => a.mailadress == model.mailadress);
                if (admin != null && admin.sifre == model.sifre)
                {
                    // Admin başarıyla giriş yaptı
                    return RedirectToAction("Index", "Hakkimizda"); // Örneğin Hakkımızda sayfasına yönlendirilebilir
                }

                // Müşteri kontrolü
                var musteri = _dbContext.musteri.FirstOrDefault(m => m.mailadress == model.mailadress);
                if (musteri != null && musteri.sifre == model.sifre)
                {
                    // Müşteri başarıyla giriş yaptı
                    return RedirectToAction("Index", "Home"); // Ana sayfaya yönlendirilebilir
                }

                // Eğer kullanıcı bulunamadıysa
                ModelState.AddModelError("", "Geçersiz e-posta veya şifre.");
                return View();
            }

            return View();
        }
    }
}
