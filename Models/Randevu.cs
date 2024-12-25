using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BerberRandevuSitesi.Models{

public class Randevu 
{
    [Key]
    public int ID { get; set; }

    [Required]
    [DataType(DataType.Date)]
    [CustomValidation(typeof(Randevu), nameof(ValidateTarih))]
    
    public DateOnly Tarih { get; set; }

    [Required]
    [CustomValidation(typeof(Randevu), nameof(ValidateSaat))]
    public string Saat { get; set; }

    public int? SubeId { get; set; }
    [ForeignKey("SubeId")]
    [ValidateNever]
    public Subeler Sube { get; set; }

    [ValidateNever]
    public int? HizmetId { get; set; }
    [ForeignKey("HizmetId")]
    [ValidateNever]
    public Hizmetler Hizmet { get; set; }

    [ValidateNever]
    public int? CalisanId { get; set; }
    [ForeignKey("CalisanId")]
    [ValidateNever]
    public Calisanlar Calisan { get; set; }

    public bool musaitlik { get; set; }

    public string? ApplicationUserId { get; set; }
    [ForeignKey("ApplicationUserId")]
    [ValidateNever]
    public ApplicationUser ApplicationUser { get; set; }

    [NotMapped]
public DateTime TarihAsDateTime
{
    get => Tarih.ToDateTime(TimeOnly.MinValue);
    set => Tarih = DateOnly.FromDateTime(value);
}


    public static ValidationResult? ValidateTarih(DateOnly tarih, ValidationContext context)
{
    var today = DateOnly.FromDateTime(DateTime.Now.Date); // Bugünün tarihi
    if (tarih < today)
    {
        return new ValidationResult("Randevu tarihi bugünden ileri bir tarih olmalıdır.");
    }
    return ValidationResult.Success;
}



    public static ValidationResult? ValidateSaat(string saat, ValidationContext context)
    {
        if (TimeSpan.TryParse(saat, out TimeSpan time))
        {
            if (time < TimeSpan.FromHours(9) || time > TimeSpan.FromHours(20))
            {
                return new ValidationResult("Randevu saati 09:00 ile 20:00 arasında olmalıdır.");
            }
        }
        else
        {
            return new ValidationResult("Saat formatı geçerli değil.");
        }
        return ValidationResult.Success;
    }

}
}


