using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AntreDeuxVins.Data;
using AntreDeuxVinsModel;

namespace AntreDeuxVins.Areas.BackOffice.Controllers
{
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
            return View(await _context.Vins.ToListAsync());
        }

        // GET: BackOffice/Vins/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vin = await _context.Vins
                .SingleOrDefaultAsync(m => m.Id == id);
            if (vin == null)
            {
                return NotFound();
            }

            return View(vin);
        }

        // GET: BackOffice/Vins/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BackOffice/Vins/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nom,Description,Type,Millesime,Volume,Image")] Vin vin)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vin);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vin);
        }

        // GET: BackOffice/Vins/Edit/5
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
            return View(vin);
        }

        // POST: BackOffice/Vins/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nom,Description,Type,Millesime,Volume,Image")] Vin vin)
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
                return RedirectToAction(nameof(Index));
            }
            return View(vin);
        }

        // GET: BackOffice/Vins/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vin = await _context.Vins
                .SingleOrDefaultAsync(m => m.Id == id);
            if (vin == null)
            {
                return NotFound();
            }

            return View(vin);
        }

        // POST: BackOffice/Vins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vin = await _context.Vins.SingleOrDefaultAsync(m => m.Id == id);
            _context.Vins.Remove(vin);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VinExists(int id)
        {
            return _context.Vins.Any(e => e.Id == id);
        }
    }
}
