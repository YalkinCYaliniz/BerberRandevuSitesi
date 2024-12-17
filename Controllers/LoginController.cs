using BerberRandevuSitesi.Data;
using Microsoft.AspNetCore.Mvc;

namespace BerberRandevuSitesi.Controllers
{
    public class LoginController : Controller
    {

        private readonly ApplicationDbContext _dbContext; // DbContext alanı

        // Constructor: Dependency Injection ile DbContext'i alıyoruz
        public LoginController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            return View(); // Ana sayfa
        }

        [HttpPost]
        public IActionResult Giris(Musteri musteri)
        {
            if (ModelState.IsValid)
            {
                // Veritabanında kullanıcıyı sorgula
                var user = _dbContext.musteri
                                     .FirstOrDefault(u => u.mailadress == musteri.mailadress && u.sifre == musteri.sifre);

                if (user != null)
                {
                    // Kullanıcı bulundu, başarılı giriş
                    // Örneğin, session veya cookie ile giriş bilgisini tutabilirsiniz
                    // Şu an sadece ana menüye yönlendiriyoruz
                    return RedirectToAction("Index", "Home"); // Ana sayfaya yönlendir
                }
                else
                {
                    // Kullanıcı bulunamadı, hata mesajı
                    ModelState.AddModelError(string.Empty, "Geçersiz e-posta veya şifre.");
                    return RedirectToAction("Index", "Login"); // Ana sayfaya yönlendir
                }
            }

            // Model geçerli değilse tekrar view'a dön
            return RedirectToAction("Index", "Login"); // Ana sayfaya yönlendir
        }
    }
}