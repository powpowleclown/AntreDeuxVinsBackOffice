using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AntreDeuxVins.Data;
using AntreDeuxVinsModel;
using Microsoft.AspNetCore.Authorization;

namespace AntreDeuxVins.Areas.BackOffice.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("BackOffice")]
    public class LocalizableEntityTranslationsController : Controller
    {
        private readonly AntreDeuxVinsDbContext _context;

        public LocalizableEntityTranslationsController(AntreDeuxVinsDbContext context)
        {
            _context = context;
        }

        // GET: BackOffice/LocalizableEntityTranslations
        public async Task<IActionResult> Index()
        {
            var antreDeuxVinsDbContext = _context.LocalizableEntityTranslations.Include(l => l.Language).Include(l => l.LocalizableEntity);
            return View(await antreDeuxVinsDbContext.ToListAsync());
        }

        // GET: BackOffice/LocalizableEntityTranslations/Details/5
        public async Task<IActionResult> Details(int? id, int? idL, int? Parent)
        {
            if (id == null)
            {
                return NotFound();
            }

            var localizableEntityTranslation = await _context.LocalizableEntityTranslations
                .Include(l => l.Language)
                .Include(l => l.LocalizableEntity)
                .SingleOrDefaultAsync(m => m.LocalizableEntityId == id && m.LanguageId == idL);
            if (localizableEntityTranslation == null)
            {
                return NotFound();
            }
            ViewBag.Parent = Parent;
            return View(localizableEntityTranslation);
        }

        // GET: BackOffice/LocalizableEntityTranslations/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Languages = new SelectList(await _context.Languages.ToListAsync(), "Id", "Name");
            ViewBag.Entitys = new SelectList(await _context.LocalizableEntitys.ToListAsync(), "Id", "EntityName");
            return View();
        }

        // POST: BackOffice/LocalizableEntityTranslations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LocalizableEntityId,PrimaryKeyValue,FieldName,LanguageId,Text")] LocalizableEntityTranslation localizableEntityTranslation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(localizableEntityTranslation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Languages = new SelectList(await _context.Languages.ToListAsync(), "Id", "Name", localizableEntityTranslation.LanguageId);
            ViewBag.Entitys = new SelectList(await _context.LocalizableEntitys.ToListAsync(), "Id", "EntityName", localizableEntityTranslation.LocalizableEntityId);
            return View(localizableEntityTranslation);
        }

        public async Task<IActionResult> CreateByEntity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.EntityId = id;
            ViewBag.Languages = new SelectList(await _context.Languages.ToListAsync(), "Id", "Name");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateByEntity([Bind("LocalizableEntityId,Text,LanguageId")] LocalizableEntityTranslation localizableEntityTranslation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(localizableEntityTranslation);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "LocalizableEntities", new { id = localizableEntityTranslation.LocalizableEntityId, Area = "BackOffice" });
            }
            ViewBag.EntityId = localizableEntityTranslation.LocalizableEntityId;
            ViewBag.Languages = new SelectList(await _context.Languages.ToListAsync(), "Id", "Name", localizableEntityTranslation.LanguageId);
            return View(localizableEntityTranslation);
        }

        // GET: BackOffice/LocalizableEntityTranslations/Edit/5
        public async Task<IActionResult> Edit(int? id, int? idL, int? Parent)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (idL == null)
            {
                return NotFound();
            }

            var localizableEntityTranslation = await _context.LocalizableEntityTranslations.SingleOrDefaultAsync(m => m.LocalizableEntityId == id && m.LanguageId == idL);
            if (localizableEntityTranslation == null)
            {
                return NotFound();
            }
            ViewBag.Parent = Parent;
            ViewBag.Languages = new SelectList(await _context.Languages.ToListAsync(), "Id", "Name", localizableEntityTranslation.LanguageId);
            ViewBag.Entitys = new SelectList(await _context.LocalizableEntitys.ToListAsync(), "Id", "EntityName", localizableEntityTranslation.LocalizableEntityId);
            return View(localizableEntityTranslation);
        }

        // POST: BackOffice/LocalizableEntityTranslations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("LocalizableEntityId,PrimaryKeyValue,FieldName,LanguageId,Text")] LocalizableEntityTranslation localizableEntityTranslation, int? Parent)
        {

            if (Parent != null)
            {
                localizableEntityTranslation.LocalizableEntityId = Parent.Value;
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(localizableEntityTranslation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LocalizableEntityTranslationExists(localizableEntityTranslation.LocalizableEntityId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                if (Parent != null)
                {
                    return RedirectToAction("Details", "LocalizableEntities", new { id = Parent, Area = "BackOffice" });
                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewBag.Parent = Parent;
            ViewBag.Languages = new SelectList(await _context.Languages.ToListAsync(), "Id", "Name", localizableEntityTranslation.LanguageId);
            ViewBag.Entitys = new SelectList(await _context.LocalizableEntitys.ToListAsync(), "Id", "EntityName", localizableEntityTranslation.LocalizableEntityId);
            return View(localizableEntityTranslation);
        }

        // GET: BackOffice/LocalizableEntityTranslations/Delete/5
        public async Task<IActionResult> Delete(int? id, int? idL, int? Parent)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (idL == null)
            {
                return NotFound();
            }

            var localizableEntityTranslation = await _context.LocalizableEntityTranslations
                .Include(l => l.Language)
                .Include(l => l.LocalizableEntity)
                .SingleOrDefaultAsync(m => m.LocalizableEntityId == id && m.LanguageId == idL);
            if (localizableEntityTranslation == null)
            {
                return NotFound();
            }
            ViewBag.Parent = Parent;
            return View(localizableEntityTranslation);
        }

        // POST: BackOffice/LocalizableEntityTranslations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, int idL, int? Parent)
        {
            var localizableEntityTranslation = await _context.LocalizableEntityTranslations.SingleOrDefaultAsync(m => m.LocalizableEntityId == id && m.LanguageId == idL);
            _context.LocalizableEntityTranslations.Remove(localizableEntityTranslation);
            await _context.SaveChangesAsync();
            if (Parent != null)
            {
                return RedirectToAction("Details", "LocalizableEntities", new { id = Parent, Area = "BackOffice" });
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }

        private bool LocalizableEntityTranslationExists(int id)
        {
            return _context.LocalizableEntityTranslations.Any(e => e.LocalizableEntityId == id);
        }

        private bool LocalizableEntityTranslationExists(int id, int idL)
        {
            return _context.LocalizableEntityTranslations.Any(e => e.LocalizableEntityId == id && e.LanguageId == idL);
        }
    }
}
