using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace BerberRandevuSitesi.Models
{
    public class EditViewModel
    {
        public string Id {get; set;}
        [Display(Name ="Ad")]
        public string? ad { get; set; } 
        
        [Display(Name ="Soyad")]
        public string? soyad { get; set; }

        [Display(Name ="Yaş")]
        [Range(5,89,ErrorMessage ="Lütfen 5 ile 89 Arasında Bir Yaş Giriniz!")]
        public int? yas { get; set; }
        
        [Display(Name ="Telefon Numarası")]
        [Phone(ErrorMessage ="Lütfen Geçerli Bir Telefon Numarası Giriniz.")]
        [RegularExpression(@"^5\d{9}$", ErrorMessage = "Telefon numarası 5 ile başlamalı ve 10 haneli olmalıdır.")]
        public string? telefonno { get; set; } 
        
        [Display(Name ="Eposta")]
        [EmailAddress(ErrorMessage ="Lütfen Geçerli Bir Eposta Adresi Giriniz.")]
        public string? mailadress { get; set; }

        [Display(Name ="Sifre")]
        [DataType(DataType.Password)]
        public string? sifre {get; set;}

        [Display(Name ="Sifre Doğrula")]
        [Compare(nameof(sifre),ErrorMessage ="Şifreniz Eşleşmiyor!")]
        public string? sifredogrula {get; set;}
    }
}