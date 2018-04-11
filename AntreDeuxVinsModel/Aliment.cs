using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AntreDeuxVinsModel
{
    public class Aliment
    {
        public int Id { get; set; }
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(AntreDeuxVinsLanguages.Resources.ErrorMessageResource))]
        [Display(Name = "Nom", ResourceType = typeof(AntreDeuxVinsLanguages.Resources.ResourceModelAliment))]
        public string Nom { get; set; }
        [Display(Name = "Description", ResourceType = typeof(AntreDeuxVinsLanguages.Resources.ResourceModelAliment))]
        public string Description { get; set; }
        public ICollection<VinAliment> AlimentVins { get; set; }

        public Aliment()
        {

        }
    }
}
