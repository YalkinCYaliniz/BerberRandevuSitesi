using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BerberRandevuSitesi.Models
{
    public class Calisanlar
     {
        [Key]
        public int CalisanId { get; set; }

        public string Adi { get; set; }

        public string Soyadi { get; set; }

        public int Maas { get; set; }

        // FOREIGN KEY 
        [ValidateNever]
        public int SubeId { get; set; }

        [ForeignKey("SubeId")]
        [ValidateNever]
        public Subeler Sube { get; set; }

        public ICollection<CalisanYetenek> CalisanYetenekler { get; set; }

    
    }
}
