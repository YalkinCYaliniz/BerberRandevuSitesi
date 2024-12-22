using BerberRandevuSitesi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BerberRandevuSitesi.Controllers
{

    public class KullanicilarController : Controller
    {
        public KullanicilarController(UserManager<ApplicationUser> userManager)
        {

            _userManager = userManager;
        }

        private UserManager<ApplicationUser> _userManager;

        public IActionResult Index()
        {
            ViewData["IsAdminPage"] = true;
            return View(_userManager.Users);
        }

        [HttpGet]
        public IActionResult Olustur()
        {
            ViewData["IsAdminPage"] = true;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> OlusturAsync(CreateViewModel model)
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
                            yas = (int)model.yas,
                            ad = model.ad,
                            soyad = model.soyad
                        
                        };

                        var result = await _userManager.CreateAsync(user, model.sifre);
                        if (result.Succeeded)
                        {
                            return RedirectToAction("Index", "Kullanicilar"); // Başarılı kullanıcı oluşturma sonrası giriş sayfasına yönlendirme
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
    
        public  async Task<IActionResult> Guncelle(string id)
        {
            ViewData["IsAdminPage"] = true;
            if(id == null)
            {
                return RedirectToAction("Index", "Kullanicilar");
            }
            var user = await _userManager.FindByIdAsync(id);
            if(user !=null)
            {
                return View(new EditViewModel{
                    Id = user.Id,
                    ad = user.ad,
                    soyad = user.soyad,
                    mailadress = user.Email,
                    yas = user.yas,
                    telefonno = user.PhoneNumber
                });
        }
            
            
            return RedirectToAction("Index", "Kullanicilar"); // Başarılı kullanıcı güncelleme sonrası giriş sayfasına yönlendirme
            }
            [HttpPost]
public async Task<IActionResult> Guncelle(string id, EditViewModel model)
{
    if (id != model.Id)
    {
        return RedirectToAction("Index");
    }

    if (ModelState.IsValid)
    {
        var user = await _userManager.FindByIdAsync(model.Id);
        if (user != null)
        {
            // E-posta adresi kontrolü
            var existingEmailUser = await _userManager.FindByEmailAsync(model.mailadress);
            if (existingEmailUser != null && existingEmailUser.Id != model.Id)
            {
                ModelState.AddModelError("mailadress", "Bu e-posta adresi zaten kullanılıyor.");
                return View(model);
            }

            // Telefon numarası kontrolü
            var usersWithPhoneNumber = _userManager.Users
                .Where(u => u.PhoneNumber == model.telefonno && u.Id != model.Id)
                .ToList();
            if (usersWithPhoneNumber.Any())
            {
                ModelState.AddModelError("telefonno", "Bu telefon numarası zaten kullanılıyor.");
                return View(model);
            }

            // Kullanıcı bilgilerini güncelle
            user.ad = model.ad;
            user.soyad = model.soyad;
            user.Email = model.mailadress;
            user.PhoneNumber = model.telefonno;
            user.yas = (int)model.yas;

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded && !string.IsNullOrEmpty(model.sifre))
{
    // Şifre koşullarını kontrol et
    if (model.sifre.Length < 6)
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

    // Eğer hatalar varsa işlemi durdur ve modeli tekrar döndür
    if (ModelState.ErrorCount > 0)
    {
        return View(model);
    }

    // Şifre geçerli ise güncelleme işlemini yap
    await _userManager.RemovePasswordAsync(user);
    await _userManager.AddPasswordAsync(user, model.sifre);
}


            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }

            foreach (IdentityError err in result.Errors)
            {
                ModelState.AddModelError("", err.Description);
            }
        }
    }

    return View(model);
}



        [HttpPost]

        public async Task<IActionResult> Sil(string id){

            var user = await _userManager.FindByIdAsync(id);

            if(user != null){

                await _userManager.DeleteAsync(user);
            }
            return RedirectToAction("Index");
        }


        }
    }







