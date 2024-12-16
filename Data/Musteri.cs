using System.ComponentModel.DataAnnotations;

namespace BerberRandevuSitesi.Data
{
    public class Musteri
    {
        //id ==>Primary Key
        [Key]
        public int MüsteriId { get; set; }
        public string MüsteriAd { get; set; }
        public string MüsteriSoyad { get; set; }

        public string Eposta { get; set; }

         public string? Telefon { get; set; }

    }
}