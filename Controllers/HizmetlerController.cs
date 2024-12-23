using BerberRandevuSitesi.Data;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class HizmetlerController : Controller
{
    private readonly ApplicationDbContext _dbContext;

    public HizmetlerController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public IActionResult GetHizmetler()
    {
        var hizmetler = _dbContext.Hizmetler
            .Select(h => new
            {
                h.ID,
                h.HizmetAdi
            })
            .ToList();

        if (hizmetler == null || !hizmetler.Any())
        {
            return NotFound(new { message = "Hizmetler bulunamadı." });
        }

        return Ok(hizmetler);
    }
}
