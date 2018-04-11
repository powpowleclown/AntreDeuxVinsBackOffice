using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AntreDeuxVins.Models
{
    public class PwdViewModel
    {
        public Guid Id { get; set; }
        [Display(Name = "Password", ResourceType = typeof(AntreDeuxVinsLanguages.Resources.ResourceModelUtilisateur))]
        [DataType(DataType.Password)]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(AntreDeuxVinsLanguages.Resources.ErrorMessageResource))]
        public string Password { get; set; }
        [Display(Name = "ConfirmPassword", ResourceType = typeof(AntreDeuxVinsLanguages.Resources.ResourceModelUtilisateur))]
        [Compare("Password", ErrorMessageResourceName = "ConfirmPwd", ErrorMessageResourceType = typeof(AntreDeuxVinsLanguages.Resources.ErrorMessageResource))]
        [DataType(DataType.Password)]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(AntreDeuxVinsLanguages.Resources.ErrorMessageResource))]
        public string ConfirmPassword { get; set; }
    }
}
