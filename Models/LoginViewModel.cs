using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace BerberRandevuSitesi.Models
{
    public class LoginViewModel : IdentityUser
    {

    [Display(Name ="Eposta")]
    [EmailAddress(ErrorMessage ="Lütfen Geçerli Bir Eposta Adresi Giriniz.")]
    [Required(ErrorMessage ="Lütfen Eposta Adresinizi Giriniz.")]
    public string mailadress { get; set; }

    [Display(Name ="Sifre")]
    [Required(ErrorMessage ="Lütfen Şifre Giriniz")]
    [DataType(DataType.Password)]
    public string sifre {get; set;}

    public bool rememberme{get; set;} = true;
        
    }
}