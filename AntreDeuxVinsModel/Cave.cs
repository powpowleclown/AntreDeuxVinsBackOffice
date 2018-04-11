using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AntreDeuxVinsModel
{
    public class Cave
    {
        public int Id { get; set; }
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(AntreDeuxVinsLanguages.Resources.ErrorMessageResource))]
        [Display(Name = "Nom", ResourceType = typeof(AntreDeuxVins.Resources.ResourceModelCave))]
        public string Nom { get; set; }
        [Display(Name = "Description", ResourceType = typeof(AntreDeuxVins.Resources.ResourceModelCave))]
        public string Description { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(AntreDeuxVinsLanguages.Resources.ErrorMessageResource))]
        public Guid UtilisateurId { get; set; }
        [Display(Name = "Utilisateur", ResourceType = typeof(AntreDeuxVins.Resources.ResourceModelCave))]
        public Utilisateur Utilisateur { get; set; }
        [Display(Name = "Vins", ResourceType = typeof(AntreDeuxVins.Resources.ResourceModelCave))]
        public ICollection<Vin> Vins { get; set; }

        public Cave()
        {

        }
    }
}
