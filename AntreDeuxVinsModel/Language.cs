using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AntreDeuxVinsModel
{
    public class Language
    {
        public int Id { get; set; }
        [Display(Name = "Code", ResourceType = typeof(AntreDeuxVinsLanguages.Resources.ResourceModelLanguage))]
        public string Code { get; set; }
        [Display(Name = "Nom", ResourceType = typeof(AntreDeuxVinsLanguages.Resources.ResourceModelLanguage))]
        public string Name { get; set; }
        public IEnumerable<LocalizableEntityTranslation> LocalizableEntityTranslations { get; set; }
    }
}
