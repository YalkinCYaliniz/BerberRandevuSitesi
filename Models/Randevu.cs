using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BerberRandevuSitesi.Models
{
    public class Randevu 
    {
        [Key]
        public int ID {get; set;}
        public DateOnly Tarih{get;set;}

        public string Saat{get; set;} 

        public int SubeId{get; set;}
        [ForeignKey("SubeId")]
        public Subeler Sube { get; set; } // Şube ilişkisi doğru

        //FOREİGN KEY İLİŞİKSİ
		[ValidateNever]
        public int HizmetId { get; set; }
        [ForeignKey("HizmetId")]
        [ValidateNever]
        public Hizmetler Hizmet { get; set; }

        [ValidateNever]
        public int CalisanId { get; set; }
        [ForeignKey("CalisanId")]
        [ValidateNever]
        public Calisanlar Calisan { get; set; }

        public bool musaitlik { get; set; }

        [ValidateNever]
        public string? ApplicationUserId { get; set; } // IdentityUser'dan gelen Id
            [ForeignKey("ApplicationUserId")]
        public ApplicationUser ApplicationUser { get; set; }

    }
}