using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetAdoptAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetBehaviorProfilesController : ControllerBase
    {
        private readonly PetAdoptionContext _context;

        public PetBehaviorProfilesController(PetAdoptionContext context)
        {
            _context = context;
        }

        // =========================
        // GET ALL
        // =========================
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PetBehaviorProfileDto>>> GetAll()
        {
            var data = await _context.PetBehaviorProfiles
                .Select(p => new PetBehaviorProfileDto
                {
                    BehaviorProfileId = p.BehaviorProfileId,
                    Behavior = p.Behavior,
                    Training = p.Training,
                    ActiveFlag = p.ActiveFlag,
                    PetId = p.PetId
                })
                .ToListAsync();

            return Ok(data);
        }

        // =========================
        // GET BY ID
        // =========================
        [HttpGet("{id}")]
        public async Task<ActionResult<PetBehaviorProfileDto>> GetById(int id)
        {
            var profile = await _context.PetBehaviorProfiles
                .Where(p => p.BehaviorProfileId == id)
                .Select(p => new PetBehaviorProfileDto
                {
                    BehaviorProfileId = p.BehaviorProfileId,
                    Behavior = p.Behavior,
                    Training = p.Training,
                    ActiveFlag = p.ActiveFlag,
                    PetId = p.PetId
                })
                .FirstOrDefaultAsync();

            if (profile == null)
                return NotFound();

            return Ok(profile);
        }

        // =========================
        // POST (CREATE)
        // =========================
        [HttpPost]
        public async Task<ActionResult<PetBehaviorProfileDto>> Create(PetBehaviorProfileDto dto)
        {
            var entity = new PetBehaviorProfile
            {
                Behavior = dto.Behavior,
                Training = dto.Training,
                ActiveFlag = dto.ActiveFlag,
                PetId = dto.PetId
            };

            _context.PetBehaviorProfiles.Add(entity);
            await _context.SaveChangesAsync();

            dto.BehaviorProfileId = entity.BehaviorProfileId;

            return CreatedAtAction(nameof(GetById), new { id = entity.BehaviorProfileId }, dto);
        }

        // =========================
        // PUT (UPDATE)
        // =========================
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, PetBehaviorProfileDto dto)
        {
            if (id != dto.BehaviorProfileId)
                return BadRequest();

            var entity = await _context.PetBehaviorProfiles.FindAsync(id);
            if (entity == null)
                return NotFound();

            entity.Behavior = dto.Behavior;
            entity.Training = dto.Training;
            entity.ActiveFlag = dto.ActiveFlag;
            entity.PetId = dto.PetId;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // =========================
        // DELETE
        // =========================
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _context.PetBehaviorProfiles.FindAsync(id);
            if (entity == null)
                return NotFound();

            _context.PetBehaviorProfiles.Remove(entity);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
    public class PetBehaviorProfileDto
    {
        public int BehaviorProfileId { get; set; }
        public string Behavior { get; set; }
        public string Training { get; set; }
        public bool ActiveFlag { get; set; }
        public int PetId { get; set; }
    }

}
