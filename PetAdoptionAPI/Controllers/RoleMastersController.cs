using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace PetAdoptAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleMastersController : ControllerBase
    {
        private readonly PetAdoptionContext _context;

        public RoleMastersController(PetAdoptionContext context)
        {
            _context = context;
        }

        // GET: api/RoleMasters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleMaster>>> GetRoleMasters()
        {
            return await _context.RoleMasters.ToListAsync();
        }

        // GET: api/RoleMasters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RoleMaster>> GetRoleMaster(int id)
        {
            var roleMaster = await _context.RoleMasters.FindAsync(id);

            if (roleMaster == null)
            {
                return NotFound();
            }

            return roleMaster;
        }

        // PUT: api/RoleMasters/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoleMaster(int id, RoleMaster roleMaster)
        {
            if (id != roleMaster.RoleId)
            {
                return BadRequest();
            }

            _context.Entry(roleMaster).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoleMasterExists(id))
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

        // POST: api/RoleMasters
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RoleMaster>> PostRoleMaster(RoleMaster roleMaster)
        {
            _context.RoleMasters.Add(roleMaster);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRoleMaster", new { id = roleMaster.RoleId }, roleMaster);
        }

        // DELETE: api/RoleMasters/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoleMaster(int id)
        {
            var roleMaster = await _context.RoleMasters.FindAsync(id);
            if (roleMaster == null)
            {
                return NotFound();
            }

            _context.RoleMasters.Remove(roleMaster);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RoleMasterExists(int id)
        {
            return _context.RoleMasters.Any(e => e.RoleId == id);
        }
    }
}
