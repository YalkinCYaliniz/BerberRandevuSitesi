using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BerberRandevuSitesi.Controllers
{
    public class HakkimizdaController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

 
    }
}