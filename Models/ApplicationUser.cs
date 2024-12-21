using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace BerberRandevuSitesi.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public int yas { get; set; }
        
    }
}