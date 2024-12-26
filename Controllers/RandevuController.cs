using Microsoft.AspNetCore.Mvc;
using BerberRandevuSitesi.Models;
using System.Linq;
using BerberRandevuSitesi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace BerberRandevuSitesi.Controllers
{
    [Authorize(Roles ="Admin")]
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
            ViewData["IsAdminPage"] = true;
            if (ModelState.IsValid)
            {
                randevu.musaitlik = true;
                _context.Randevular.Add(randevu);
                _context.SaveChanges();
                return RedirectToAction("Index");
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
                .OrderBy(r => r.Tarih)
                .ThenBy(r => r.Saat)
                .Where(r => r.SubeId != null) // Geçersiz değerleri filtreleyin
                .ToList();

            return View(randevular);
        }

        public IActionResult Delete(int id)
        {
            var randevu = _context.Randevular.Find(id);
            if (randevu != null)
            {
                 // İlgili kullanıcının randevu sayısını azalt
                var kullanici = _context.Users.FirstOrDefault(u => u.Id == randevu.ApplicationUserId);
                if (kullanici != null && kullanici.randevusayisi > 0) // Negatif randevu sayısını engelle
                {
                    kullanici.randevusayisi--;
                    _context.Users.Update(kullanici); // Kullanıcıyı güncelle
                }

                _context.Randevular.Remove(randevu);
                _context.SaveChanges();
            }
            return RedirectToAction("Index"); // "Index" action'ına yönlendir
        }

    }
}
