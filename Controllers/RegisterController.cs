using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using BerberRandevuSitesi.Models;

namespace BerberRandevuSitesi.Controllers
{
    public class RegisterController: Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public RegisterController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        [HttpGet]
        public IActionResult Index(){

            return View();
        }

        [HttpPost]
public async Task<IActionResult> IndexAsync(CreateViewModel model)
{
    if (ModelState.IsValid)
    {
        try
        {
            // E-posta adresi ile kullanıcı kontrolü
            var existingUser = await _userManager.FindByEmailAsync(model.mailadress);
            if (existingUser != null)
            {
                ModelState.AddModelError("mailadress", "Bu e-posta adresi zaten kayıtlı.");
            }

            // Telefon numarası ile kullanıcı kontrolü
            if (_userManager.Users.Any(u => u.PhoneNumber == model.telefonno))
            {
                ModelState.AddModelError("telefonno", "Bu telefon numarası zaten kayıtlı.");
            }
             // Şifre koşullarını kontrol et
            if (string.IsNullOrEmpty(model.sifre) || model.sifre.Length < 6)
            {
                ModelState.AddModelError("sifre", "Şifre en az 6 karakter uzunluğunda olmalıdır.");
            }
            else if (!model.sifre.Any(char.IsLower))
            {
                ModelState.AddModelError("sifre", "Şifre en az bir küçük harf içermelidir.");
            }
            else if (!model.sifre.Any(char.IsUpper))
            {
                ModelState.AddModelError("sifre", "Şifre en az bir büyük harf içermelidir.");
            }
            else if (!model.sifre.Any(char.IsPunctuation) && !model.sifre.Any(char.IsSymbol))
            {
                ModelState.AddModelError("sifre", "Şifre en az bir özel karakter içermelidir.");
            }

            if (ModelState.ErrorCount > 0)
            {
                return View(model);
            }

            // Eğer hata yoksa, yeni kullanıcı oluştur
            if (ModelState.ErrorCount == 0)
            {
                var user = new ApplicationUser
                {
                    UserName = model.mailadress.Split('@')[0],
                    Email = model.mailadress,
                    PhoneNumber = model.telefonno,
                    yas = model.yas
                    
                };

                var result = await _userManager.CreateAsync(user, model.sifre);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Login"); // Başarılı kullanıcı oluşturma sonrası giriş sayfasına yönlendirme
                }

                // Şifre veya diğer doğrulama hataları
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
        }
        catch (Exception ex)
        {
            // Genel hata yönetimi
            ModelState.AddModelError("", "Bir hata oluştu, lütfen tekrar deneyiniz. " + ex.Message);
        }
    }

    // Hata durumu veya geçersiz modelde sayfayı tekrar göster
    return View(model);
}


    }
}