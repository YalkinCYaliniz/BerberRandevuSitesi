using Microsoft.EntityFrameworkCore;

namespace BerberRandevuSitesi.Data
{
    public class DataContext: DbContext
    {
        public DbSet<Musteri> Musteriler { get; set; }
        public DbSet<Calisan> Calisanlar { get; set; }
        public DbSet<Randevu> Randevular { get; set; }
    }   
}
