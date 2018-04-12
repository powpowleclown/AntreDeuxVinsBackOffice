using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;
using System.Text;

namespace AntreDeuxVinsModel
{
    public class Utilisateur : IdentityUser<Guid>
    {
        [Display(Name = "Email", ResourceType = typeof(AntreDeuxVinsLanguages.Resources.ResourceModelUtilisateur))]
        [DataType(DataType.EmailAddress, ErrorMessageResourceName ="ErrorEmail",ErrorMessageResourceType = typeof(AntreDeuxVinsLanguages.Resources.ErrorMessageResource))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(AntreDeuxVinsLanguages.Resources.ErrorMessageResource))]
        public override string Email { get => base.Email; set => base.Email = value; }
        [Display(Name = "Nom", ResourceType = typeof(AntreDeuxVinsLanguages.Resources.ResourceModelUtilisateur))]
        public string Nom { get; set; }
        [Display(Name = "Prenom", ResourceType = typeof(AntreDeuxVinsLanguages.Resources.ResourceModelUtilisateur))]
        public string Prenom { get; set; }
        [Display(Name = "Password", ResourceType = typeof(AntreDeuxVinsLanguages.Resources.ResourceModelUtilisateur))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(AntreDeuxVinsLanguages.Resources.ErrorMessageResource))]
        [DataType(DataType.Password)]
        [NotMapped]
        public string Password { get; set; }
        [Display(Name = "ConfirmPassword", ResourceType = typeof(AntreDeuxVinsLanguages.Resources.ResourceModelUtilisateur))]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessageResourceName = "ConfirmPwd", ErrorMessageResourceType = typeof(AntreDeuxVinsLanguages.Resources.ErrorMessageResource))]
        [NotMapped]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(AntreDeuxVinsLanguages.Resources.ErrorMessageResource))]
        [NotMapped]
        public Guid RoleId { get; set; }
        [Display(Name = "Role", ResourceType = typeof(AntreDeuxVinsLanguages.Resources.ResourceModelUtilisateur))]
        [NotMapped]
        public Role Role { get; set; }
        public Utilisateur(string Mail, string Nom, string Prenom, string Password) : base(Mail)
        {
            base.Email = Mail;
            base.UserName = Mail;
            this.Nom = Nom;
            this.Prenom = Prenom;
            this.Password = Password;
        }
        public Utilisateur()
        {

        }
    }
}
