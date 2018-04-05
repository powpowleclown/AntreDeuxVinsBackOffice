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
    public class CouleursController : Controller
    {
        private readonly AntreDeuxVinsDbContext _context;

        public CouleursController(AntreDeuxVinsDbContext context)
        {
            _context = context;
        }

        // GET: BackOffice/Couleurs
        public async Task<IActionResult> Index()
        {
            return View(await _context.Couleurs.ToListAsync());
        }

        // GET: BackOffice/Couleurs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var couleur = await _context.Couleurs
                .SingleOrDefaultAsync(m => m.Id == id);
            if (couleur == null)
            {
                return NotFound();
            }

            return View(couleur);
        }

        // GET: BackOffice/Couleurs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BackOffice/Couleurs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nom")] Couleur couleur)
        {
            if (ModelState.IsValid)
            {
                _context.Add(couleur);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(couleur);
        }

        // GET: BackOffice/Couleurs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var couleur = await _context.Couleurs.SingleOrDefaultAsync(m => m.Id == id);
            if (couleur == null)
            {
                return NotFound();
            }
            return View(couleur);
        }

        // POST: BackOffice/Couleurs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nom")] Couleur couleur)
        {
            if (id != couleur.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(couleur);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CouleurExists(couleur.Id))
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
            return View(couleur);
        }

        // GET: BackOffice/Couleurs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var couleur = await _context.Couleurs
                .SingleOrDefaultAsync(m => m.Id == id);
            if (couleur == null)
            {
                return NotFound();
            }

            return View(couleur);
        }

        // POST: BackOffice/Couleurs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var couleur = await _context.Couleurs.SingleOrDefaultAsync(m => m.Id == id);
            _context.Couleurs.Remove(couleur);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CouleurExists(int id)
        {
            return _context.Couleurs.Any(e => e.Id == id);
        }
    }
}
