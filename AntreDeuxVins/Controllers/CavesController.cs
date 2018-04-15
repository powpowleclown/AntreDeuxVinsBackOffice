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
    public class CavesController : TranslateController
    {
        private readonly AntreDeuxVinsDbContext _context;
        private readonly UserManager<Utilisateur> _userManager;

        public CavesController(AntreDeuxVinsDbContext context, UserManager<Utilisateur> userManager)
        {
            _context = context;
            _userManager = userManager;
            _localization = new Localization(_context);
        }

        // GET: Caves/Details/5
        public async Task<IActionResult> Details()
        {
            var user = await _userManager.GetUserAsync(User);
            var cave = await _context.Caves.Include(c => c.Utilisateur).Include(c => c.Vins).ThenInclude(v => v.Couleur).Include(c => c.Vins).ThenInclude(v => v.Pays).Include(c => c.Vins).ThenInclude(v => v.Region).SingleOrDefaultAsync(c => c.Utilisateur.Id == user.Id);
            if (cave == null)
            {
                return RedirectToAction(nameof(Create));
            }
            _localization.ApplyTranslateCave(cave);
            return View(cave);
        }

        // GET: Caves/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Caves/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nom,Description")] Cave cave)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                cave.Utilisateur = user;
                _context.Add(cave);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details));
            }
            return View(cave);
        }

        // GET: Caves/Edit/5
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
            return View(cave);
        }

        // POST: Caves/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nom,Description")] Cave cave)
        {
            if (id != cave.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.GetUserAsync(User);
                    cave.Utilisateur = user;
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
                return RedirectToAction(nameof(Details));
            }
            return View(cave);
        }

        private bool CaveExists(int id)
        {
            return _context.Caves.Any(e => e.Id == id);
        }
    }
}
