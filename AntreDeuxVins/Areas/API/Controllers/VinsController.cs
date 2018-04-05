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
    [Route("api/Vins")]
    public class VinsController : Controller
    {
        private readonly AntreDeuxVinsDbContext _context;

        public VinsController(AntreDeuxVinsDbContext context)
        {
            _context = context;
        }

        // GET: api/Vins
        [HttpGet]
        public IEnumerable<Vin> GetVins()
        {
            return _context.Vins;
        }

        // GET: api/Vins/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVin([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vin = await _context.Vins.SingleOrDefaultAsync(m => m.Id == id);

            if (vin == null)
            {
                return NotFound();
            }

            return Ok(vin);
        }

        // PUT: api/Vins/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVin([FromRoute] int id, [FromBody] Vin vin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vin.Id)
            {
                return BadRequest();
            }

            _context.Entry(vin).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VinExists(id))
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

        // POST: api/Vins
        [HttpPost]
        public async Task<IActionResult> PostVin([FromBody] Vin vin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Vins.Add(vin);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVin", new { id = vin.Id }, vin);
        }

        // DELETE: api/Vins/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVin([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vin = await _context.Vins.SingleOrDefaultAsync(m => m.Id == id);
            if (vin == null)
            {
                return NotFound();
            }

            _context.Vins.Remove(vin);
            await _context.SaveChangesAsync();

            return Ok(vin);
        }

        private bool VinExists(int id)
        {
            return _context.Vins.Any(e => e.Id == id);
        }
    }
}