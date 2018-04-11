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
using Microsoft.AspNetCore.Identity;

namespace AntreDeuxVins.Areas.BackOffice.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("BackOffice")]
    public class CavesController : Controller
    {
        private readonly AntreDeuxVinsDbContext _context;

        public CavesController(AntreDeuxVinsDbContext context)
        {
            _context = context;
        }

        // GET: BackOffice/Caves
        public async Task<IActionResult> Index()
        {
            return View(await _context.Caves.Include(c => c.Utilisateur).ToListAsync());
        }

        // GET: BackOffice/Caves/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var cave = await _context.Caves.Include(c => c.Utilisateur).Include(c => c.Vins).ThenInclude(v => v.Couleur).Include(c => c.Vins).ThenInclude(v => v.Pays).Include(c => c.Vins).ThenInclude(v => v.Region).SingleOrDefaultAsync(c => c.Id == id);
            if (cave == null)
            {
                return NotFound();
            }

            return View(cave);
        }

        // GET: BackOffice/Caves/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Utilisateurs = new SelectList(await _context.Utilisateurs.ToListAsync(), "Id", "UserName");
            return View();
        }

        // POST: BackOffice/Caves/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nom,Description,UtilisateurId")] Cave cave)
        {
            //USER
            if (ModelState.IsValid)
            {
                _context.Add(cave);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Utilisateurs = new SelectList(await _context.Utilisateurs.ToListAsync(), "Id", "UserName");
            return View(cave);
        }

        // GET: BackOffice/Caves/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cave = await _context.Caves.SingleOrDefaultAsync(m => m.Id == id);
            if (cave == null)
            {
                return NotFound();
            }
            ViewBag.Utilisateurs = new SelectList(await _context.Utilisateurs.ToListAsync(), "Id", "UserName");
            return View(cave);
        }

        // POST: BackOffice/Caves/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nom,Description,UtilisateurId")] Cave cave)
        {
            if (id != cave.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cave);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CaveExists(cave.Id))
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
            ViewBag.Utilisateurs = new SelectList(await _context.Utilisateurs.ToListAsync(), "Id", "UserName");
            return View(cave);
        }

        // GET: BackOffice/Caves/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cave = await _context.Caves.Include(c => c.Utilisateur).SingleOrDefaultAsync(m => m.Id == id);
            if (cave == null)
            {
                return NotFound();
            }

            return View(cave);
        }

        // POST: BackOffice/Caves/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cave = await _context.Caves.SingleOrDefaultAsync(m => m.Id == id);
            _context.Caves.Remove(cave);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CaveExists(int id)
        {
            return _context.Caves.Any(e => e.Id == id);
        }
    }
}
