using System;
using System.Collections.Generic;
using System.Text;

namespace AntreDeuxVinsModel
{
    public class Language
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public IEnumerable<LocalizableEntityTranslation> LocalizableEntityTranslations { get; set; }
    }
}
