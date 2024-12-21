using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using BerberRandevuSitesi.Models;
using System.Threading.Tasks;

namespace BerberRandevuSitesi.Controllers
{
    public class LoginController : Controller

    
    
    {
            

        
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public LoginController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        // GET: Login
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // POST: Login
        [HttpPost]
        public async Task<IActionResult> IndexAsync(LoginViewModel model)
        {
           if (ModelState.IsValid)
    {
        try
        {
            // Kullanıcıyı e-posta adresi ile bul
            var user = await _userManager.FindByEmailAsync(model.mailadress);
            if (user != null)
            {
                await _signInManager.SignOutAsync();

                // Şifreyi kontrol et
                var result = await _signInManager.PasswordSignInAsync(user, model.sifre, model.rememberme, false);

                if (result.Succeeded)
                {
                    // Kullanıcının rollerini al
                    var roles = await _userManager.GetRolesAsync(user);

                    // Eğer kullanıcı Admin rolündeyse admin sayfasına yönlendir
                    if (roles.Contains("Admin"))
                    {
                        return RedirectToAction("Index", "Admin"); // Admin ana sayfası
                    }

                    return RedirectToAction("Index", "Home"); // Diğer kullanıcılar için ana sayfa
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Hatalı şifre.");
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Böyle bir kullanıcı bulunamadı.");
            }
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, "Bir hata oluştu, lütfen tekrar deneyiniz. " + ex.Message);
        }
    }

            // Hata durumunda veya geçersiz modelde sayfayı tekrar göster
            if (!ModelState.IsValid)
{
    var errors = ModelState.Values.SelectMany(v => v.Errors);
    foreach (var error in errors)
    {
        Console.WriteLine(error.ErrorMessage);
    }
}

            return View(model);
        }
        [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();  // Kullanıcıyı çıkart
        return RedirectToAction("Index", "Home");  // Ana sayfaya yönlendir
    }
    }
}
