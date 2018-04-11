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

namespace AntreDeuxVins.Controllers
{
    [Authorize]
    public class UtilisateursController : Controller
    {
        private readonly AntreDeuxVinsDbContext _context;
        private readonly UserManager<Utilisateur> _userManager;

        public UtilisateursController(AntreDeuxVinsDbContext context, UserManager<Utilisateur> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Utilisateurs/Details/5
        public async Task<IActionResult> Details()
        {
            var utilisateur = await _userManager.GetUserAsync(User);
            if (utilisateur == null)
            {
                return NotFound();
            }

            return View(utilisateur);
        }

        // GET: Utilisateurs/Edit/5
        public async Task<IActionResult> Edit()
        {
            var utilisateur = await _userManager.GetUserAsync(User);
            if (utilisateur == null)
            {
                return NotFound();
            }
            EditUserViewModel editUserViewModel = new EditUserViewModel { Id = utilisateur.Id, Email = utilisateur.Email, Nom=utilisateur.Nom, Prenom = utilisateur.Prenom };
            return View(editUserViewModel);
        }

        // POST: Utilisateurs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Nom,Prenom,Email")] EditUserViewModel editUserViewModel)
        {
            if (ModelState.IsValid)
            {
                var currentuser = await _userManager.GetUserAsync(User);
                if(currentuser.Email != editUserViewModel.Email)
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
                var resultUpdate = await _userManager.UpdateAsync(currentuser);
                if (resultUpdate.Succeeded)
                {
                    return RedirectToAction(nameof(Details));
                }
                editUserViewModel.Id = currentuser.Id;
            }
            return View(editUserViewModel);
        }

        public async Task<IActionResult> EditPwd()
        {
            var utilisateur = await _userManager.GetUserAsync(User);
            if (utilisateur == null)
            {
                return NotFound();
            }
            var pwdmodel = new PwdViewModel { Password = "", ConfirmPassword = "" };
            return View(pwdmodel);
        }

        // POST: Utilisateurs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPwd([Bind("Password,ConfirmPassword")] PwdViewModel pwdViewModel)
        {
            var user = await _userManager.GetUserAsync(User);
            if (ModelState.IsValid)
            {
                await _userManager.RemovePasswordAsync(user);
                var resultUserPwd = await _userManager.AddPasswordAsync(user, pwdViewModel.Password);
                if(resultUserPwd.Succeeded)
                {
                    return RedirectToAction(nameof(Details));
                }
            }
            return View(pwdViewModel);
        }

        private bool UtilisateurExists(Guid id)
        {
            return _context.Utilisateurs.Any(e => e.Id == id);
        }
    }
}
