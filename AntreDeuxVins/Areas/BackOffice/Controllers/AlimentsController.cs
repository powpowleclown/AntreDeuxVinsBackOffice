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
    public class AlimentsController : TranslateController
    {
        private readonly AntreDeuxVinsDbContext _context;

        public AlimentsController(AntreDeuxVinsDbContext context)
        {
            _context = context;
            _localization = new Localization(_context);
        }

        // GET: BackOffice/Aliments
        public async Task<IActionResult> Index()
        {
            return View(_localization.ApplyTranslateList(await _context.Aliments.ToListAsync()));
        }

        // GET: BackOffice/Aliments/Details/5
        public async Task<IActionResult> Details(int? id, int? Parent, int? ParentParent)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aliment = await _context.Aliments
                .SingleOrDefaultAsync(m => m.Id == id);
            if (aliment == null)
            {
                return NotFound();
            }
            ViewBag.ParentParent = ParentParent;
            ViewBag.Parent = Parent;
            _localization.ApplyTranslate(aliment);
            return View(aliment);
        }

        // GET: BackOffice/Aliments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BackOffice/Aliments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nom,Description")] Aliment aliment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aliment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(aliment);
        }

        public IActionResult CreateByVin(int? id, int? ParentParent)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.ParentParent = ParentParent;
            ViewBag.VinId = id;
            return View();
        }

        // POST: BackOffice/Aliments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateByVin([Bind("Nom,Description")] Aliment aliment, int VinId, int? ParentParent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aliment);
                _context.Add(new VinAliment { AlimentId = aliment.Id, VinId = VinId });
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Vins", new { id = VinId, Parent = ParentParent, Area = "BackOffice" });
            }
            ViewBag.ParentParent = ParentParent;
            ViewBag.VinId = VinId;
            return View(aliment);
        }

        // GET: BackOffice/Aliments/Edit/5
        public async Task<IActionResult> Edit(int? id, int? Parent, int? ParentParent)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aliment = await _context.Aliments.SingleOrDefaultAsync(m => m.Id == id);
            if (aliment == null)
            {
                return NotFound();
            }
            ViewBag.ParentParent = ParentParent;
            ViewBag.Parent = Parent;
            _localization.ApplyTranslate(aliment);
            return View(aliment);
        }

        // POST: BackOffice/Aliments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nom,Description")] Aliment aliment, int? Parent, int? ParentParent)
        {
            if (id != aliment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aliment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlimentExists(aliment.Id))
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
                    return RedirectToAction("Details", "Vins", new { id = Parent, Parent = ParentParent, Area = "BackOffice" });
                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewBag.ParentParent = ParentParent;
            ViewBag.Parent = Parent;
            _localization.ApplyTranslate(aliment);
            return View(aliment);
        }

        // GET: BackOffice/Aliments/Delete/5
        public async Task<IActionResult> Delete(int? id, int? Parent, int? ParentParent)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aliment = await _context.Aliments
                .SingleOrDefaultAsync(m => m.Id == id);
            if (aliment == null)
            {
                return NotFound();
            }
            ViewBag.ParentParent = ParentParent;
            ViewBag.Parent = Parent;
            _localization.ApplyTranslate(aliment);
            return View(aliment);
        }

        // POST: BackOffice/Aliments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, int? Parent, int? ParentParent)
        {
            var aliment = await _context.Aliments.SingleOrDefaultAsync(m => m.Id == id);
            _context.Aliments.Remove(aliment);
            await _context.SaveChangesAsync();
            if (Parent != null)
            {
                return RedirectToAction("Details", "Vins", new { id = Parent, Parent= ParentParent, Area = "BackOffice" });
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }

        private bool AlimentExists(int id)
        {
            return _context.Aliments.Any(e => e.Id == id);
        }
    }
}
