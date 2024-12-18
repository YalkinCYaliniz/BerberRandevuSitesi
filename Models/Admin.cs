using System.ComponentModel.DataAnnotations;

public class Admin
{
    [Key]
    public int id { get; set; }
  
    [Display(Name ="Eposta")]
    [EmailAddress(ErrorMessage ="Lütfen Geçerli Bir Eposta Adresi Giriniz.")]
    [Required(ErrorMessage ="Lütfen Eposta Adresinizi Giriniz.")]
    public string mailadress { get; set; }

    [Display(Name ="Sifre")]
    [Required(ErrorMessage ="Lütfen Şifre Giriniz")]
    
    public string sifre {get; set;}

}
