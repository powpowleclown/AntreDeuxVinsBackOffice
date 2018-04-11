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
    public class VinsController : Controller
    {
        private readonly AntreDeuxVinsDbContext _context;

        public VinsController(AntreDeuxVinsDbContext context)
        {
            _context = context;
        }

        // GET: BackOffice/Vins
        public async Task<IActionResult> Index()
        {
            return View(await _context.Vins.Include(v => v.Cave).Include(v => v.Couleur).Include(v => v.Pays).Include(v => v.Region).ToListAsync());
        }

        // GET: BackOffice/Vins/Details/5
        public async Task<IActionResult> Details(int? id, int? Parent)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vin = await _context.Vins.Include(v => v.Cave).Include(v => v.Couleur).Include(v => v.Pays).Include(v => v.Region).Include(v => v.VinAliments).ThenInclude(va => va.Aliment).SingleOrDefaultAsync(m => m.Id == id);
            if (vin == null)
            {
                return NotFound();
            }
            ViewBag.Parent = Parent;
            return View(vin);
        }

        // GET: BackOffice/Vins/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Caves = new SelectList(await _context.Caves.ToListAsync(), "Id", "Nom");
            ViewBag.Couleurs = new SelectList(await _context.Couleurs.ToListAsync(), "Id", "Nom");
            ViewBag.Pays = new SelectList(await _context.Pays.ToListAsync(), "Id", "Nom");
            ViewBag.Regions = new SelectList(await _context.Regions.ToListAsync(), "Id", "Nom");
            ViewBag.Aliments = new SelectList(await _context.Aliments.ToListAsync(), "Id", "Nom");
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
                foreach(int aliment in AlimentsId)
                {
                    _context.Add(new VinAliment { VinId = vin.Id, AlimentId = aliment });
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Caves = new SelectList(await _context.Caves.ToListAsync(), "Id", "Nom");
            ViewBag.Couleurs = new SelectList(await _context.Couleurs.ToListAsync(), "Id", "Nom");
            ViewBag.Pays = new SelectList(await _context.Pays.ToListAsync(), "Id", "Nom");
            ViewBag.Regions = new SelectList(await _context.Regions.ToListAsync(), "Id", "Nom");
            ViewBag.Aliments = new SelectList(await _context.Aliments.ToListAsync(), "Id", "Nom");
            return View(vin);
        }

        // GET: BackOffice/Vins/Create
        public async Task<IActionResult> CreateByCave(int? CaveId)
        {
            if (CaveId == null)
            {
                return NotFound();
            }
            ViewBag.CaveId = CaveId;
            ViewBag.Couleurs = new SelectList(await _context.Couleurs.ToListAsync(), "Id", "Nom");
            ViewBag.Pays = new SelectList(await _context.Pays.ToListAsync(), "Id", "Nom");
            ViewBag.Regions = new SelectList(await _context.Regions.ToListAsync(), "Id", "Nom");
            ViewBag.Aliments = new SelectList(await _context.Aliments.ToListAsync(), "Id", "Nom");
            return View();
        }

        // POST: BackOffice/Vins/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateByCave([Bind("Nom,Description,Type,Millesime,Volume,Image,CouleurId,PaysId,RegionId,CaveId")] Vin vin, int[] AlimentsId)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vin);
                foreach (int aliment in AlimentsId)
                {
                    _context.Add(new VinAliment { VinId = vin.Id, AlimentId = aliment });
                }
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Caves", new { id = vin.CaveId, Area = "BackOffice" });
            }
            ViewBag.CaveId = vin.CaveId;
            ViewBag.Couleurs = new SelectList(await _context.Couleurs.ToListAsync(), "Id", "Nom");
            ViewBag.Pays = new SelectList(await _context.Pays.ToListAsync(), "Id", "Nom");
            ViewBag.Regions = new SelectList(await _context.Regions.ToListAsync(), "Id", "Nom");
            ViewBag.Aliments = new SelectList(await _context.Aliments.ToListAsync(), "Id", "Nom");
            return View(vin);
        }

        // GET: BackOffice/Vins/Edit/5
        public async Task<IActionResult> Edit(int? id, int? Parent)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vin = await _context.Vins.Include(v => v.Cave).Include(v => v.VinAliments).ThenInclude(v => v.Aliment).SingleOrDefaultAsync(m => m.Id == id);
            if (vin == null)
            {
                return NotFound();
            }
            ViewBag.Parent = Parent;
            ViewBag.Caves = new SelectList(await _context.Caves.ToListAsync(), "Id", "Nom");
            ViewBag.Couleurs = new SelectList(await _context.Couleurs.ToListAsync(), "Id", "Nom");
            ViewBag.Pays = new SelectList(await _context.Pays.ToListAsync(), "Id", "Nom");
            ViewBag.Regions = new SelectList(await _context.Regions.ToListAsync(), "Id", "Nom");
            ViewBag.Aliments = new SelectList(await _context.Aliments.ToListAsync(), "Id", "Nom");
            ViewBag.VinAliments = await _context.VinAlments.Where(va => va.VinId == vin.Id).AsNoTracking().Select(va => va.AlimentId).ToListAsync();
            return View(vin);
        }

        // POST: BackOffice/Vins/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nom,Description,Type,Millesime,Volume,Image,CouleurId,PaysId,RegionId,CaveId,Quantite")] Vin vin, int[] AlimentsId, int? Parent)
        {
            if (id != vin.Id)
            {
                return NotFound();
            }
            if(Parent != null)
            {
                vin.CaveId = Parent.Value;
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
                if(Parent != null)
                {
                    return RedirectToAction("Details", "Caves", new { id = Parent, Area = "BackOffice" });
                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }
                
            }
            ViewBag.Parent = Parent;
            ViewBag.Caves = new SelectList(await _context.Caves.ToListAsync(), "Id", "Nom");
            ViewBag.Couleurs = new SelectList(await _context.Couleurs.ToListAsync(), "Id", "Nom");
            ViewBag.Pays = new SelectList(await _context.Pays.ToListAsync(), "Id", "Nom");
            ViewBag.Regions = new SelectList(await _context.Regions.ToListAsync(), "Id", "Nom");
            ViewBag.Aliments = new SelectList(await _context.Aliments.ToListAsync(), "Id", "Nom");
            ViewBag.VinAliments = await _context.VinAlments.Where(va => va.VinId == vin.Id).AsNoTracking().Select(va => va.AlimentId).ToListAsync();
            return View(vin);
        }

        // GET: BackOffice/Vins/Delete/5
        public async Task<IActionResult> Delete(int? id, int? Parent)
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
            ViewBag.Parent = Parent;
            return View(vin);
        }

        // POST: BackOffice/Vins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, int? Parent)
        {
            var vin = await _context.Vins.SingleOrDefaultAsync(m => m.Id == id);
            _context.Vins.Remove(vin);
            await _context.SaveChangesAsync();
            if(Parent != null)
            {
                return RedirectToAction("Details", "Caves", new { id = Parent, Area = "BackOffice" });
            }
            else
            {
                return RedirectToAction(nameof(Index));
            } 
        }

        private bool VinExists(int id)
        {
            return _context.Vins.Any(e => e.Id == id);
        }
    }
}
