using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AntreDeuxVinsModel
{
    public class Role : IdentityRole<Guid>
    {
        [Display(Name = "Nom", ResourceType = typeof(AntreDeuxVinsLanguages.Resources.ResourceModelRole))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(AntreDeuxVinsLanguages.Resources.ErrorMessageResource))]
        public override string Name { get => base.Name; set => base.Name = value; }
        [Display(Name = "Utilisateurs", ResourceType = typeof(AntreDeuxVinsLanguages.Resources.ResourceModelRole))]
        public ICollection<Utilisateur> Utilisateurs { get; set; }
        public Role(string name) : base(name)
        {
            base.Name = name;
        }
        public Role()
        {

        }
        
    }
}
