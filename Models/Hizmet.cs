public class Hizmet
{
    public int Id { get; set; }
    public string Ad { get; set; }
    public ICollection<Calisan> Calisanlar { get; set; }
}
