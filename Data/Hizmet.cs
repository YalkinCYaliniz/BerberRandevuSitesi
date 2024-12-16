using System.ComponentModel.DataAnnotations;

namespace BerberRandevuSitesi.Data
{
    public class Hizmet
    {
        //id ==>Primary Key
        [Key]
        public int HizmetId { get; set; }

        public string HizmetAd {get; set; }
        

    }
}