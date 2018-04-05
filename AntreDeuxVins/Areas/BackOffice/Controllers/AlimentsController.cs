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
    public class AlimentsController : Controller
    {
        private readonly AntreDeuxVinsDbContext _context;

        public AlimentsController(AntreDeuxVinsDbContext context)
        {
            _context = context;
        }

        // GET: BackOffice/Aliments
        public async Task<IActionResult> Index()
        {
            return View(await _context.Aliments.ToListAsync());
        }

        // GET: BackOffice/Aliments/Details/5
        public async Task<IActionResult> Details(int? id)
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
        public async Task<IActionResult> Create([Bind("Id,Nom,Description")] Aliment aliment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aliment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(aliment);
        }

        // GET: BackOffice/Aliments/Edit/5
        public async Task<IActionResult> Edit(int? id)
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
            return View(aliment);
        }

        // POST: BackOffice/Aliments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nom,Description")] Aliment aliment)
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
                return RedirectToAction(nameof(Index));
            }
            return View(aliment);
        }

        // GET: BackOffice/Aliments/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

            return View(aliment);
        }

        // POST: BackOffice/Aliments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var aliment = await _context.Aliments.SingleOrDefaultAsync(m => m.Id == id);
            _context.Aliments.Remove(aliment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlimentExists(int id)
        {
            return _context.Aliments.Any(e => e.Id == id);
        }
    }
}
