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
    [Route("api/Pays")]
    public class PaysController : Controller
    {
        private readonly AntreDeuxVinsDbContext _context;

        public PaysController(AntreDeuxVinsDbContext context)
        {
            _context = context;
        }

        // GET: api/Pays
        [HttpGet]
        public IEnumerable<Pays> GetPays()
        {
            return _context.Pays;
        }

        // GET: api/Pays/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPays([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pays = await _context.Pays.SingleOrDefaultAsync(m => m.Id == id);

            if (pays == null)
            {
                return NotFound();
            }

            return Ok(pays);
        }

        // PUT: api/Pays/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPays([FromRoute] int id, [FromBody] Pays pays)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pays.Id)
            {
                return BadRequest();
            }

            _context.Entry(pays).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaysExists(id))
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

        // POST: api/Pays
        [HttpPost]
        public async Task<IActionResult> PostPays([FromBody] Pays pays)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Pays.Add(pays);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPays", new { id = pays.Id }, pays);
        }

        // DELETE: api/Pays/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePays([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pays = await _context.Pays.SingleOrDefaultAsync(m => m.Id == id);
            if (pays == null)
            {
                return NotFound();
            }

            _context.Pays.Remove(pays);
            await _context.SaveChangesAsync();

            return Ok(pays);
        }

        private bool PaysExists(int id)
        {
            return _context.Pays.Any(e => e.Id == id);
        }
    }
}