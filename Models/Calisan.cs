public class Calisan
{
    public int Id { get; set; }
    public string AdSoyad { get; set; }
    public int SubeId { get; set; }
    public Sube Sube { get; set; }
    public ICollection<Hizmet> Hizmetler { get; set; }
}
