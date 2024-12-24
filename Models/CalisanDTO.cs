using System.Collections.Generic;

namespace BerberRandevuSitesi.DTOs
{
    public class CalisanDTO
    {
        public int CalisanId { get; set; }
        public string Adi { get; set; }
        public string Soyadi { get; set; }
        public int Maas { get; set; }
        public int SubeId { get; set; }
        public List<CalisanYetenekDTO> CalisanYetenekler { get; set; }
    }

}
