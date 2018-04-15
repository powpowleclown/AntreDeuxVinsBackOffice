using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AntreDeuxVins.Data;
using AntreDeuxVinsModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace AntreDeuxVins.Controllers
{
    [Authorize]
    public class VinsController : TranslateController
    {
        private readonly AntreDeuxVinsDbContext _context;
        private readonly UserManager<Utilisateur> _userManager;

        public VinsController(AntreDeuxVinsDbContext context, UserManager<Utilisateur> userManager)
        {
            _context = context;
            _userManager = userManager;
            _localization = new Localization(_context);
        }

        // GET: Vins/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vin = await _context.Vins.Include(v => v.Cave).Include(v => v.Couleur).Include(v => v.Pays).Include(v => v.Region).SingleOrDefaultAsync(m => m.Id == id);
            if (vin == null)
            {
                return NotFound();
            }
            //Translate
            _localization.ApplyTranslate(vin.Couleur);
            _localization.ApplyTranslate(vin.Pays);
            return View(vin);
        }

        public async Task<IActionResult> Create(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewBag.CaveId = id;
            ViewBag.Couleurs = _localization.ApplyTranslateSelectList(await _context.Couleurs.ToListAsync(), "Id", "Nom");
            ViewBag.Pays = _localization.ApplyTranslateSelectList(await _context.Pays.ToListAsync(), "Id", "Nom");
            ViewBag.Regions = new SelectList(await _context.Regions.ToListAsync(), "Id", "Nom");
            ViewBag.Aliments = _localization.ApplyTranslateSelectList(await _context.Aliments.ToListAsync(), "Id", "Nom");
            return View();
        }

        // POST: BackOffice/Vins/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nom,Description,Type,Millesime,Volume,Image,CouleurId,PaysId,RegionId,CaveId,Quantite")] Vin vin, int[] AlimentsId)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vin);
                foreach (int aliment in AlimentsId)
                {
                    _context.Add(new VinAliment { VinId = vin.Id, AlimentId = aliment });
                }
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Caves");
            }
            ViewBag.CaveId = vin.CaveId;
            ViewBag.Couleurs = _localization.ApplyTranslateSelectList(await _context.Couleurs.ToListAsync(), "Id", "Nom", vin.CouleurId);
            ViewBag.Pays = _localization.ApplyTranslateSelectList(await _context.Pays.ToListAsync(), "Id", "Nom", vin.PaysId);
            ViewBag.Regions = new SelectList(await _context.Regions.ToListAsync(), "Id", "Nom", vin.RegionId);
            ViewBag.Aliments = _localization.ApplyTranslateSelectList(await _context.Aliments.ToListAsync(), "Id", "Nom");
            return View(vin);
        }

        // GET: Vins/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vin = await _context.Vins.SingleOrDefaultAsync(m => m.Id == id);
            if (vin == null)
            {
                return NotFound();
            }
            ViewBag.CaveId = vin.CaveId;
            ViewBag.Couleurs = _localization.ApplyTranslateSelectList(await _context.Couleurs.ToListAsync(), "Id", "Nom");
            ViewBag.Pays = _localization.ApplyTranslateSelectList(await _context.Pays.ToListAsync(), "Id", "Nom");
            ViewBag.Regions = new SelectList(await _context.Regions.ToListAsync(), "Id", "Nom");
            ViewBag.Aliments = _localization.ApplyTranslateSelectList(await _context.Aliments.ToListAsync(), "Id", "Nom");
            ViewBag.VinAliments = await _context.VinAlments.Where(va => va.VinId == vin.Id).AsNoTracking().Select(va => va.AlimentId).ToListAsync();
            return View(vin);
        }

        // POST: Vins/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nom,Description,Type,Millesime,Volume,Image,CouleurId,RegionId,PaysId,CaveId,Quantite")] Vin vin, int[] AlimentsId)
        {
            if (id != vin.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vin);
                    var vinaliments = await _context.VinAlments.Where(va => va.VinId == vin.Id).AsNoTracking().ToListAsync();
                    _context.VinAlments.RemoveRange(vinaliments);
                    await _context.SaveChangesAsync();
                    foreach (int aliment in AlimentsId)
                    {
                        _context.Add(new VinAliment { VinId = vin.Id, AlimentId = aliment });
                    }
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VinExists(vin.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction("Details", "Caves");
            }
            ViewBag.CaveId = vin.CaveId;
            ViewBag.Couleurs = _localization.ApplyTranslateSelectList(await _context.Couleurs.ToListAsync(), "Id", "Nom", vin.CouleurId);
            ViewBag.Pays = _localization.ApplyTranslateSelectList(await _context.Pays.ToListAsync(), "Id", "Nom", vin.PaysId);
            ViewBag.Regions = new SelectList(await _context.Regions.ToListAsync(), "Id", "Nom", vin.RegionId);
            ViewBag.Aliments = _localization.ApplyTranslateSelectList(await _context.Aliments.ToListAsync(), "Id", "Nom");
            ViewBag.VinAliments = await _context.VinAlments.Where(va => va.VinId == vin.Id).AsNoTracking().Select(va => va.AlimentId).ToListAsync();
            return View(vin);
        }

        // GET: Vins/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vin = await _context.Vins.Include(v => v.Cave).Include(v => v.Couleur).Include(v => v.Pays).Include(v => v.Region).SingleOrDefaultAsync(m => m.Id == id);
            if (vin == null)
            {
                return NotFound();
            }
            //Translate
            _localization.ApplyTranslate(vin.Couleur);
            _localization.ApplyTranslate(vin.Pays);
            return View(vin);
        }

        // POST: Vins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vin = await _context.Vins.SingleOrDefaultAsync(m => m.Id == id);
            _context.Vins.Remove(vin);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Caves");
        }

        private bool VinExists(int id)
        {
            return _context.Vins.Any(e => e.Id == id);
        }
    }
}
