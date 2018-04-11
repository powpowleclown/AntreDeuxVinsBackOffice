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
using AntreDeuxVins.Models;

namespace AntreDeuxVins.Areas.BackOffice.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("BackOffice")]
    public class UtilisateursController : Controller
    {
        private readonly AntreDeuxVinsDbContext _context;
        private readonly UserManager<Utilisateur> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public UtilisateursController(AntreDeuxVinsDbContext context, UserManager<Utilisateur> userManager, RoleManager<Role> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: BackOffice/Utilisateurs
        public async Task<IActionResult> Index()
        {
            return View(await _context.Utilisateurs.Include(u => u.Role).ToListAsync());
        }

        // GET: BackOffice/Utilisateurs/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var utilisateur = await _context.Utilisateurs.Include(u => u.Role).SingleOrDefaultAsync(m => m.Id == id);
            if (utilisateur == null)
            {
                return NotFound();
            }

            return View(utilisateur);
        }

        // GET: BackOffice/Utilisateurs/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.RolesId = new SelectList(await _context.Role.ToListAsync(), "Id", "Name");
            return View();
        }

        // POST: BackOffice/Utilisateurs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nom,Prenom,Email,Password,ConfirmPassword")] Utilisateur utilisateur, string RoleId)
        {
            if (ModelState.IsValid)
            {
                Utilisateur createutilisateur = new Utilisateur(utilisateur.Email, utilisateur.Nom, utilisateur.Prenom, utilisateur.Password);
                var role = await _roleManager.Roles.FirstAsync(r => r.Id == Guid.Parse(RoleId));
                var result = await _userManager.CreateAsync(createutilisateur, createutilisateur.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(createutilisateur, role.Name);
                    return RedirectToAction(nameof(Index));
                }
                
            }
            ViewBag.RolesId = new SelectList(await _context.Role.ToListAsync(), "Id", "Name");
            return View(utilisateur);
        }

        // GET: BackOffice/Utilisateurs/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var currentuser = await _userManager.FindByIdAsync(id.ToString());
            if (currentuser == null)
            {
                return NotFound();
            }
            var currentrolename = (await _userManager.GetRolesAsync(currentuser))[0];
            EditUserViewModel editUserViewModel = new EditUserViewModel { Id = currentuser.Id, Email = currentuser.Email, Nom = currentuser.Nom, Prenom = currentuser.Prenom};
            ViewBag.RoleName = currentrolename;
            ViewBag.RolesId = new SelectList(await _context.Role.ToListAsync(), "Id", "Name");
            return View(editUserViewModel);
        }

        // POST: BackOffice/Utilisateurs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Nom,Prenom,Email")] EditUserViewModel editUserViewModel, string RoleId)
        {
            if (id != editUserViewModel.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var currentuser = await  _userManager.FindByIdAsync(id.ToString());
                if (currentuser.Email != editUserViewModel.Email)
                {
                    currentuser.Email = editUserViewModel.Email;
                }
                if (currentuser.Nom != editUserViewModel.Nom)
                {
                    currentuser.Nom = editUserViewModel.Nom;
                }
                if (currentuser.Prenom != editUserViewModel.Prenom)
                {
                    currentuser.Prenom = editUserViewModel.Prenom;
                }
                var currentRole = await _userManager.GetRolesAsync(currentuser);
                var currentRoleName = currentRole[0];
                var selectedRole = await _roleManager.FindByIdAsync(RoleId);
                if (selectedRole.Name != currentRoleName)
                {
                    await _userManager.RemoveFromRoleAsync(currentuser, currentRoleName);
                    currentuser.Role = selectedRole;
                    await _userManager.AddToRoleAsync(currentuser, selectedRole.Name);
                }
                var resultUpdate = await _userManager.UpdateAsync(currentuser);
                if(resultUpdate.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                editUserViewModel.Id = currentuser.Id;
            }
            ViewBag.RoleName = await _roleManager.FindByIdAsync(RoleId);
            ViewBag.RolesId = new SelectList(await _context.Role.ToListAsync(), "Id", "Name");
            return View(editUserViewModel);
        }

        // GET: BackOffice/Utilisateurs/Edit/5
        public async Task<IActionResult> EditPwd(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var utilisateur = await _context.Utilisateurs.SingleOrDefaultAsync(m => m.Id == id);
            if (utilisateur == null)
            {
                return NotFound();
            }
            var pwdmodel = new PwdViewModel { Id = id.Value, Password = "", ConfirmPassword = "" };
            return View(pwdmodel);
        }

        // POST: BackOffice/Utilisateurs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPwd(Guid id, [Bind("Id,Password,ConfirmPassword")] PwdViewModel pwdViewModel)
        {
            if (id != pwdViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var user = await _userManager.Users.FirstAsync(u => u.Id == pwdViewModel.Id);
                await _userManager.RemovePasswordAsync(user);
                var resultUserPwd = await _userManager.AddPasswordAsync(user, pwdViewModel.Password);
                if (resultUserPwd.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(pwdViewModel);
        }

        // GET: BackOffice/Utilisateurs/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var utilisateur = await _context.Utilisateurs.Include(u => u.Role).SingleOrDefaultAsync(m => m.Id == id);
            if (utilisateur == null)
            {
                return NotFound();
            }

            return View(utilisateur);
        }

        // POST: BackOffice/Utilisateurs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var utilisateur = await _context.Utilisateurs.SingleOrDefaultAsync(m => m.Id == id);
            _context.Utilisateurs.Remove(utilisateur);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UtilisateurExists(Guid id)
        {
            return _context.Utilisateurs.Any(e => e.Id == id);
        }
    }
}
