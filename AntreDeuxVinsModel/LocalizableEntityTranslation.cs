using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AntreDeuxVinsModel
{
    public class LocalizableEntityTranslation
    {
        //public int Id { get; set; }
        public int LocalizableEntityId { get; set; }
        [Display(Name = "Entity", ResourceType = typeof(AntreDeuxVinsLanguages.Resources.ResourceModelEntityTranslation))]
        public LocalizableEntity LocalizableEntity { get; set; }
        [Display(Name = "Id", ResourceType = typeof(AntreDeuxVinsLanguages.Resources.ResourceModelEntityTranslation))]
        public int PrimaryKeyValue { get; set; }
        [Display(Name = "Field", ResourceType = typeof(AntreDeuxVinsLanguages.Resources.ResourceModelEntityTranslation))]
        public string FieldName { get; set; }
        public int LanguageId { get; set; }
        [Display(Name = "Language", ResourceType = typeof(AntreDeuxVinsLanguages.Resources.ResourceModelEntityTranslation))]
        public Language Language { get; set; }
        [Display(Name = "Text", ResourceType = typeof(AntreDeuxVinsLanguages.Resources.ResourceModelEntityTranslation))]
        public string Text { get; set; } 
    }
}
