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
    [Route("api/Regions")]
    public class RegionsController : Controller
    {
        private readonly AntreDeuxVinsDbContext _context;

        public RegionsController(AntreDeuxVinsDbContext context)
        {
            _context = context;
        }

        // GET: api/Regions
        [HttpGet]
        public IEnumerable<Region> GetRegions()
        {
            return _context.Regions;
        }

        // GET: api/Regions/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRegion([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var region = await _context.Regions.SingleOrDefaultAsync(m => m.Id == id);

            if (region == null)
            {
                return NotFound();
            }

            return Ok(region);
        }

        // PUT: api/Regions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRegion([FromRoute] int id, [FromBody] Region region)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != region.Id)
            {
                return BadRequest();
            }

            _context.Entry(region).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RegionExists(id))
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

        // POST: api/Regions
        [HttpPost]
        public async Task<IActionResult> PostRegion([FromBody] Region region)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Regions.Add(region);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRegion", new { id = region.Id }, region);
        }

        // DELETE: api/Regions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRegion([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var region = await _context.Regions.SingleOrDefaultAsync(m => m.Id == id);
            if (region == null)
            {
                return NotFound();
            }

            _context.Regions.Remove(region);
            await _context.SaveChangesAsync();

            return Ok(region);
        }

        private bool RegionExists(int id)
        {
            return _context.Regions.Any(e => e.Id == id);
        }
    }
}