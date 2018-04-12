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
            //COULEUR
            if (!context.Couleurs.Any())
            {
                var resultCouleursRouge = await context.Couleurs.AddAsync(new Couleur { Nom = "Rouge" });
                var resultCouleursBlanc = await context.Couleurs.AddAsync(new Couleur { Nom = "Blanc" });
                var resultCouleursRose = await context.Couleurs.AddAsync(new Couleur { Nom = "Rose" });
            }
            //PAYS
            if (!context.Pays.Any())
            {
                var resultPaysFrance = await context.Pays.AddAsync(new Pays { Nom = "France" });
                var resultPaysItalie = await context.Pays.AddAsync(new Pays { Nom = "Italie" });
                //REGIONS
                if (!context.Regions.Any())
                {
                    //FRANCE
                    var resultRegionsFranceBordeaux = await context.Regions.AddAsync(new Region { Nom = "Bordeaux", PaysId = resultPaysFrance.Entity.Id });
                    var resultRegionsFranceAix = await context.Regions.AddAsync(new Region { Nom = "Aix", PaysId = resultPaysFrance.Entity.Id });
                    //ITALIE
                    var resultRegionsItalieRome = await context.Regions.AddAsync(new Region { Nom = "Rome", PaysId = resultRegionsFranceAix.Entity.Id });
                    var resultRegionsItalieVenise = await context.Regions.AddAsync(new Region { Nom = "Venise", PaysId = resultRegionsFranceAix.Entity.Id });
                }
            }
            //ALIMENTS
            if (!context.Aliments.Any())
            {
                var resultAlimentsViandeRouge = await context.Aliments.AddAsync(new Aliment { Nom = "Viande Rouge", Description = "Viande Rouge"});
                var resultAlimentsViandeBlanche = await context.Aliments.AddAsync(new Aliment { Nom = "Viande Blanche", Description = "Viande Blanche" });
                var resultAlimentsFromage = await context.Aliments.AddAsync(new Aliment { Nom = "Fromage", Description = "Fromage" });
            }
            //ROLE
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
                var user = new Utilisateur("bernard.dupond@gmail.fr", "Dupond", "Bernard", "Dupond123!");
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
