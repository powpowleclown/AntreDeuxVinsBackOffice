using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AntreDeuxVins.Data;
using AntreDeuxVinsModel;

namespace AntreDeuxVins.Areas.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Couleurs")]
    public class CouleursController : Controller
    {
        private readonly AntreDeuxVinsDbContext _context;

        public CouleursController(AntreDeuxVinsDbContext context)
        {
            _context = context;
        }

        // GET: api/Couleurs
        [HttpGet]
        public IEnumerable<Couleur> GetCouleurs()
        {
            return _context.Couleurs;
        }

        // GET: api/Couleurs/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCouleur([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var couleur = await _context.Couleurs.SingleOrDefaultAsync(m => m.Id == id);

            if (couleur == null)
            {
                return NotFound();
            }

            return Ok(couleur);
        }

        // PUT: api/Couleurs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCouleur([FromRoute] int id, [FromBody] Couleur couleur)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != couleur.Id)
            {
                return BadRequest();
            }

            _context.Entry(couleur).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CouleurExists(id))
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

        // POST: api/Couleurs
        [HttpPost]
        public async Task<IActionResult> PostCouleur([FromBody] Couleur couleur)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Couleurs.Add(couleur);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCouleur", new { id = couleur.Id }, couleur);
        }

        // DELETE: api/Couleurs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCouleur([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var couleur = await _context.Couleurs.SingleOrDefaultAsync(m => m.Id == id);
            if (couleur == null)
            {
                return NotFound();
            }

            _context.Couleurs.Remove(couleur);
            await _context.SaveChangesAsync();

            return Ok(couleur);
        }

        private bool CouleurExists(int id)
        {
            return _context.Couleurs.Any(e => e.Id == id);
        }
    }
}