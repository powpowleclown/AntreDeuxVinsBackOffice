using AntreDeuxVins.Data;
using AntreDeuxVinsModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AntreDeuxVins
{
    public class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            ILogger<AntreDeuxVinsDbContext> logger = serviceProvider.GetRequiredService<ILogger<AntreDeuxVinsDbContext>>();
            var context = serviceProvider.GetRequiredService<AntreDeuxVinsDbContext>();
            var usermanager = serviceProvider.GetRequiredService<UserManager<Utilisateur>>();
            var rolemanager = serviceProvider.GetRequiredService<RoleManager<Role>>();
            context.Database.EnsureCreated();
            if (!context.Role.Any())
            {
                var resultRoleAdmin = await rolemanager.CreateAsync(new Role("Admin"));
                var resultRoleUser = await rolemanager.CreateAsync(new Role("User"));
            }
            if (!context.Users.Any())
            {
                //ADMIN
                var admin = new Utilisateur("admin@admin.fr", "Admin", "Admin", "Admin123!");
                admin.Role = rolemanager.Roles.SingleOrDefault(r => r.Name == "Admin");
                var resultAdmin = await usermanager.CreateAsync(admin);
                if (resultAdmin.Succeeded)
                {
                    var resulAdminPwd = await usermanager.AddPasswordAsync(admin, admin.Password);
                    if(resulAdminPwd.Succeeded)
                    {
                        var resulAdminRole = await usermanager.AddToRoleAsync(admin, "Admin");
                    }
                }
                //USER
                var user = new Utilisateur("user@user.fr", "User", "User", "User123!");
                user.Role = rolemanager.Roles.SingleOrDefault(r => r.Name == "User");
                var resultUser = await usermanager.CreateAsync(user);
                if (resultUser.Succeeded)
                {
                    var resulUserPwd = await usermanager.AddPasswordAsync(user, user.Password);
                    if (resulUserPwd.Succeeded)
                    {
                        var resulUserRole = await usermanager.AddToRoleAsync(user, "User");
                    }
                }
            }
        }
    }
}
