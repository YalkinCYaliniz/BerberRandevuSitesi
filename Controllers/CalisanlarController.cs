using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BerberRandevuSitesi.Data;
using BerberRandevuSitesi.Models;
using System.Linq;
using System.Threading.Tasks;

namespace BerberRandevuSitesi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CalisanlarController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public CalisanlarController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Çalışanları listeleme ve düzenleme sayfasını döndürme (Razor View)
        [HttpGet]
        [Route("/Calisanlar")]
        public IActionResult Index()
        {
            ViewData["IsAdminPage"] = true;
            return View();  // Çalışanlar listesi sayfası
        }
        [HttpGet]
        public IActionResult GetAllCalisanlar()
        {
            var calisanlar = _dbContext.Calisanlar
                .Include(c => c.Sube)
                .Include(c => c.CalisanYetenekler)
                .ThenInclude(cy => cy.Hizmet)
                .ToList();

            if (calisanlar == null || !calisanlar.Any())
            {
                return NotFound(new { message = "Çalışanlar bulunamadı." });
            }

            return Ok(calisanlar);
        }

        

        // API: Çalışan bilgilerini getir
     
[HttpGet("{id}")]
public async Task<IActionResult> GetCalisan(int id)
{
    var calisan = await _dbContext.Calisanlar
        .Include(c => c.Sube)
        .Include(c => c.CalisanYetenekler)
        .ThenInclude(cy => cy.Hizmet)
        .FirstOrDefaultAsync(c => c.CalisanId == id);

    if (calisan == null)
    {
        return NotFound(new { message = "Çalışan bulunamadı." });
    }

    return Ok(new
    {
        calisan.CalisanId,
        calisan.Adi,
        calisan.Soyadi,
        calisan.Maas,
        calisan.SubeId,
        Yetenekler = calisan.CalisanYetenekler.Select(cy => new
        {
            HizmetId = cy.HizmetId,
            HizmetAdi = cy.Hizmet.HizmetAdi
        })
    });
}



    [HttpGet("/Calisanlar/Duzenle/{id}")]
        public async Task<IActionResult> Duzenle(int id)
        {
        ViewData["IsAdminPage"] = true;
            var calisan = await _dbContext.Calisanlar
                .Include(c => c.Sube)
                .Include(c => c.CalisanYetenekler)
                .FirstOrDefaultAsync(c => c.CalisanId == id);

            if (calisan == null)
            {
                return NotFound("Çalışan bulunamadı.");
            }

            return View(calisan); // Razor View'ı döndürür
        }




               [HttpPut("/api/Calisanlar/{id}")]
public async Task<IActionResult> Duzenle(int id, [FromBody] Calisanlar calisan)
{
    if (id != calisan.CalisanId)
    {
        return BadRequest(new { message = "Çalışan ID'si uyuşmuyor." });
    }

    if (!ModelState.IsValid)
    {
        return BadRequest(new { 
            message = "Gönderilen veri modeli geçerli değil.", 
            errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) 
        });
    }

    var existingCalisan = await _dbContext.Calisanlar
        .Include(c => c.CalisanYetenekler)
        .FirstOrDefaultAsync(c => c.CalisanId == id);
    if (existingCalisan == null)
    {
        return NotFound(new { message = "Çalışan bulunamadı." });
    }

    existingCalisan.Adi = calisan.Adi;
    existingCalisan.Soyadi = calisan.Soyadi;
    existingCalisan.Maas = calisan.Maas;
    existingCalisan.SubeId = calisan.SubeId;

    // Yetenek güncelleme
    _dbContext.CalisanYetenekler.RemoveRange(existingCalisan.CalisanYetenekler);
    if (calisan.CalisanYetenekler != null)
    {
        foreach (var yetenek in calisan.CalisanYetenekler)
        {
            _dbContext.CalisanYetenekler.Add(new CalisanYetenek
            {
                CalisanId = id,
                HizmetId = yetenek.HizmetId
            });
        }
    }

    try
    {
        await _dbContext.SaveChangesAsync();
    }
    catch (DbUpdateException ex)
    {
        return StatusCode(500, new { message = "Veritabanı güncelleme hatası.", details = ex.Message });
    }

    return NoContent();
}





        [HttpDelete("{id}")]
public IActionResult Sil(int id)
{
    var calisan = _dbContext.Calisanlar
        .Include(c => c.CalisanYetenekler)
        .FirstOrDefault(c => c.CalisanId == id);

    if (calisan == null)
        return NotFound(new { message = "Çalışan bulunamadı." });

    _dbContext.Calisanlar.Remove(calisan);
    _dbContext.SaveChanges();

    return NoContent();
}

    }
}
