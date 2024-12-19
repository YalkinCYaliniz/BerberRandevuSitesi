
using BerberRandevuSitesi.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BerberRandevuSitesi.Models
{

    
    public class IdentitySeedData{
    private const string adminUser = "Admin";
    private const string adminPassword = "sau";
        public static async void IdentityTestUser(IApplicationBuilder app){

        var context = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<ApplicationDbContext>();

        if(context.Database.GetAppliedMigrations().Any()){

            context.Database.Migrate();
        }

        var userManager = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
        var user =  await userManager.FindByNameAsync(adminUser);

        if(user == null) {

            user = new IdentityUser{
                UserName = adminUser,
                Email = "B221210101@sakarya.edu.tr",
                PhoneNumber = "5320580805"

            };
            await userManager.CreateAsync(user,adminPassword);
        }
    }

    }
    
}

