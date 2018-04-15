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
    public class LocalizableEntitiesController : Controller
    {
        private readonly AntreDeuxVinsDbContext _context;
        private readonly SelectList _entitys;

        public LocalizableEntitiesController(AntreDeuxVinsDbContext context)
        {
            _context = context;
            _entitys = LoadEntitys();
        }

        private SelectList LoadEntitys()
        {
            var couleur = new SelectListItem { Text = "Couleur", Value = "Couleur" };
            var pays = new SelectListItem { Text = "Pays", Value = "Pays" };
            var region = new SelectListItem { Text = "Region", Value = "Region" };
            List<SelectListItem> entity = new List<SelectListItem>{ couleur, pays, region };
            SelectList entitys = new SelectList(entity, "Value", "Text");
            return entitys;
        }

        // GET: BackOffice/LocalizableEntities
        public async Task<IActionResult> Index()
        {
            return View(await _context.LocalizableEntitys.ToListAsync());
        }

        // GET: BackOffice/LocalizableEntities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var localizableEntity = await _context.LocalizableEntitys.Include(m => m.LocalizableEntityTranslations).ThenInclude(m => m.Language).SingleOrDefaultAsync(m => m.Id == id);
            if (localizableEntity == null)
            {
                return NotFound();
            }

            return View(localizableEntity);
        }

        // GET: BackOffice/LocalizableEntities/Create
        public IActionResult Create()
        {
            ViewBag.Entitys = _entitys;
            return View();
        }

        // POST: BackOffice/LocalizableEntities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EntityName,PrimaryKeyFieldName")] LocalizableEntity localizableEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(localizableEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Entitys = _entitys;
            return View(localizableEntity);
        }

        // GET: BackOffice/LocalizableEntities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var localizableEntity = await _context.LocalizableEntitys.SingleOrDefaultAsync(m => m.Id == id);
            if (localizableEntity == null)
            {
                return NotFound();
            }
            ViewBag.Entitys = _entitys;
            return View(localizableEntity);
        }

        // POST: BackOffice/LocalizableEntities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EntityName,PrimaryKeyFieldName")] LocalizableEntity localizableEntity)
        {
            if (id != localizableEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(localizableEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LocalizableEntityExists(localizableEntity.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Entitys = _entitys;
            return View(localizableEntity);
        }

        // GET: BackOffice/LocalizableEntities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var localizableEntity = await _context.LocalizableEntitys
                .SingleOrDefaultAsync(m => m.Id == id);
            if (localizableEntity == null)
            {
                return NotFound();
            }

            return View(localizableEntity);
        }

        // POST: BackOffice/LocalizableEntities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var localizableEntity = await _context.LocalizableEntitys.SingleOrDefaultAsync(m => m.Id == id);
            _context.LocalizableEntitys.Remove(localizableEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LocalizableEntityExists(int id)
        {
            return _context.LocalizableEntitys.Any(e => e.Id == id);
        }
    }
}
