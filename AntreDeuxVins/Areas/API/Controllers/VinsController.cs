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
using Microsoft.AspNetCore.Authentication.JwtBearer;

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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [HttpGet]
        public IEnumerable<Vin> GetVins()
        {
            return _context.Vins;
        }

        // GET: api/Vins
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User")]
        [HttpGet("GetVinsByCaveId/{caveid}")]
        public async Task<IEnumerable<Vin>> GetVinsByCaveId([FromRoute] int caveid)
        {
            var caves = await _context.Caves.Include(c => c.Utilisateur).Include(c => c.Vins).ThenInclude(v => v.Couleur).Include(c => c.Vins).ThenInclude(v => v.Pays).Include(c => c.Vins).ThenInclude(v => v.Region).SingleOrDefaultAsync(c => c.Id == caveid);
            return caves.Vins;
        }

        // GET: api/Vins/5
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
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

        // GET: api/Vins/5
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User")]
        [HttpGet("GetVinByCaveId/{caveid}/{id}")]
        public async Task<IActionResult> GetVinByCaveId([FromRoute] int caveid, int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vin = await _context.Vins.SingleOrDefaultAsync(m => m.Id == id && m.CaveId == caveid);

            if (vin == null)
            {
                return NotFound();
            }

            return Ok(vin);
        }


        // PUT: api/Vins/5
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
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

        // PUT: api/Vins/5
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User")]
        [HttpPut("PutVinByCaveId/{caveid}/{id}")]
        public async Task<IActionResult> PutVinByCaveId([FromRoute] int caveid, int id, [FromBody] Vin vin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vin.Id)
            {
                return BadRequest();
            }

            if (caveid != vin.CaveId)
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
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

        // POST: api/Vins
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User")]
        [HttpPost("PostVinByCaveId/{caveid}")]
        public async Task<IActionResult> PostVinByCaveId([FromRoute] int caveid, [FromBody] Vin vin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(caveid != vin.CaveId)
            {
                return BadRequest();
            }
            _context.Vins.Add(vin);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVinByCaveId", new { caveid = vin.CaveId, id = vin.Id }, vin);
        }

        // DELETE: api/Vins/5
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
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

        // DELETE: api/Vins/5
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [HttpDelete("DeleteVinByCaveId/{caveid}/{id}")]
        public async Task<IActionResult> DeleteVinByCaveId([FromRoute] int caveid, int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vin = await _context.Vins.SingleOrDefaultAsync(m => m.Id == id && m.CaveId == caveid);
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