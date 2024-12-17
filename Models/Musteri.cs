using System.ComponentModel.DataAnnotations;

public class Musteri
{
    public int Id { get; set; }
    [Required]
    public string Ad { get; set; }
    [Required]
    public string soyAd { get; set; }
    [Required]
    public string Yas { get; set; }

    [Phone]
    public string telefonNo { get; set; }

    [EmailAddress]
    public string mailAdres { get; set; }

}
