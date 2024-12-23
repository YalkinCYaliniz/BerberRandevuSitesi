using Microsoft.AspNetCore.Mvc;
using BerberRandevuSitesi.Data;
using BerberRandevuSitesi.Models;
using Microsoft.EntityFrameworkCore;

namespace BerberRandevuSitesi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubelerController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public SubelerController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

      [HttpGet]
        public IActionResult GetSubeler()
        {
            var subeler = _dbContext.Subeler
                .Include(s => s.Calisanlar)
                .Select(s => new
                {
                    s.Id,
                    s.SubeAdi,
                    Calisanlar = s.Calisanlar.Select(c => new { c.CalisanId, c.Adi, c.Soyadi }).ToList()
                })
                .ToList();

            return Ok(subeler);
        }

        // Razor View: Şubeler sayfasını döner
        [HttpGet("/Subeler")]
        public IActionResult Index()
        {
        ViewData["IsAdminPage"] = true;
            return View(); // Razor View olan Views/Subeler/Index.cshtml dosyasını döner
        }

        // Şube Bilgisi Getirme (GET)
        [HttpGet("{id}")]
        public IActionResult GetSube(int id)
        {
            var sube = _dbContext.Subeler
                .Include(s => s.Calisanlar)
                .Where(s => s.Id == id)
                .Select(s => new
                {
                    s.Id,
                    s.SubeAdi,
                    Calisanlar = s.Calisanlar.Select(c => new { c.CalisanId, c.Adi, c.Soyadi })
                })
                .FirstOrDefault();

            if (sube == null)
                return NotFound(new { message = "Şube bulunamadı." });

            return Ok(sube);
        }

        // Şube Ekleme (POST)
        [HttpGet("/Subeler/Ekle")]
public IActionResult Ekle()
{
ViewData["IsAdminPage"] = true;
    return View();
}

[HttpPost("/Subeler/Ekle")]
public IActionResult Ekle([FromForm] Subeler sube)
{
    if (!ModelState.IsValid)
    {
        return View(sube); // Formu tekrar göster
    }

    _dbContext.Subeler.Add(sube);
    _dbContext.SaveChanges();
    return RedirectToAction("Index", "Subeler");
}


        // Şube Güncelleme (PUT)
        [HttpPut("{id}")]
        public IActionResult UpdateSube(int id, [FromBody] Subeler sube)
        {
            if (id != sube.Id)
                return BadRequest("Şube ID uyuşmuyor.");

            var existingSube = _dbContext.Subeler.FirstOrDefault(s => s.Id == id);
            if (existingSube == null)
                return NotFound(new { message = "Şube bulunamadı." });

            existingSube.SubeAdi = sube.SubeAdi;
            _dbContext.SaveChanges();

            return NoContent();
        }

        // Şube Silme (DELETE)
        [HttpDelete("{id}")]
        public IActionResult DeleteSube(int id)
        {
            var sube = _dbContext.Subeler
                .Include(s => s.Calisanlar)
                .FirstOrDefault(s => s.Id == id);

            if (sube == null)
                return NotFound(new { message = "Şube bulunamadı." });

            if (sube.Calisanlar != null && sube.Calisanlar.Any())
                _dbContext.Calisanlar.RemoveRange(sube.Calisanlar);

            _dbContext.Subeler.Remove(sube);
            _dbContext.SaveChanges();

            return NoContent();
        }
    }
}
