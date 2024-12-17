using System.ComponentModel.DataAnnotations;

public class Musteri
{
    public int Id { get; set; }
    
    [Display(Name ="Ad")]
    [Required(ErrorMessage = "Lütfen Adınızı Giriniz")]
    public string Ad { get; set; }
    
    [Display(Name ="Soyad")]
    [Required(ErrorMessage ="Lütfen Soyadınızı Giriniz.")]
    public string soyAd { get; set; }
    
    [Display(Name ="Yaş")]
    [Required(ErrorMessage ="Lütfen Yaş Bilgisini Giriniz")]
    [Range(5,80,ErrorMessage ="Yaş Aralığı 5-80 Olmalıdır.")]
    public string Yas { get; set; }
   
    [Display(Name ="Telefon Numarası")]
    [Phone(ErrorMessage ="Lütfen Geçerli Bir Telefon Numarası Giriniz.")]
    [RegularExpression(@"^5\d{9}$", ErrorMessage = "Telefon numarası 5 ile başlamalı ve 10 haneli olmalıdır.")]
    public string? telefonNo { get; set; }
    
    [Display(Name ="Eposta")]
    [EmailAddress(ErrorMessage ="Lütfen Geçerli Bir Eposta Adresi Giriniz.")]
    [Required(ErrorMessage ="Lütfen Eposta Adresinizi Giriniz.")]
    public string mailadress { get; set; }

    [Display(Name ="Sifre")]
    [Required(ErrorMessage ="Lütfen Şifre Giriniz")]
    public string sifre {get; set;}

}
