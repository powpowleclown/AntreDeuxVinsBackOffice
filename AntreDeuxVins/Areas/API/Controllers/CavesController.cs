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
    [Route("api/Caves")]
    public class CavesController : Controller
    {
        private readonly AntreDeuxVinsDbContext _context;

        public CavesController(AntreDeuxVinsDbContext context)
        {
            _context = context;
        }

        // GET: api/Caves
        [HttpGet]
        public IEnumerable<Cave> GetCaves()
        {
            return _context.Caves;
        }

        // GET: api/Caves/5
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

        // PUT: api/Caves/5
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

        // POST: api/Caves
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

        // DELETE: api/Caves/5
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