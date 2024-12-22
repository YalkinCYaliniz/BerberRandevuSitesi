using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BerberRandevuSitesi.Data;
using BerberRandevuSitesi.Models;
using System.Linq;

namespace BerberRandevuSitesi.Controllers
{
    public class CalisanlarController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public CalisanlarController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Çalışanlar Index view'ını döndüren metod
        public IActionResult Index()
        {
            ViewData["IsAdminPage"] = true; 
            // Çalışanlar, her bir çalışanın bağlı olduğu şube, yetenekleri ve hizmetleriyle birlikte listeleniyor
            var calisanlar = _dbContext.Calisanlar
                .Include(c => c.Sube)  // Çalışanın şubesini dahil et
                .Include(c => c.CalisanYetenekler)
                .ThenInclude(cy => cy.Hizmet)  // Yeteneklerle ilişkili hizmetleri dahil et
                .ToList();

            return View(calisanlar);
        }
    }
}
 