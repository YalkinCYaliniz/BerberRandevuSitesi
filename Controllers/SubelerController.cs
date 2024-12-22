using Microsoft.AspNetCore.Mvc;
using BerberRandevuSitesi.Data;
using BerberRandevuSitesi.Models;
using Microsoft.EntityFrameworkCore;

namespace BerberRandevuSitesi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SubelerController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public SubelerController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // Index view'ı döndüren metod
        public IActionResult Index()
        {
            ViewData["IsAdminPage"] = true; // Admin sayfası olarak işaretlemek için ViewData kullanabilirsiniz
             var subeler = dbContext.Subeler
        .Include(s => s.Calisanlar) // Şubelere bağlı çalışanları dahil et
        .ToList();

    return View(subeler); // View'a gönder
    // View döndürme
        }
    

        // API metodu: Yeni Sube eklemek
    [HttpGet("Ekle")]
    public IActionResult Ekle()
    {
        ViewData["IsAdminPage"] = true; 
        return View();
    }

    // Şube ekleme işlemini gerçekleştiren metod
    [HttpPost("Ekle")]
public IActionResult Ekle([FromForm] Subeler sube)
{
    if (!ModelState.IsValid)
    {
        var errors = ModelState.Values.SelectMany(v => v.Errors);
        foreach (var error in errors)
        {
            Console.WriteLine(error.ErrorMessage);
        }
        return View(sube); // Hatalı verilerle formu tekrar göster
    }

    dbContext.Subeler.Add(sube);
    dbContext.SaveChanges();
    return RedirectToAction("Index");
}



    [HttpPost("Sil/{id}")]
    public IActionResult Sil(int id)
    {
        var sube = dbContext.Subeler
            .Include(s => s.Calisanlar) // Çalışanlarla birlikte kontrol
            .FirstOrDefault(s => s.Id == id);

        if (sube == null)
        {
            return NotFound();
        }

        if (sube.Calisanlar != null && sube.Calisanlar.Any())
        {
            // Şubenin çalışanları varsa, önce onları silmek gerekir.
            dbContext.Calisanlar.RemoveRange(sube.Calisanlar);
        }

        dbContext.Subeler.Remove(sube);
        dbContext.SaveChanges();

        return RedirectToAction("Index");
    }
    }
}
