using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BerberRandevuSitesi.Models;

namespace BerberRandevuSitesi.Controllers
{
    public class RegisterController: Controller
    {
        public IActionResult Index(){

            return View();
        }
    }
}