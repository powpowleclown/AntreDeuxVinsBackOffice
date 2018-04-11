using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AntreDeuxVinsModel
{
    public class Region
    {
        public int Id { get; set; }
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(AntreDeuxVinsLanguages.Resources.ErrorMessageResource))]
        [Display(Name = "Nom", ResourceType = typeof(AntreDeuxVinsLanguages.Resources.ResourceModelRegion))]
        public string Nom { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(AntreDeuxVinsLanguages.Resources.ErrorMessageResource))]
        public int PaysId { get; set; }
        [Display(Name = "Pays", ResourceType = typeof(AntreDeuxVinsLanguages.Resources.ResourceModelRegion))]
        public Pays Pays { get; set; }
        public Region()
        {

        }
    }
}
