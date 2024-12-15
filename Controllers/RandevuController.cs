using Microsoft.AspNetCore.Mvc;

public class RandevuController : Controller
{
    private readonly RandevuDbContext _context;

    public RandevuController(RandevuDbContext context)
    {
        _context = context;
    }

    [HttpGet]
public IActionResult Yeni()
{
    var model = new RandevuViewModel
    {
        Subeler = _context.Subeler.ToList(),
        Hizmetler = _context.Hizmetler.ToList()
    };
    return View(model);
}

[HttpPost]
public IActionResult Yeni(RandevuViewModel model)
{
    if (ModelState.IsValid)
    {
        var yeniRandevu = new Randevu
        {
            CalisanId = model.CalisanId,
            HizmetId = model.HizmetId,
            RandevuSaati = model.RandevuSaati.Date.Add(TimeSpan.Parse(Request.Form["Saat"]))
        };

        _context.Randevular.Add(yeniRandevu);
        _context.SaveChanges();
        return RedirectToAction("Basarili");
    }

    model.Subeler = _context.Subeler.ToList();
    model.Hizmetler = _context.Hizmetler.ToList();
    model.Calisanlar = _context.Calisanlar
        .Where(c => c.SubeId == model.SubeId).ToList();
    return View(model);
}
