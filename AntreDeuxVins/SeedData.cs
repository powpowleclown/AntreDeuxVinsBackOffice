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
            //LANGUAGE
            if (!context.Languages.Any())
            {
                await context.Languages.AddAsync(new Language { Name = "fr-FR", Code= "fr-FR" });
                await context.Languages.AddAsync(new Language { Name = "fr", Code = "fr" });
                await context.Languages.AddAsync(new Language { Name = "en-US", Code= "en-US" });
                await context.Languages.AddAsync(new Language { Name = "en", Code = "en" });
                await context.SaveChangesAsync();
            }
            //COULEUR
            if (!context.Couleurs.Any())
            {
                //TEST TRANSLATE
                //LANGUAGE
                var FRfr = context.Languages.SingleOrDefault(c => c.Name == "fr-FR");
                var fr = context.Languages.SingleOrDefault(c => c.Name == "fr");
                var ENen = context.Languages.SingleOrDefault(c => c.Name == "en-US");
                var en = context.Languages.SingleOrDefault(c => c.Name == "en");
                //LOCALIZABLE
                var resultLocalizable = await context.LocalizableEntitys.AddAsync(new LocalizableEntity { EntityName = "Couleur", PrimaryKeyFieldName= "Id"});
                await context.SaveChangesAsync();
                //ROUGE
                var resultCouleursRouge = await context.Couleurs.AddAsync(new Couleur { Nom = "Rouge" });
                await context.SaveChangesAsync();
                //LOCALIZABLE TRANSLATION ROUGE
                await context.LocalizableEntityTranslations.AddAsync(new LocalizableEntityTranslation { FieldName = "Nom", PrimaryKeyValue= resultCouleursRouge.Entity.Id, LanguageId = FRfr.Id, LocalizableEntityId= resultLocalizable.Entity.Id, Text="Rouge" });
                await context.LocalizableEntityTranslations.AddAsync(new LocalizableEntityTranslation { FieldName = "Nom", PrimaryKeyValue = resultCouleursRouge.Entity.Id, LanguageId = fr.Id, LocalizableEntityId = resultLocalizable.Entity.Id, Text = "Rouge" });
                await context.LocalizableEntityTranslations.AddAsync(new LocalizableEntityTranslation { FieldName = "Nom", PrimaryKeyValue = resultCouleursRouge.Entity.Id, LanguageId = ENen.Id, LocalizableEntityId = resultLocalizable.Entity.Id, Text = "Red" });
                await context.LocalizableEntityTranslations.AddAsync(new LocalizableEntityTranslation { FieldName = "Nom", PrimaryKeyValue = resultCouleursRouge.Entity.Id, LanguageId = en.Id, LocalizableEntityId = resultLocalizable.Entity.Id, Text = "Red" });
                await context.SaveChangesAsync();
                //BLANC
                var resultCouleursBlanc = await context.Couleurs.AddAsync(new Couleur { Nom = "Blanc" });
                await context.SaveChangesAsync();
                //LOCALIZABLE TRANSLATION BLANC
                await context.LocalizableEntityTranslations.AddAsync(new LocalizableEntityTranslation { FieldName = "Nom", PrimaryKeyValue = resultCouleursBlanc.Entity.Id, LanguageId = FRfr.Id, LocalizableEntityId = resultLocalizable.Entity.Id, Text = "Blanc" });
                await context.LocalizableEntityTranslations.AddAsync(new LocalizableEntityTranslation { FieldName = "Nom", PrimaryKeyValue = resultCouleursBlanc.Entity.Id, LanguageId = fr.Id, LocalizableEntityId = resultLocalizable.Entity.Id, Text = "Blanc" });
                await context.LocalizableEntityTranslations.AddAsync(new LocalizableEntityTranslation { FieldName = "Nom", PrimaryKeyValue = resultCouleursBlanc.Entity.Id, LanguageId = ENen.Id, LocalizableEntityId = resultLocalizable.Entity.Id, Text = "White" });
                await context.LocalizableEntityTranslations.AddAsync(new LocalizableEntityTranslation { FieldName = "Nom", PrimaryKeyValue = resultCouleursBlanc.Entity.Id, LanguageId = en.Id, LocalizableEntityId = resultLocalizable.Entity.Id, Text = "White" });
                await context.SaveChangesAsync();

                var resultCouleursRose = await context.Couleurs.AddAsync(new Couleur { Nom = "Rose" });
                await context.SaveChangesAsync();
                //LOCALIZABLE TRANSLATION ROSE
                await context.LocalizableEntityTranslations.AddAsync(new LocalizableEntityTranslation { FieldName = "Nom", PrimaryKeyValue = resultCouleursRose.Entity.Id, LanguageId = FRfr.Id, LocalizableEntityId = resultLocalizable.Entity.Id, Text = "Rose" });
                await context.LocalizableEntityTranslations.AddAsync(new LocalizableEntityTranslation { FieldName = "Nom", PrimaryKeyValue = resultCouleursRose.Entity.Id, LanguageId = fr.Id, LocalizableEntityId = resultLocalizable.Entity.Id, Text = "Rose" });
                await context.LocalizableEntityTranslations.AddAsync(new LocalizableEntityTranslation { FieldName = "Nom", PrimaryKeyValue = resultCouleursRose.Entity.Id, LanguageId = ENen.Id, LocalizableEntityId = resultLocalizable.Entity.Id, Text = "Pink" });
                await context.LocalizableEntityTranslations.AddAsync(new LocalizableEntityTranslation { FieldName = "Nom", PrimaryKeyValue = resultCouleursRose.Entity.Id, LanguageId = en.Id, LocalizableEntityId = resultLocalizable.Entity.Id, Text = "Pink" });
                await context.SaveChangesAsync();
            }
            //PAYS
            if (!context.Pays.Any())
            {
                //TEST TRANSLATE
                //LANGUAGE
                var FRfr = context.Languages.SingleOrDefault(c => c.Name == "fr-FR");
                var fr = context.Languages.SingleOrDefault(c => c.Name == "fr");
                var ENen = context.Languages.SingleOrDefault(c => c.Name == "en-US");
                var en = context.Languages.SingleOrDefault(c => c.Name == "en");
                //LOCALIZABLE
                var resultLocalizable = await context.LocalizableEntitys.AddAsync(new LocalizableEntity { EntityName = "Pays", PrimaryKeyFieldName = "Id" });
                await context.SaveChangesAsync();
                //FRANCE
                var resultPaysFrance = await context.Pays.AddAsync(new Pays { Nom = "France" });
                await context.SaveChangesAsync();
                //LOCALIZABLE TRANSLATION FRANCE
                await context.LocalizableEntityTranslations.AddAsync(new LocalizableEntityTranslation { FieldName = "Nom", PrimaryKeyValue = resultPaysFrance.Entity.Id, LanguageId = FRfr.Id, LocalizableEntityId = resultLocalizable.Entity.Id, Text = "France" });
                await context.LocalizableEntityTranslations.AddAsync(new LocalizableEntityTranslation { FieldName = "Nom", PrimaryKeyValue = resultPaysFrance.Entity.Id, LanguageId = fr.Id, LocalizableEntityId = resultLocalizable.Entity.Id, Text = "France" });
                await context.LocalizableEntityTranslations.AddAsync(new LocalizableEntityTranslation { FieldName = "Nom", PrimaryKeyValue = resultPaysFrance.Entity.Id, LanguageId = ENen.Id, LocalizableEntityId = resultLocalizable.Entity.Id, Text = "France" });
                await context.LocalizableEntityTranslations.AddAsync(new LocalizableEntityTranslation { FieldName = "Nom", PrimaryKeyValue = resultPaysFrance.Entity.Id, LanguageId = en.Id, LocalizableEntityId = resultLocalizable.Entity.Id, Text = "France" });
                await context.SaveChangesAsync();

                //ITALIE
                var resultPaysItalie = await context.Pays.AddAsync(new Pays { Nom = "Italie" });
                await context.SaveChangesAsync();
                //LOCALIZABLE TRANSLATION ITALIE
                await context.LocalizableEntityTranslations.AddAsync(new LocalizableEntityTranslation { FieldName = "Nom", PrimaryKeyValue = resultPaysItalie.Entity.Id, LanguageId = FRfr.Id, LocalizableEntityId = resultLocalizable.Entity.Id, Text = "Italie" });
                await context.LocalizableEntityTranslations.AddAsync(new LocalizableEntityTranslation { FieldName = "Nom", PrimaryKeyValue = resultPaysItalie.Entity.Id, LanguageId = fr.Id, LocalizableEntityId = resultLocalizable.Entity.Id, Text = "Italie" });
                await context.LocalizableEntityTranslations.AddAsync(new LocalizableEntityTranslation { FieldName = "Nom", PrimaryKeyValue = resultPaysItalie.Entity.Id, LanguageId = ENen.Id, LocalizableEntityId = resultLocalizable.Entity.Id, Text = "Italy" });
                await context.LocalizableEntityTranslations.AddAsync(new LocalizableEntityTranslation { FieldName = "Nom", PrimaryKeyValue = resultPaysItalie.Entity.Id, LanguageId = en.Id, LocalizableEntityId = resultLocalizable.Entity.Id, Text = "Italy" });
                await context.SaveChangesAsync();

            }
            //REGIONS
            if (!context.Regions.Any())
            {
                //FRANCE
                var resultPaysFrance = context.Pays.SingleOrDefault(c => c.Nom == "France");

                await context.Regions.AddAsync(new Region { Nom = "Bordeaux", PaysId = resultPaysFrance.Id });
                await context.Regions.AddAsync(new Region { Nom = "Aix", PaysId = resultPaysFrance.Id });
                //ITALIE
                var resultPaysItalie = context.Pays.SingleOrDefault(c => c.Nom == "Italie");

                await context.Regions.AddAsync(new Region { Nom = "Rome", PaysId = resultPaysItalie.Id });
                await context.Regions.AddAsync(new Region { Nom = "Venise", PaysId = resultPaysItalie.Id });
                await context.SaveChangesAsync();
            }
            //ALIMENTS
            if (!context.Aliments.Any())
            {
                //TEST TRANSLATE
                //LANGUAGE
                var FRfr = context.Languages.SingleOrDefault(c => c.Name == "fr-FR");
                var fr = context.Languages.SingleOrDefault(c => c.Name == "fr");
                var ENen = context.Languages.SingleOrDefault(c => c.Name == "en-US");
                var en = context.Languages.SingleOrDefault(c => c.Name == "en");
                //LOCALIZABLE
                var resultLocalizable = await context.LocalizableEntitys.AddAsync(new LocalizableEntity { EntityName = "Aliment", PrimaryKeyFieldName = "Id" });
                await context.SaveChangesAsync();
                //VIANDE ROUGE
                var resultAlimentsViandeRouge = await context.Aliments.AddAsync(new Aliment { Nom = "Viande Rouge", Description = "Viande Rouge"});
                await context.SaveChangesAsync();
                //LOCALIZABLE TRANSLATION VIANDE ROUGE
                await context.LocalizableEntityTranslations.AddAsync(new LocalizableEntityTranslation { FieldName = "Nom", PrimaryKeyValue = resultAlimentsViandeRouge.Entity.Id, LanguageId = FRfr.Id, LocalizableEntityId = resultLocalizable.Entity.Id, Text = "Viande Rouge" });
                await context.LocalizableEntityTranslations.AddAsync(new LocalizableEntityTranslation { FieldName = "Nom", PrimaryKeyValue = resultAlimentsViandeRouge.Entity.Id, LanguageId = fr.Id, LocalizableEntityId = resultLocalizable.Entity.Id, Text = "Viande Rouge" });
                await context.LocalizableEntityTranslations.AddAsync(new LocalizableEntityTranslation { FieldName = "Nom", PrimaryKeyValue = resultAlimentsViandeRouge.Entity.Id, LanguageId = ENen.Id, LocalizableEntityId = resultLocalizable.Entity.Id, Text = "Red Meat" });
                await context.LocalizableEntityTranslations.AddAsync(new LocalizableEntityTranslation { FieldName = "Nom", PrimaryKeyValue = resultAlimentsViandeRouge.Entity.Id, LanguageId = en.Id, LocalizableEntityId = resultLocalizable.Entity.Id, Text = "Red Meat" });
                await context.SaveChangesAsync();

                //VIANDE BLANCHE
                var resultAlimentsViandeBlanche = await context.Aliments.AddAsync(new Aliment { Nom = "Viande Blanche", Description = "Viande Blanche" });
                await context.SaveChangesAsync();
                //LOCALIZABLE TRANSLATION VIANDE BLANCHE
                await context.LocalizableEntityTranslations.AddAsync(new LocalizableEntityTranslation { FieldName = "Nom", PrimaryKeyValue = resultAlimentsViandeBlanche.Entity.Id, LanguageId = FRfr.Id, LocalizableEntityId = resultLocalizable.Entity.Id, Text = "Viande Blanche" });
                await context.LocalizableEntityTranslations.AddAsync(new LocalizableEntityTranslation { FieldName = "Nom", PrimaryKeyValue = resultAlimentsViandeBlanche.Entity.Id, LanguageId = fr.Id, LocalizableEntityId = resultLocalizable.Entity.Id, Text = "Viande Blanche" });
                await context.LocalizableEntityTranslations.AddAsync(new LocalizableEntityTranslation { FieldName = "Nom", PrimaryKeyValue = resultAlimentsViandeBlanche.Entity.Id, LanguageId = ENen.Id, LocalizableEntityId = resultLocalizable.Entity.Id, Text = "White Meat" });
                await context.LocalizableEntityTranslations.AddAsync(new LocalizableEntityTranslation { FieldName = "Nom", PrimaryKeyValue = resultAlimentsViandeBlanche.Entity.Id, LanguageId = en.Id, LocalizableEntityId = resultLocalizable.Entity.Id, Text = "White Meat" });
                await context.SaveChangesAsync();

                //FROMAGE
                var resultAlimentsFromage = await context.Aliments.AddAsync(new Aliment { Nom = "Fromage", Description = "Fromage" });
                await context.SaveChangesAsync();

                //LOCALIZABLE TRANSLATION FROMAGE
                await context.LocalizableEntityTranslations.AddAsync(new LocalizableEntityTranslation { FieldName = "Nom", PrimaryKeyValue = resultAlimentsFromage.Entity.Id, LanguageId = FRfr.Id, LocalizableEntityId = resultLocalizable.Entity.Id, Text = "Fromage" });
                await context.LocalizableEntityTranslations.AddAsync(new LocalizableEntityTranslation { FieldName = "Nom", PrimaryKeyValue = resultAlimentsFromage.Entity.Id, LanguageId = fr.Id, LocalizableEntityId = resultLocalizable.Entity.Id, Text = "Fromage" });
                await context.LocalizableEntityTranslations.AddAsync(new LocalizableEntityTranslation { FieldName = "Nom", PrimaryKeyValue = resultAlimentsFromage.Entity.Id, LanguageId = ENen.Id, LocalizableEntityId = resultLocalizable.Entity.Id, Text = "Cheese" });
                await context.LocalizableEntityTranslations.AddAsync(new LocalizableEntityTranslation { FieldName = "Nom", PrimaryKeyValue = resultAlimentsFromage.Entity.Id, LanguageId = en.Id, LocalizableEntityId = resultLocalizable.Entity.Id, Text = "Cheese" });
                await context.SaveChangesAsync();
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
                await context.SaveChangesAsync();
            }
        }
    }
}
