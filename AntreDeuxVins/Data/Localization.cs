using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AntreDeuxVinsModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

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
            string entityName = entity.GetType().Name;

            var locEntity = _context.LocalizableEntitys.Include(c => c.LocalizableEntityTranslations).ThenInclude(c => c.LocalizableEntity).Include(c => c.LocalizableEntityTranslations).ThenInclude(c => c.Language).SingleOrDefault(c => c.EntityName == entityName);
            if (null != locEntity)
            {
                int entityId = (int)entity.GetType().
                                    GetProperty(locEntity.PrimaryKeyFieldName).GetValue(entity, null);
                var ler = locEntity.LocalizableEntityTranslations
                            .Where(er => er.LocalizableEntity.EntityName.Equals(entityName)
                                            && er.PrimaryKeyValue.Equals(entityId)
                                            && er.Language.Code.Equals(languageCode));
                foreach (var t in ler)
                {
                    entity.GetType().GetProperty(t.FieldName).SetValue(entity, t.Text, null);
                }
            }
            return entity;
        }

        public void ApplyTranslate(Object entity)
        {
            var CultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
            entity = Translate(entity, CultureInfo.Name);
        }

        public SelectList ApplyTranslateSelectList<T>(List<T> listentity, string id, string value)
        {
            listentity.ForEach(e => ApplyTranslate(e));
            return new SelectList(listentity, id, value);
        }

        public SelectList ApplyTranslateSelectList<T>(List<T> listentity, string id, string value, int? selected)
        {
            listentity.ForEach(e => ApplyTranslate(e));
            return new SelectList(listentity, id, value, selected);
        }
        public List<T> ApplyTranslateList<T>(List<T> listentity)
        {
            listentity.ForEach(e => ApplyTranslate(e));
            return listentity;
        }

        public List<Vin> ApplyTranslateListVins(List<Vin> listvin)
        {
            listvin.ForEach(e => ApplyTranslate(e.Couleur));
            listvin.ForEach(e => ApplyTranslate(e.Pays));
            return listvin;
        }
        public Cave ApplyTranslateCave(Cave cave)
        {
            cave.Vins.ToList().ForEach(e => ApplyTranslate(e.Couleur));
            cave.Vins.ToList().ForEach(e => ApplyTranslate(e.Pays));
            return cave;
        }
    }
}
