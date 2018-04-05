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
    public class PaysController : Controller
    {
        private readonly AntreDeuxVinsDbContext _context;

        public PaysController(AntreDeuxVinsDbContext context)
        {
            _context = context;
        }

        // GET: BackOffice/Pays
        public async Task<IActionResult> Index()
        {
            return View(await _context.Pays.ToListAsync());
        }

        // GET: BackOffice/Pays/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pays = await _context.Pays
                .SingleOrDefaultAsync(m => m.Id == id);
            if (pays == null)
            {
                return NotFound();
            }

            return View(pays);
        }

        // GET: BackOffice/Pays/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BackOffice/Pays/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nom")] Pays pays)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pays);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pays);
        }

        // GET: BackOffice/Pays/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pays = await _context.Pays.SingleOrDefaultAsync(m => m.Id == id);
            if (pays == null)
            {
                return NotFound();
            }
            return View(pays);
        }

        // POST: BackOffice/Pays/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nom")] Pays pays)
        {
            if (id != pays.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pays);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaysExists(pays.Id))
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
            return View(pays);
        }

        // GET: BackOffice/Pays/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pays = await _context.Pays
                .SingleOrDefaultAsync(m => m.Id == id);
            if (pays == null)
            {
                return NotFound();
            }

            return View(pays);
        }

        // POST: BackOffice/Pays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pays = await _context.Pays.SingleOrDefaultAsync(m => m.Id == id);
            _context.Pays.Remove(pays);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaysExists(int id)
        {
            return _context.Pays.Any(e => e.Id == id);
        }
    }
}
