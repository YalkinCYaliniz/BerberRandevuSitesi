using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BerberRandevuSitesi.Models
{
    public class CalisanYetenek
    {

        // FOREIGN KEY - Çalışan
        [ForeignKey("CalisanId")]
        public int CalisanId { get; set; }
        public Calisanlar Calisan { get; set; }
        [ValidateNever]
        
        public int HizmetId { get; set; }

        // FOREIGN KEY - Hizmet
        [ForeignKey("HizmetId")]
        public Hizmetler Hizmet { get; set; }

        [NotMapped] 
        public string HizmetAdi => Hizmet?.HizmetAdi;



    }
}
