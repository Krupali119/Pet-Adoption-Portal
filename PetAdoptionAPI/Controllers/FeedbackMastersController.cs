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
    public class FeedbackMastersController : ControllerBase
    {
        private readonly PetAdoptionContext _context;

        public FeedbackMastersController(PetAdoptionContext context)
        {
            _context = context;
        }

        // GET: api/FeedbackMasters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FeedbackMaster>>> GetFeedbackMasters()
        {
            return await _context.FeedbackMasters.ToListAsync();
        }

        // GET: api/FeedbackMasters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FeedbackMaster>> GetFeedbackMaster(int id)
        {
            var feedbackMaster = await _context.FeedbackMasters.FindAsync(id);

            if (feedbackMaster == null)
            {
                return NotFound();
            }

            return feedbackMaster;
        }

        // PUT: api/FeedbackMasters/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFeedbackMaster(int id, FeedbackMaster feedbackMaster)
        {
            if (id != feedbackMaster.FeedbackId)
            {
                return BadRequest();
            }

            _context.Entry(feedbackMaster).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FeedbackMasterExists(id))
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

        // POST: api/FeedbackMasters
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FeedbackMaster>> Create(FeedbackMaster feedbackMaster)
        {
            _context.FeedbackMasters.Add(feedbackMaster);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFeedbackMaster", new { id = feedbackMaster.FeedbackId }, feedbackMaster);
        }

        // DELETE: api/FeedbackMasters/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFeedbackMaster(int id)
        {
            var feedbackMaster = await _context.FeedbackMasters.FindAsync(id);
            if (feedbackMaster == null)
            {
                return NotFound();
            }

            _context.FeedbackMasters.Remove(feedbackMaster);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FeedbackMasterExists(int id)
        {
            return _context.FeedbackMasters.Any(e => e.FeedbackId == id);
        }
    }
}
