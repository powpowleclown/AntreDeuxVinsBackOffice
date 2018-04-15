using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AntreDeuxVinsModel
{
    public class Pays
    {
        public int Id { get; set; }
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(AntreDeuxVinsLanguages.Resources.ErrorMessageResource))]
        [Display(Name = "Nom", ResourceType = typeof(AntreDeuxVinsLanguages.Resources.ResourceModelPays))]
        public string Nom { get; set; }
        [Display(Name = "Regions", ResourceType = typeof(AntreDeuxVinsLanguages.Resources.ResourceModelPays))]
        public ICollection<Region> Regions { get; set; }
        public Pays()
        {

        }
    }
}
