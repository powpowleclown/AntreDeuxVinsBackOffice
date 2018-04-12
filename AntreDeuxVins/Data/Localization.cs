using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using AntreDeuxVinsModel;

namespace AntreDeuxVins.Data
{
    public class Localization
    {
        private readonly AntreDeuxVinsDbContext _context;

        public Localization(AntreDeuxVinsDbContext context)
        {
            _context = context;
        }
        public T Translate<T>(T entity, string languageCode)
        {
            string entityName = entity.GetType().BaseType.Name;
            using (var ctx = _context)
            {
                // Get the entity info
                var locEntity = _context.LocalizableEntitys.Where(p => p.EntityName == entityName).SingleOrDefault();
                if (null != locEntity)
                {
                    // Get the entity id
                    int entityId = (int)entity.GetType().
                                       GetProperty(locEntity.PrimaryKeyFieldName).GetValue(entity, null);
                    // Get the  fields that need to be translated for this entity
                    var ler = locEntity.LocalizableEntityTranslations
                                .Where(er => er.LocalizableEntity.EntityName.Equals(entityName)
                                             && er.PrimaryKeyValue.Equals(entityId)
                                             && er.Language.Code.Equals(languageCode));
                    foreach (var t in ler)
                    {
                        // Overwrite each field with the translated value
                        entity.GetType().GetProperty(t.FieldName).SetValue(entity, t.Text, null);
                    }
                }
            }
            return entity;
        }
    }
}
