using System;
using System.Collections.Generic;
using System.Text;

namespace AntreDeuxVinsModel
{
    public class LocalizableEntity
    {
        public int Id { get; set; }
        public string EntityName { get; set; }
        public string PrimaryKeyFieldName { get; set; }
        public IEnumerable<LocalizableEntityTranslation> LocalizableEntityTranslations { get; set; }
    }
}
