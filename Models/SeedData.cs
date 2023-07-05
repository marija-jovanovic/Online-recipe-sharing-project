using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Recepti.Areas.Identity.Data;
using Recepti.Data;
using System.IO;

namespace Recepti.Models
{
    public class SeedData
    {

        public static async Task CreateUserRoles(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<ReceptiUser>>();
            string[] roleNames = { "Admin", "User" };
            IdentityResult roleResult;
            //Add Admin Role
            var roleCheck = await RoleManager.RoleExistsAsync("Admin");
            if (!roleCheck) { roleResult = await RoleManager.CreateAsync(new IdentityRole("Admin")); }
            ReceptiUser user = await UserManager.FindByEmailAsync("admin@workshopimproved.com");
            if (user == null)
            {
                var User = new ReceptiUser();
                User.Email = "admin@workshopimproved.com";
                User.UserName = "admin@workshopimproved.com";
                string userPWD = "Admin123";
                IdentityResult chkUser = await UserManager.CreateAsync(User, userPWD);
                //Add default User to Role Admin
                if (chkUser.Succeeded) { await UserManager.AddToRoleAsync(User, "Admin"); }

            }

            var roleCheck1 = await RoleManager.RoleExistsAsync("User");
            if (!roleCheck1) { roleResult = await RoleManager.CreateAsync(new IdentityRole("User")); }
            ReceptiUser user1 = await UserManager.FindByEmailAsync("linux@workshopimproved.com");
            if (user1 == null)
            {
                var User = new ReceptiUser();
                User.Email = "linux@workshopimproved.com";
                User.UserName = "linux@workshopimproved.com";
                string userPWD = "Linux123";
                IdentityResult chkUser = await UserManager.CreateAsync(User, userPWD);
                //Add default User to Role Admin
                if (chkUser.Succeeded) { await UserManager.AddToRoleAsync(User, "User"); }

            }


        }
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ReceptiContext(
            serviceProvider.GetRequiredService<
            DbContextOptions<ReceptiContext>>()))
            {
                CreateUserRoles(serviceProvider).Wait();

            }
        }
    }
}
