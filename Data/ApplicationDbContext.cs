using Microsoft.EntityFrameworkCore;
using BerberRandevuSitesi.Models;

namespace BerberRandevuSitesi.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<Musteri> musteri { get; set; }
        // Diğer DbSet'lerinizi burada ekleyebilirsiniz
    }
}
