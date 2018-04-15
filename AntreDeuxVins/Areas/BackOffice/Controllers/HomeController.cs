using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AntreDeuxVins.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AntreDeuxVins.Areas.BackOffice.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("BackOffice")]
    public class HomeController : Controller
    {
        private readonly AntreDeuxVinsDbContext _context;

        public HomeController(AntreDeuxVinsDbContext context)
        {
            _context = context;
        }
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.AlimentsSize = _context.Aliments.Count();
            ViewBag.CavesSize = _context.Caves.Count();
            ViewBag.CouleursSize = _context.Couleurs.Count();
            ViewBag.PaysSize = _context.Pays.Count();
            ViewBag.RegionsSize = _context.Regions.Count();
            ViewBag.RolesSize = _context.Roles.Count();
            ViewBag.UtilisateursSize = _context.Utilisateurs.Count();
            ViewBag.VinsSize = _context.Vins.Count();
            ViewBag.LanguagesSize = _context.Languages.Count();
            ViewBag.EntitysSize = _context.LocalizableEntitys.Count();
            return View();
        }
    }
}