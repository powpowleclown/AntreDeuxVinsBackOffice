using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AntreDeuxVins.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using AntreDeuxVinsModel;
using AntreDeuxVins.Data;
using AntreDeuxVins.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc.Localization;

namespace AntreDeuxVins.Controllers
{
    public class HomeController : Controller
    {
        private readonly AntreDeuxVinsDbContext _context;
        private readonly UserManager<Utilisateur> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly SignInManager<Utilisateur> _signInManager;

        public HomeController(AntreDeuxVinsDbContext context, UserManager<Utilisateur> userManager, SignInManager<Utilisateur> signInManager, RoleManager<Role> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp([Bind("Nom,Prenom,Email,Password,ConfirmPassword")] RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                Utilisateur utilisateur = new Utilisateur(registerViewModel.Email, registerViewModel.Nom, registerViewModel.Prenom, registerViewModel.Password);
                var role = await _roleManager.Roles.FirstAsync(r => r.Name == "User");
                utilisateur.Role = role;
                var result = await _userManager.CreateAsync(utilisateur, utilisateur.Password);
                
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(utilisateur, role.Name);
                    await _signInManager.SignInAsync(utilisateur, isPersistent: false);
                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }
            }
            return View(registerViewModel);
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(AuthenticationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Mail, model.Password, model.IsPersistent, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }
                
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
