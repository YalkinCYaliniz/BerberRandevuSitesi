using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BerberRandevuSitesi.Controllers
{
    public class UserController:Controller
    {
        private UserManager<IdentityUser> _userManager;
        public UserController(UserManager<IdentityUser> userManager){

            _userManager = userManager;
        }
        
        public IActionResult Index(){

            return View(_userManager.Users);
        }
        
    }
}