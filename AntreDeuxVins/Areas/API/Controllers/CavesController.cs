using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AntreDeuxVins.Data;
using AntreDeuxVinsModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace AntreDeuxVins.Areas.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Caves")]
    public class CavesController : Controller
    {
        private readonly AntreDeuxVinsDbContext _context;
        private readonly UserManager<Utilisateur> _userManager;

        public CavesController(AntreDeuxVinsDbContext context, UserManager<Utilisateur> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/Caves
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [HttpGet]
        public IEnumerable<Cave> GetCaves()
        {
            return _context.Caves;
        }

        // GET: api/Caves/5
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCave([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cave = await _context.Caves.SingleOrDefaultAsync(m => m.Id == id);

            if (cave == null)
            {
                return NotFound();
            }

            return Ok(cave);
        }

        // GET: api/Caves/5
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User")]
        [HttpGet("GetCaveByUserId/{userid}")]
        public async Task<IActionResult> GetCaveByUserId([FromRoute] string userid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.Id == Guid.Parse(userid));
            var cave = await _context.Caves.Include(c => c.Utilisateur).Include(c => c.Vins).ThenInclude(v => v.Couleur).Include(c => c.Vins).ThenInclude(v => v.Pays).Include(c => c.Vins).ThenInclude(v => v.Region).SingleOrDefaultAsync(c => c.Utilisateur.Id == user.Id);

            if (cave == null)
            {
                return NotFound();
            }

            return Ok(cave);
        }

        // PUT: api/Caves/5
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCave([FromRoute] int id, [FromBody] Cave cave)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cave.Id)
            {
                return BadRequest();
            }

            _context.Entry(cave).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CaveExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // PUT: api/Caves/5
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User")]
        [HttpPut("PutCaveByUserId/{userid}")]
        public async Task<IActionResult> PutCaveByUserId([FromRoute] string userid, [FromBody] Cave cave)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.Id == Guid.Parse(userid));
            var oldcave = await _context.Caves.SingleOrDefaultAsync(c => c.Utilisateur.Id == user.Id);

            if (oldcave.Id != cave.Id)
            {
                return BadRequest();
            }

            _context.Entry(cave).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CaveExists(oldcave.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Caves
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> PostCave([FromBody] Cave cave)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Caves.Add(cave);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCave", new { id = cave.Id }, cave);
        }

        // POST: api/Caves
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User")]
        [HttpPost("PostCaveByUserId/{userid}")]
        public async Task<IActionResult> PostCaveByUserId([FromRoute] string userid, [FromBody] Cave cave)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var caveuser = await _context.Caves.SingleOrDefaultAsync(c => c.Utilisateur.Id == Guid.Parse(userid));
            if(cave != null)
            {
                return BadRequest();
            }

            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.Id == Guid.Parse(userid));
            cave.Utilisateur = user;
            _context.Caves.Add(cave);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCaveByUserId", new { userid = user.Id }, cave);
        }

        // DELETE: api/Caves/5
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCave([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cave = await _context.Caves.SingleOrDefaultAsync(m => m.Id == id);
            if (cave == null)
            {
                return NotFound();
            }

            _context.Caves.Remove(cave);
            await _context.SaveChangesAsync();

            return Ok(cave);
        }

        private bool CaveExists(int id)
        {
            return _context.Caves.Any(e => e.Id == id);
        }
    }
}