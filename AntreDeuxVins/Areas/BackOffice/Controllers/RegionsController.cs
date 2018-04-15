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
    public class RegionsController : TranslateController
    {
        private readonly AntreDeuxVinsDbContext _context;

        public RegionsController(AntreDeuxVinsDbContext context)
        {
            _context = context;
            _localization = new Localization(_context);
        }

        // GET: BackOffice/Regions
        public async Task<IActionResult> Index()
        {
            var regions = await _context.Regions.Include(r => r.Pays).ToListAsync();
            foreach (var region in regions)
            {
                _localization.ApplyTranslate(region);
            }
            return View(regions);
        }

        // GET: BackOffice/Regions/Details/5
        public async Task<IActionResult> Details(int? id, int? Parent)
        {
            if (id == null)
            {
                return NotFound();
            }

            var region = await _context.Regions.Include(r => r.Pays).SingleOrDefaultAsync(m => m.Id == id);
            if (region == null)
            {
                return NotFound();
            }
            ViewBag.Parent = Parent;
            _localization.ApplyTranslate(region);
            return View(region);
        }

        // GET: BackOffice/Regions/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Pays = _localization.ApplyTranslateSelectList(await _context.Pays.ToListAsync(), "Id", "Nom");
            return View();
        }

        // POST: BackOffice/Regions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nom,PaysId")] Region region)
        {
            if (ModelState.IsValid)
            {
                _context.Add(region);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Pays = new SelectList(await _context.Pays.ToListAsync(), "Id", "Nom", region.PaysId);
            return View(region);
        }

        // GET: BackOffice/Regions/Create
        public async Task<IActionResult> CreateByPays(int? PaysId)
        {
            if (PaysId == null)
            {
                return NotFound();
            }
            ViewBag.PaysId = PaysId;
            ViewBag.Pays = new SelectList(await _context.Pays.ToListAsync(), "Id", "Nom");
            return View();
        }

        // POST: BackOffice/Regions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateByPays([Bind("Nom,PaysId")] Region region)
        {
            if (ModelState.IsValid)
            {
                _context.Add(region);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Pays", new { id = region.PaysId, Area = "BackOffice" });
            }
            ViewBag.PaysId = region.PaysId;
            ViewBag.Pays = new SelectList(await _context.Pays.ToListAsync(), "Id", "Nom", region.PaysId);
            return View(region);
        }

        // GET: BackOffice/Regions/Edit/5
        public async Task<IActionResult> Edit(int? id, int? Parent)
        {
            if (id == null)
            {
                return NotFound();
            }

            var region = await _context.Regions.Include(r => r.Pays).SingleOrDefaultAsync(m => m.Id == id);
            if (region == null)
            {
                return NotFound();
            }
            ViewBag.Parent = Parent;
            ViewBag.Pays = new SelectList(await _context.Pays.ToListAsync(), "Id", "Nom");
            _localization.ApplyTranslate(region);
            return View(region);
        }

        // POST: BackOffice/Regions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nom,PaysId")] Region region, int? Parent)
        {
            if (id != region.Id)
            {
                return NotFound();
            }
            if(Parent != null)
            {
                region.PaysId = Parent.Value;
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(region);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RegionExists(region.Id))
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
                    return RedirectToAction("Details", "Pays", new { id = Parent, Area = "BackOffice" });
                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewBag.Parent = Parent;
            ViewBag.Pays = new SelectList(await _context.Pays.ToListAsync(), "Id", "Nom", region.PaysId);
            _localization.ApplyTranslate(region);
            return View(region);
        }

        // GET: BackOffice/Regions/Delete/5
        public async Task<IActionResult> Delete(int? id, int? Parent)
        {
            if (id == null)
            {
                return NotFound();
            }

            var region = await _context.Regions.Include(r => r.Pays).SingleOrDefaultAsync(m => m.Id == id);
            if (region == null)
            {
                return NotFound();
            }
            ViewBag.Parent = Parent;
            _localization.ApplyTranslate(region);
            return View(region);
        }

        // POST: BackOffice/Regions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, int? Parent)
        {
            var region = await _context.Regions.SingleOrDefaultAsync(m => m.Id == id);
            _context.Regions.Remove(region);
            await _context.SaveChangesAsync();
            if (Parent != null)
            {
                return RedirectToAction("Details", "Pays", new { id = Parent, Area = "BackOffice" });
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }

        private bool RegionExists(int id)
        {
            return _context.Regions.Any(e => e.Id == id);
        }
    }
}
