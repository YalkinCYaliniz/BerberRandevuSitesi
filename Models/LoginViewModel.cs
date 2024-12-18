using System.ComponentModel.DataAnnotations;

public class LoginViewModel
{
    [Required(ErrorMessage = "E-posta adresinizi giriniz.")]
    [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
    public string mailadress { get; set; }

    [Required(ErrorMessage = "Şifrenizi giriniz.")]
    public string sifre { get; set; }
}
