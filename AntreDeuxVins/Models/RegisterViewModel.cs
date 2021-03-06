﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AntreDeuxVins.Models
{
    public class RegisterViewModel
    {
        [Display(Name = "Email", ResourceType = typeof(AntreDeuxVinsLanguages.Resources.ResourceModelUtilisateur))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(AntreDeuxVinsLanguages.Resources.ErrorMessageResource))]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Nom", ResourceType = typeof(AntreDeuxVinsLanguages.Resources.ResourceModelUtilisateur))]
        public string Nom { get; set; }
        [Required]
        [Display(Name = "Prenom", ResourceType = typeof(AntreDeuxVinsLanguages.Resources.ResourceModelUtilisateur))]
        public string Prenom { get; set; }
        [Display(Name = "Password", ResourceType = typeof(AntreDeuxVinsLanguages.Resources.ResourceModelUtilisateur))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(AntreDeuxVinsLanguages.Resources.ErrorMessageResource))]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "ConfirmPassword", ResourceType = typeof(AntreDeuxVinsLanguages.Resources.ResourceModelUtilisateur))]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessageResourceName = "ConfirmPwd", ErrorMessageResourceType = typeof(AntreDeuxVinsLanguages.Resources.ErrorMessageResource))]
        public string ConfirmPassword { get; set; }
    }
}
