using BerberRandevuSitesi.Models;
using Microsoft.AspNetCore.Identity;

public class IdentitySeedData
{
    public static async Task Initialize(IServiceProvider serviceProvider, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
{
    var roleName = "Admin";
    var roleExist = await roleManager.RoleExistsAsync(roleName);
    if (!roleExist)
    {
        var roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
    }

    var user3 = await userManager.FindByEmailAsync("B221210101@sakarya.edu.tr");
    if (user3 == null)
    {
        user3 = new ApplicationUser
        {
            UserName = "Admin1",  
            Email = "B221210101@sakarya.edu.tr",
            PhoneNumber = "5320580805",
            yas = 20,
            ad = "Yalkın",
            soyad = "Yalınız"
        };
        
        // Admin için özel şifre (sau)
        var result = await userManager.CreateAsync(user3, "sau");  
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(user3, roleName);
        }
        else
        {
            // Hata mesajını loglama
            foreach (var error in result.Errors)
            {
                Console.WriteLine(error.Description);
            }
        }
    }

    var user4 = await userManager.FindByEmailAsync("B211210056@sakarya.edu.tr");
    if (user4 == null)
    {
        user4 = new ApplicationUser
        {
            UserName = "Admin2", 
            Email = "B211210056@sakarya.edu.tr",
            PhoneNumber = "5330911884",
            yas = 23,
            ad = "Furkan",
            soyad = "Bilen"
        };
        
        // Admin için özel şifre (sau) 
        var result = await userManager.CreateAsync(user4, "sau");  
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(user4, roleName);
        }
        else
        {
            // Hata mesajını loglama
            foreach (var error in result.Errors)
            {
                Console.WriteLine(error.Description);
            }
        }
    }
}

}

