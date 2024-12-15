public class RandevuViewModel
{
    public int SubeId { get; set; }
    public int CalisanId { get; set; }
    public int HizmetId { get; set; }
    public DateTime RandevuSaati { get; set; }

    public List<Sube> Subeler { get; set; }
    public List<Calisan> Calisanlar { get; set; }
    public List<Hizmet> Hizmetler { get; set; }
}
