using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace BerberRandevuSitesi.Models
{
    public class CreateViewModel : IdentityUser
    {
    [Display(Name ="Ad")]
    [Required(ErrorMessage = "Lütfen Adınızı Giriniz")]
    public string ad { get; set; }
    
    [Display(Name ="Soyad")]
    [Required(ErrorMessage ="Lütfen Soyadınızı Giriniz.")]
    public string soyad { get; set; }
    
    [Display(Name ="Telefon Numarası")]
    [Required(ErrorMessage ="Lütfen Telefon Numaranızı Giriniz")]
    [Phone(ErrorMessage ="Lütfen Geçerli Bir Telefon Numarası Giriniz.")]
    [RegularExpression(@"^5\d{9}$", ErrorMessage = "Telefon numarası 5 ile başlamalı ve 10 haneli olmalıdır.")]
    public string? telefonno { get; set; }
    
    [Display(Name ="Eposta")]
    [EmailAddress(ErrorMessage ="Lütfen Geçerli Bir Eposta Adresi Giriniz.")]
    [Required(ErrorMessage ="Lütfen Eposta Adresinizi Giriniz.")]
    public string mailadress { get; set; }

    [Display(Name ="Sifre")]
    [Required(ErrorMessage ="Lütfen Şifre Giriniz")]
    [DataType(DataType.Password)]
    public string sifre {get; set;}

    [Display(Name ="Sifre Doğrula")]
    [Required(ErrorMessage ="Lütfen Şifre Giriniz")]
    [Compare(nameof(sifre),ErrorMessage ="Şifreniz Eşleşmiyor!")]
    public string sifredogrula {get; set;}







    }
}