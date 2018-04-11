using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AntreDeuxVins.Models
{
    public class AuthenticationViewModel
    {
        [Display(Name = "Email", ResourceType = typeof(AntreDeuxVinsLanguages.Resources.ResourceModelUtilisateur))]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(AntreDeuxVinsLanguages.Resources.ErrorMessageResource))]
        public string Mail { get; set; }

        [Display(Name = "Password", ResourceType = typeof(AntreDeuxVinsLanguages.Resources.ResourceModelUtilisateur))]
        [DataType(DataType.Password)]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(AntreDeuxVinsLanguages.Resources.ErrorMessageResource))]
        public string Password { get; set; }
        public Boolean IsPersistent { get; set; }
    }
}
