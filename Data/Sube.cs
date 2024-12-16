using System.ComponentModel.DataAnnotations;

namespace BerberRandevuSitesi.Data;
{
    public class Sube
    {
        [Key]
        public int SubeId { get; set; }
        public string SubeAd { get; set; }
        public ICollection<Calisan> Calisanlar { get; set; }
    }
}
