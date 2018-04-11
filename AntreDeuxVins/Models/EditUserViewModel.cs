using AntreDeuxVinsModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AntreDeuxVins.Models
{
    public class EditUserViewModel
    {
        public Guid Id { get; set; }
        [Display(Name = "Email", ResourceType = typeof(AntreDeuxVinsLanguages.Resources.ResourceModelUtilisateur))]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(AntreDeuxVinsLanguages.Resources.ErrorMessageResource))]
        public string Email { get; set; }
        [Display(Name = "Nom", ResourceType = typeof(AntreDeuxVinsLanguages.Resources.ResourceModelUtilisateur))]
        public string Nom { get; set; }
        [Display(Name = "Prenom", ResourceType = typeof(AntreDeuxVinsLanguages.Resources.ResourceModelUtilisateur))]
        public string Prenom { get; set; }
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(AntreDeuxVinsLanguages.Resources.ErrorMessageResource))]
        public int RoleId { get; set; }
        [Display(Name = "Role", ResourceType = typeof(AntreDeuxVinsLanguages.Resources.ResourceModelUtilisateur))]
        public Role Role { get; set; }
    }
}
