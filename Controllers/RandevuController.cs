using Microsoft.AspNetCore.Mvc;
using BerberRandevuSitesi.Models;
using System.Linq;
using BerberRandevuSitesi.Data;
using Microsoft.EntityFrameworkCore;

namespace BerberRandevuSitesi.Controllers
{
    public class RandevuController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RandevuController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
    public IActionResult Create()
    {
    ViewData["IsAdminPage"] = true;
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Randevu randevu)
    {

        if (ModelState.IsValid)
        {
            _context.Randevular.Add(randevu);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        if (!ModelState.IsValid)
{
    foreach (var state in ModelState)
    {
        Console.WriteLine($"Field: {state.Key}, Errors: {string.Join(", ", state.Value.Errors.Select(e => e.ErrorMessage))}");
    }
}
        foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
    {
        Console.WriteLine($"Hata: {error.ErrorMessage}");
    }
        return View(randevu);
    }

    public IActionResult Index()
{
ViewData["IsAdminPage"] = true;
    var randevular = _context.Randevular
        .Include(r => r.Sube)
        .Include(r => r.Calisan)
        .Include(r => r.Hizmet)
        .Include(r => r.ApplicationUser)
        .Where(r => r.SubeId != null) // Geçersiz değerleri filtreleyin
        .ToList();

    return View(randevular);
}


        public IActionResult Delete(int id)
{
    var randevu = _context.Randevular.Find(id);
    if (randevu != null)
    {
        _context.Randevular.Remove(randevu);
        _context.SaveChanges();
    }
    return RedirectToAction("Index"); // "Index" action'ına yönlendir
}

    }
}
