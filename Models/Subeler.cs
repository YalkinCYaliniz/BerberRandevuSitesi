using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BerberRandevuSitesi.Models
{
    public class Subeler
    {   
        [Key]
        public int Id { get; set; }

        public string SubeAdi { get; set; }

        // Şubeye ait çalışanlar listesi
        public List<Calisanlar> Calisanlar { get; set; }
    }
}
