using Microsoft.AspNetCore.Mvc;
using BerberRandevuSitesi.Data;
using BerberRandevuSitesi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace BerberRandevuSitesi.Controllers
{
    [Authorize(Roles ="Admin")]
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

        [HttpGet("/Subeler")]
        public IActionResult Index()
        {
            ViewData["IsAdminPage"] = true;
            return View(); 
        }

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
        
        [HttpGet("sube/{subeId}")]
        public IActionResult GetSubeCalisanlar(int subeId)
        {
            var calisanlar = _dbContext.Calisanlar
                .Where(c => c.SubeId == subeId)
                .Select(c => new
                {
                    id = c.CalisanId, // Benzersiz çalışan ID'si
                    adi = c.Adi,
                    soyadi = c.Soyadi
                })
                .ToList();

            if (!calisanlar.Any())
            {
                return NotFound(new { message = "Bu şubeye ait çalışan bulunamadı." });
            }

            return Ok(calisanlar);
        }

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
