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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [Produces("application/json")]
    [Route("api/Aliments")]
    public class AlimentsController : Controller
    {
        private readonly AntreDeuxVinsDbContext _context;

        public AlimentsController(AntreDeuxVinsDbContext context)
        {
            _context = context;
        }

        // GET: api/Aliments
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [HttpGet]
        public IEnumerable<Aliment> GetAliments()
        {
            return _context.Aliments;
        }

        // GET: api/Aliments/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAliment([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var aliment = await _context.Aliments.SingleOrDefaultAsync(m => m.Id == id);

            if (aliment == null)
            {
                return NotFound();
            }

            return Ok(aliment);
        }

        // PUT: api/Aliments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAliment([FromRoute] int id, [FromBody] Aliment aliment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != aliment.Id)
            {
                return BadRequest();
            }

            _context.Entry(aliment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlimentExists(id))
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

        // POST: api/Aliments
        [HttpPost]
        public async Task<IActionResult> PostAliment([FromBody] Aliment aliment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Aliments.Add(aliment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAliment", new { id = aliment.Id }, aliment);
        }

        // DELETE: api/Aliments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAliment([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var aliment = await _context.Aliments.SingleOrDefaultAsync(m => m.Id == id);
            if (aliment == null)
            {
                return NotFound();
            }

            _context.Aliments.Remove(aliment);
            await _context.SaveChangesAsync();

            return Ok(aliment);
        }

        private bool AlimentExists(int id)
        {
            return _context.Aliments.Any(e => e.Id == id);
        }
    }
}