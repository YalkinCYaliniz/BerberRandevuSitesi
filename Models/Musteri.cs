using System.ComponentModel.DataAnnotations;

public class Musteri
{
    [Key]
    public int id { get; set; }
    
    [Display(Name ="Ad")]
    [Required(ErrorMessage = "Lütfen Adınızı Giriniz")]
    public string ad { get; set; }
    
    [Display(Name ="Soyad")]
    [Required(ErrorMessage ="Lütfen Soyadınızı Giriniz.")]
    public string soyad { get; set; }
    
    [Display(Name ="Yaş")]
    [Required(ErrorMessage ="Lütfen Yaş Bilgisini Giriniz")]
    [Range(5,80,ErrorMessage ="Yaş Aralığı 5-80 Olmalıdır.")]
    public int yas { get; set; }
    
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
    
    public string sifre {get; set;}

}
