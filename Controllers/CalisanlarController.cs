using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BerberRandevuSitesi.Data;
using BerberRandevuSitesi.Models;
using System.Linq;
using System.Threading.Tasks;
using BerberRandevuSitesi.DTOs;

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
            return View(); // Çalışanlar listesi sayfası
        }
        

        // Tüm çalışanları getir
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

        // Belirli bir çalışan bilgilerini getir
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

    var calisanDto = new CalisanDTO
    {
        CalisanId = calisan.CalisanId,
        Adi = calisan.Adi,
        Soyadi = calisan.Soyadi,
        Maas = calisan.Maas,
        SubeId = calisan.SubeId,
        CalisanYetenekler = calisan.CalisanYetenekler.Select(cy => new CalisanYetenekDTO
        {
            HizmetId = cy.HizmetId,
            HizmetAdi = cy.Hizmet?.HizmetAdi
        }).ToList()
    };

    return Ok(calisanDto);
}


        // Çalışan düzenleme sayfasını döndürme
        [HttpGet("/Calisanlar/Duzenle/{id}")]
        public async Task<IActionResult> Duzenle(int id)
        {
            ViewData["IsAdminPage"] = true;

            var calisan = await _dbContext.Calisanlar
                .Include(c => c.Sube)
                .Include(c => c.CalisanYetenekler)
                .ThenInclude(cy => cy.Hizmet)
                .FirstOrDefaultAsync(c => c.CalisanId == id);

            if (calisan == null)
            {
                return NotFound("Çalışan bulunamadı.");
            }

            var hizmetler = await _dbContext.Hizmetler.ToListAsync();
            ViewBag.Hizmetler = hizmetler;

            return View(calisan);
        }

        // Çalışan düzenleme işlemi (Yalnızca isim, soyisim, maaş, şube güncelleme)
        [HttpPut("{id}")]
public async Task<IActionResult> Duzenle(int id, [FromBody] CalisanDTO calisanDto)
{
    if (id != calisanDto.CalisanId)
        return BadRequest(new { message = "ID uyuşmazlığı." });

    if (!ModelState.IsValid)
        return BadRequest(new { message = "Doğrulama hatası.", errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });

    var existingCalisan = await _dbContext.Calisanlar
        .Include(c => c.CalisanYetenekler)
        .FirstOrDefaultAsync(c => c.CalisanId == id);

    if (existingCalisan == null)
        return NotFound(new { message = "Çalışan bulunamadı." });

    existingCalisan.Adi = calisanDto.Adi;
    existingCalisan.Soyadi = calisanDto.Soyadi;
    existingCalisan.Maas = calisanDto.Maas;
    existingCalisan.SubeId = calisanDto.SubeId;

    // Eski yetenekleri sil
    _dbContext.CalisanYetenekler.RemoveRange(existingCalisan.CalisanYetenekler);

    // Yeni yetenekleri ekle
    if (calisanDto.CalisanYetenekler != null)
    {
        foreach (var yetenek in calisanDto.CalisanYetenekler)
        {
            _dbContext.CalisanYetenekler.Add(new CalisanYetenek
            {
                CalisanId = id,
                HizmetId = yetenek.HizmetId
            });
        }
    }

    await _dbContext.SaveChangesAsync();
    return Ok(existingCalisan);
}



        // Yetenek ekleme ve silme işlemi
        [HttpPost("/Calisanlar/Duzenle/Yetenek/{id}")]
public async Task<IActionResult> YetenekGuncelle(int id, [FromBody] List<CalisanYetenekDTO> yeniYetenekDtos)
{
    var existingCalisan = await _dbContext.Calisanlar
        .Include(c => c.CalisanYetenekler)
        .FirstOrDefaultAsync(c => c.CalisanId == id);

    if (existingCalisan == null)
    {
        return NotFound(new { message = "Çalışan bulunamadı." });
    }

    // Mevcut yetenekleri kaldır
    _dbContext.CalisanYetenekler.RemoveRange(existingCalisan.CalisanYetenekler);

    // Yeni yetenekleri ekle
    if (yeniYetenekDtos != null)
    {
        foreach (var dto in yeniYetenekDtos)
        {
            var hizmet = await _dbContext.Hizmetler.FirstOrDefaultAsync(h => h.ID == dto.HizmetId);
            if (hizmet != null)
            {
                _dbContext.CalisanYetenekler.Add(new CalisanYetenek
                {
                    CalisanId = id,
                    HizmetId = dto.HizmetId
                });
            }
        }
    }

    await _dbContext.SaveChangesAsync();

    return Ok(new { message = "Yetenekler güncellendi." });
}

        


   [HttpGet]
[Route("/Calisanlar/Ekle")]
public IActionResult Ekle()
{
ViewData["IsAdminPage"] = true;
    return View(); // Razor View dosyasını döner
}

         [HttpPost]
public async Task<IActionResult> Ekle([FromBody] CalisanDTO calisanDto)
{
    if (!ModelState.IsValid)
    {
        return BadRequest(new
        {
            message = "Gönderilen veri modeli geçerli değil.",
            errors = ModelState
        });
    }

    try
    {
        var calisan = new Calisanlar
        {
            Adi = calisanDto.Adi,
            Soyadi = calisanDto.Soyadi,
            Maas = calisanDto.Maas,
            SubeId = calisanDto.SubeId,
            CalisanYetenekler = calisanDto.CalisanYetenekler?.Select(y => new CalisanYetenek
            {
                HizmetId = y.HizmetId
            }).ToList() ?? new List<CalisanYetenek>()
        };

        _dbContext.Calisanlar.Add(calisan);
        await _dbContext.SaveChangesAsync();

        return Ok(new { message = "Çalışan başarıyla eklendi." });
    }
    catch (Exception ex)
    {
        return StatusCode(500, new { message = "Sunucu hatası.", details = ex.Message });
    }
}
[HttpGet("Sube/{subeId}")]
    public IActionResult GetSubeCalisanlar(int subeId)
    {
        var calisanlar = _dbContext.Calisanlar
                                 .Where(c => c.SubeId == subeId)
                                 .Select(c => new
                                 {
                                     id = c.SubeId,
                                     adi = c.Adi
                                 }).ToList();

        return Ok(calisanlar);
    }









// Çalışan silme
        [HttpDelete("{id}")]
        public IActionResult Sil(int id)
        {
            var calisan = _dbContext.Calisanlar
                .Include(c => c.CalisanYetenekler)
                .FirstOrDefault(c => c.CalisanId == id);

            if (calisan == null)
            {
                return NotFound(new { message = "Çalışan bulunamadı." });
            }

            _dbContext.Calisanlar.Remove(calisan);
            _dbContext.SaveChanges();

            return NoContent();
        }


    }
    
    
}
