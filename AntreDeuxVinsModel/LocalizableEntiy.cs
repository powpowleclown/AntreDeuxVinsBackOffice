using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AntreDeuxVinsModel
{
    public class LocalizableEntity
    {
        public int Id { get; set; }
        [Display(Name = "Name", ResourceType = typeof(AntreDeuxVinsLanguages.Resources.ResourceModelEntity))]
        public string EntityName { get; set; }
        [Display(Name = "Id", ResourceType = typeof(AntreDeuxVinsLanguages.Resources.ResourceModelEntity))]
        public string PrimaryKeyFieldName { get; set; }
        public IEnumerable<LocalizableEntityTranslation> LocalizableEntityTranslations { get; set; }
    }
}
