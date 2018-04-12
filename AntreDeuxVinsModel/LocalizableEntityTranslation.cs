using System;
using System.Collections.Generic;
using System.Text;

namespace AntreDeuxVinsModel
{
    public class LocalizableEntityTranslation
    {
        public int Id { get; set; }
        public int LocalizableEntityId { get; set; }
        public LocalizableEntity LocalizableEntity { get; set; }
        public int PrimaryKeyValue { get; set; }
        public string FieldName { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; }
        public string Text { get; set; } 
    }
}
