using System.ComponentModel.DataAnnotations;
public class Calisan
{
    public int Id { get; set; }

    [Required]
	public string Ad { get; set; }

    [Required]
    public int Yas{get;set;}
    [Required]
    public int Maas{get;set;}
    
    public int SubeId { get; set; }}
