using BerberRandevuSitesi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;
public class RegisterController : Controller
{
    private readonly ApplicationDbContext _context;

    public RegisterController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
public IActionResult Index(Musteri model)
{
    if (ModelState.IsValid)
    {
        try
        {
            // Telefon numarasını ve e-postayı LINQ ile kontrol et
            var existingUser = _context.musteri
                .FirstOrDefault(m => m.mailadress == model.mailadress || m.telefonno == model.telefonno);

            if (existingUser != null)
            {
                if (existingUser.mailadress == model.mailadress)
                {
                    ModelState.AddModelError("mailadress", "Bu e-posta adresi zaten kayıtlıdır.");
                }
                if (existingUser.telefonno == model.telefonno)
                {
                    ModelState.AddModelError("telefonno", "Bu telefon numarası zaten kayıtlıdır.");
                }
                

            }
            else
            {
                // Hem e-posta hem telefon numarası benzersizse, yeni müşteri kaydet
                _context.musteri.Add(model);
                _context.SaveChanges();

                ViewBag.Message = "Kayıt başarıyla oluşturuldu.";
                return RedirectToAction("Index", "Login"); // Başarı durumu
            }
        }
        catch (DbUpdateException ex)
        {
            if (ex.InnerException is PostgresException postgresException && postgresException.SqlState == "23505")
            {
                // Unique constraint ihlali
                ModelState.AddModelError("", "E-posta veya telefon numarası zaten kayıtlıdır.");
            }
            else
            {
                ModelState.AddModelError("", "Bir hata oluştu, lütfen tekrar deneyiniz.");
            }
        }
    }

    // Hata durumu: Sayfayı tekrar kullanıcıya göster
    return View(model);
}


    
}
