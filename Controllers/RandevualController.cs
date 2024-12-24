using System;
using System.Linq;
using System.Threading.Tasks;
using BerberRandevuSitesi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using BerberRandevuSitesi.Models;
using Microsoft.AspNetCore.Identity;

namespace BerberRandevuSitesi.Controllers
{
    public class RandevualController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager; 

       public RandevualController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;  // UserManager'ı enjekte ettik
        }
        

        public async Task<IActionResult> Index(DateOnly? selectedDate)
{
    // Giriş kontrolü
    if (!User.Identity.IsAuthenticated)
    {
        // Kullanıcı giriş yapmamışsa giriş sayfasına yönlendir
        return RedirectToAction("Index", "Login"); // Login sayfası ve controller adı burada örnek
    }
    IQueryable<Randevu> randevularQuery = _context.Randevular
        .Include(r => r.Sube)
        .Include(r => r.Hizmet)
        .Include(r => r.Calisan)
        .OrderBy(r => r.Tarih)
        .ThenBy(r => r.Saat);    // Sonra saat sıralaması

    if (selectedDate.HasValue)
    {
        randevularQuery = randevularQuery.Where(r => r.Tarih == selectedDate.Value);
    }

    var randevular = await randevularQuery.ToListAsync();

    if (randevular.Count == 0)
    {
        ViewBag.Message = "Seçilen tarihte randevu bulunmamaktadır.";
    }

    return View(randevular);
}

    [HttpPost]
public async Task<IActionResult> RandevuOlustur(int id)
{
   // Giriş yapan kullanıcının ApplicationUser nesnesini al
    var user = await _userManager.GetUserAsync(User);  // UserManager aracılığıyla giriş yapan kullanıcıyı alsteriId = User.Identity.Name; 

    if (user == null)
    {
        // Kullanıcı oturum açmamış, hata veya yönlendirme yapılabilir
        return RedirectToAction("Index", "Login");
    }

    var musteriId = user.Id;

    // Randevuyu veritabanından al
    var randevu = await _context.Randevular
        .FirstOrDefaultAsync(r => r.ID == id);

    // Eğer randevu bulunmazsa, hata döndür
    if (randevu == null)
    {
        return NotFound();
    }
    
    // Eğer müşteri zaten 2 randevu aldıysa, yeni bir randevu almasına izin verme
    if (user.randevusayisi >= 2)
    {
        TempData["UyarıMesaji"] = "Bir kullanıcı en fazla 2 randevu alabilir.";
    return RedirectToAction("Index"); // Aynı sayfada kalmak için yönlendir
    }
    else
    {
    // Randevu alındı, randevunun uygunluk durumunu güncelle
    randevu.musaitlik = false;  // Randevu alındı, dolu yapıldı.
    randevu.ApplicationUserId = musteriId;  // Müşterinin ID'sini randevuya ekle
    user.randevusayisi +=1;

    // Veritabanına kaydet
    _context.Randevular.Update(randevu);
    await _context.SaveChangesAsync();

    // Kullanıcıyı başarıyla işlem yaptıktan sonra yönlendir
    return RedirectToAction("Index");  // Aynı sayfada kalmak için
    }
}






    }
}
