using BerberRandevuSitesi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BerberRandevuSitesi.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Subeler> Subeler { get; set; }
        public DbSet<Calisanlar> Calisanlar { get; set; }
        public DbSet<Hizmetler> Hizmetler { get; set; }
        public DbSet<Randevu> Randevular { get; set; }

        public DbSet<CalisanYetenek> CalisanYetenekler { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
 
            base.OnModelCreating(modelBuilder);
 
            modelBuilder.Entity<ApplicationUser>()
                .Property(e => e.yas)
                .HasMaxLength(2);

            // Subeler ve Calisanlar ilişkisi (1-N)
            modelBuilder.Entity<Subeler>()
                .HasMany(s => s.Calisanlar)
                .WithOne(c => c.Sube)
                .HasForeignKey(c => c.SubeId)
                .OnDelete(DeleteBehavior.Cascade); // Şube silindiğinde çalışanlar da silinir.

            // Randevu ve Hizmetler ilişkisi (1-N)
            modelBuilder.Entity<Randevu>()
                .HasOne(r => r.Hizmet)
                .WithMany()
                .HasForeignKey(r => r.HizmetId);

            // Randevu ve Calisanlar ilişkisi (1-N)
            modelBuilder.Entity<Randevu>()
                .HasOne(r => r.Calisan)
                .WithMany()
                .HasForeignKey(r => r.CalisanId);

            // Randevu ve Subeler ilişkisi (1-N)
            modelBuilder.Entity<Randevu>()
                .HasOne(r => r.Sube)
                .WithMany()
                .HasForeignKey(r => r.SubeId);

            modelBuilder.Entity<CalisanYetenek>()
        .HasKey(cy => new { cy.CalisanId, cy.HizmetId }); // Bir çalışan birden fazla hizmet verebilir.

    modelBuilder.Entity<CalisanYetenek>()
        .HasOne(cy => cy.Calisan)
        .WithMany(c => c.CalisanYetenekler)
        .HasForeignKey(cy => cy.CalisanId)
        .OnDelete(DeleteBehavior.Cascade); // Çalışan silindiğinde, bu çalışanın yetenekleri de silinir.

    modelBuilder.Entity<CalisanYetenek>()
        .HasOne(cy => cy.Hizmet)
        .WithMany()
        .HasForeignKey(cy => cy.HizmetId)
        .OnDelete(DeleteBehavior.Restrict); // Hizmet silindiğinde, bu hizmeti kullanan çalışan yetenekleri etkilenmez.



            
        }
   

    


 }
}