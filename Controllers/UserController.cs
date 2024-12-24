using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BerberRandevuSitesi.Data;
using BerberRandevuSitesi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BerberRandevuSitesi.Controllers
{
    [Authorize]
    public class UserController:Controller
    {
         private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager; 

       public UserController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;  // UserManager'ı enjekte ettik
        }
        
        // Profil sayfasına yönlendiren method
        public async Task<IActionResult> Index()
        {
            // Giriş yapan kullanıcının bilgilerini al
            var user = await _userManager.GetUserAsync(User);

            // Kullanıcı bilgilerini View'a gönder
            return View(user);
        }

        public  async Task<IActionResult> Edit(string id)
        {
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
            
            
            return RedirectToAction("Index", "User"); // Başarılı kullanıcı güncelleme sonrası giriş sayfasına yönlendirme
            }
            [HttpPost]
public async Task<IActionResult> Edit(string id, EditViewModel model)
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

    public async Task<IActionResult> RandevuGuncelle(string id)
{
    var user = await _userManager.FindByIdAsync(id);
    if (user != null)
    {
       var randevular = _context.Randevular
                         .Include(r => r.Sube)    // Şube bilgisini yükle
                         .Include(r => r.Hizmet)  // Hizmet bilgisini yükle
                         .Include(r => r.Calisan) // Çalışan bilgisini yükle
                         .Where(r => r.ApplicationUserId == id)  // Kullanıcıya ait randevuları filtrele
                         .ToList();

        // Kullanıcı ve randevular ile birlikte view'a gönder
        return View(randevular);
    }
    return RedirectToAction("Index", "User");  // Kullanıcı bulunamazsa, profil sayfasına geri dön
}



[HttpPost]
public async Task<IActionResult> Delete(int id)
{
    Console.WriteLine("Help");
    try
    {
        var user = await _userManager.GetUserAsync(User);
        // Randevuyu ID ile bul
        var randevu = await _context.Randevular
                                     .FirstOrDefaultAsync(r => r.ID == id);
        
        if (randevu != null)
        {
            // Randevunun ApplicationUserId'sini null yap
            randevu.ApplicationUserId = null;
            randevu.musaitlik = true;
            user.randevusayisi-=1;

            // Değişiklikleri kaydet
            await _context.SaveChangesAsync();
            
            // Başarı mesajı
            TempData["SuccessMessage"] = "Randevunuz başarıyla iptal edilmiştir.";
        }
        else
        {
            // Hata mesajı
            TempData["ErrorMessage"] = "Randevu bulunamadı.";
        }
    }
    catch (Exception ex)
    {
        TempData["ErrorMessage"] = "Bir hata oluştu: " + ex.Message;
    }

    // Randevular sayfasına yönlendir
    return RedirectToAction("RandevuGuncelle", "User");
}



    }
}