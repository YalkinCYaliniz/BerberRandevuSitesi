using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BerberRandevuSitesi.Models
{
    public class Hizmetler
    {
        [Key]
        public int ID{get; set;}

        public string HizmetAdi{get;set;}
    }
}