using Microsoft.EntityFrameworkCore;

public class RandevuDbContext : DbContext
{
    public RandevuDbContext(DbContextOptions<RandevuDbContext> options) : base(options) { }

    public DbSet<Calisan> Calisanlar { get; set; }
    public DbSet<Hizmet> Hizmetler { get; set; }
    public DbSet<Sube> Subeler { get; set; }
    public DbSet<Randevu> Randevular { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Calisan>()
            .HasMany(c => c.Hizmetler)
            .WithMany(h => h.Calisanlar);

        modelBuilder.Entity<Sube>().HasData(
            new Sube { Id = 1, Ad = "Serdivan" },
            new Sube { Id = 2, Ad = "Adapazarı" }
        );

        modelBuilder.Entity<Calisan>().HasData(
            // Serdivan
            new Calisan { Id = 1, AdSoyad = "Mert Duran", SubeId = 1 },
            new Calisan { Id = 2, AdSoyad = "Mehmet Çağlar", SubeId = 1 },
            new Calisan { Id = 3, AdSoyad = "Kemal Selim", SubeId = 1 },
            new Calisan { Id = 4, AdSoyad = "Alp Türk", SubeId = 1 },
            // Adapazarı
            new Calisan { Id = 5, AdSoyad = "Emin Gezgin", SubeId = 2 },
            new Calisan { Id = 6, AdSoyad = "Hasan Korkmaz", SubeId = 2 },
            new Calisan { Id = 7, AdSoyad = "Hilmi Gök", SubeId = 2 },
            new Calisan { Id = 8, AdSoyad = "Samet Dingin", SubeId = 2 }
        );

        modelBuilder.Entity<Hizmet>().HasData(
            new Hizmet { Id = 1, Ad = "Saç Kesimi" },
            new Hizmet { Id = 2, Ad = "Saç Boyama" },
            new Hizmet { Id = 3, Ad = "Sakal Tıraşı" },
            new Hizmet { Id = 4, Ad = "Cilt Bakımı" }
        );
    }
}
