using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AntreDeuxVinsModel
{
    public class Couleur
    {
        public int Id { get; set; }
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(AntreDeuxVinsLanguages.Resources.ErrorMessageResource))]
        [Display(Name = "Nom", ResourceType = typeof(AntreDeuxVinsLanguages.Resources.ResourceModelCouleur))]
        public string Nom { get; set; }
        [Display(Name = "Vins", ResourceType = typeof(AntreDeuxVinsLanguages.Resources.ResourceModelCouleur))]
        public ICollection<Vin> Vins { get; set; }
        public Couleur()
        {

        }
    }
}
