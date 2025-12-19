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
    public class PetCategoryMastersController : ControllerBase
    {
        private readonly PetAdoptionContext _context;

        public PetCategoryMastersController(PetAdoptionContext context)
        {
            _context = context;
        }

        // GET: api/PetCategoryMasters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PetCategoryMaster>>> GetPetCategoryMasters()
        {
            return await _context.PetCategoryMasters.ToListAsync();
        }

        // GET: api/PetCategoryMasters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PetCategoryMaster>> GetPetCategoryMaster(int id)
        {
            var petCategoryMaster = await _context.PetCategoryMasters.FindAsync(id);

            if (petCategoryMaster == null)
            {
                return NotFound();
            }

            return petCategoryMaster;
        }

        // PUT: api/PetCategoryMasters/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPetCategoryMaster(int id, PetCategoryMaster petCategoryMaster)
        {
            if (id != petCategoryMaster.CategoryId)
            {
                return BadRequest();
            }

            _context.Entry(petCategoryMaster).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PetCategoryMasterExists(id))
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

        // POST: api/PetCategoryMasters
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PetCategoryMaster>> PostPetCategoryMaster(PetCategoryMaster petCategoryMaster)
        {
            _context.PetCategoryMasters.Add(petCategoryMaster);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPetCategoryMaster", new { id = petCategoryMaster.CategoryId }, petCategoryMaster);
        }

        // DELETE: api/PetCategoryMasters/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePetCategoryMaster(int id)
        {
            var petCategoryMaster = await _context.PetCategoryMasters.FindAsync(id);
            if (petCategoryMaster == null)
            {
                return NotFound();
            }

            _context.PetCategoryMasters.Remove(petCategoryMaster);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PetCategoryMasterExists(int id)
        {
            return _context.PetCategoryMasters.Any(e => e.CategoryId == id);
        }
    }
}
