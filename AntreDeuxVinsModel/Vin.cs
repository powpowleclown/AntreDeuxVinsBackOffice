using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AntreDeuxVinsModel
{
    public class Vin
    {
        public int Id { get; set; }
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(AntreDeuxVinsLanguages.Resources.ErrorMessageResource))]
        [Display(Name = "Nom", ResourceType = typeof(AntreDeuxVins.Resources.ResourceModelVin))]
        public string Nom { get; set; }
        [Display(Name = "Description", ResourceType = typeof(AntreDeuxVins.Resources.ResourceModelVin))]
        public string Description { get; set; }
        [Display(Name = "Type", ResourceType = typeof(AntreDeuxVins.Resources.ResourceModelVin))]
        public string Type { get; set; }
        [Display(Name = "Millesime", ResourceType = typeof(AntreDeuxVins.Resources.ResourceModelVin))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(AntreDeuxVinsLanguages.Resources.ErrorMessageResource))]
        [DisplayFormat(DataFormatString = @"{0:dd\/MM\/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Millesime { get; set; }
        [Display(Name = "Volume", ResourceType = typeof(AntreDeuxVins.Resources.ResourceModelVin))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(AntreDeuxVinsLanguages.Resources.ErrorMessageResource))]
        [DisplayFormat(DataFormatString = "{0} L", ApplyFormatInEditMode = true)]
        [Range(0.094, 150, ErrorMessageResourceName ="ErrorVolume", ErrorMessageResourceType = typeof(AntreDeuxVinsLanguages.Resources.ErrorMessageResource))]
        public int Volume { get; set; }
        [Display(Name = "Image", ResourceType = typeof(AntreDeuxVins.Resources.ResourceModelVin))]
        public string Image { get; set; }
        public int? CouleurId { get; set; }
        [Display(Name = "Couleur", ResourceType = typeof(AntreDeuxVins.Resources.ResourceModelVin))]
        public Couleur Couleur { get; set; }
        public int? PaysId { get; set; }
        [Display(Name = "Pays", ResourceType = typeof(AntreDeuxVins.Resources.ResourceModelVin))]
        public Pays Pays { get; set; }
        public int? RegionId { get; set; }
        [Display(Name = "Region", ResourceType = typeof(AntreDeuxVins.Resources.ResourceModelVin))]
        public Region Region { get; set; }
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(AntreDeuxVinsLanguages.Resources.ErrorMessageResource))]
        public int CaveId { get; set; }
        [Display(Name = "Cave", ResourceType = typeof(AntreDeuxVins.Resources.ResourceModelVin))]
        public Cave Cave { get; set; }
        [Display(Name = "Quantite", ResourceType = typeof(AntreDeuxVins.Resources.ResourceModelVin))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(AntreDeuxVinsLanguages.Resources.ErrorMessageResource))]
        [Range(1, 150,ErrorMessageResourceName ="ErrorQuantite",ErrorMessageResourceType = typeof(AntreDeuxVinsLanguages.Resources.ErrorMessageResource))]
        public int Quantite { get; set; }
        [Display(Name = "VinAliments", ResourceType = typeof(AntreDeuxVins.Resources.ResourceModelVin))]
        public ICollection<VinAliment> VinAliments { get; set; }
        public Vin()
        {

        }
    }
}
