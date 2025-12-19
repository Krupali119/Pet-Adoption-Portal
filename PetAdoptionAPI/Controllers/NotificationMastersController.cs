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
    public class NotificationMastersController : ControllerBase
    {
        private readonly PetAdoptionContext _context;

        public NotificationMastersController(PetAdoptionContext context)
        {
            _context = context;
        }

        // GET: api/NotificationMasters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NotificationMaster>>> GetNotificationMasters()
        {
            return await _context.NotificationMasters.ToListAsync();
        }

        // GET: api/NotificationMasters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NotificationMaster>> GetNotificationMaster(int id)
        {
            var notificationMaster = await _context.NotificationMasters.FindAsync(id);

            if (notificationMaster == null)
            {
                return NotFound();
            }

            return notificationMaster;
        }

        // PUT: api/NotificationMasters/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNotificationMaster(int id, NotificationMaster notificationMaster)
        {
            if (id != notificationMaster.NotificationId)
            {
                return BadRequest();
            }

            _context.Entry(notificationMaster).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NotificationMasterExists(id))
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

        // POST: api/NotificationMasters
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<NotificationMaster>> PostNotificationMaster(NotificationMaster notificationMaster)
        {
            _context.NotificationMasters.Add(notificationMaster);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNotificationMaster", new { id = notificationMaster.NotificationId }, notificationMaster);
        }

        // DELETE: api/NotificationMasters/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotificationMaster(int id)
        {
            var notificationMaster = await _context.NotificationMasters.FindAsync(id);
            if (notificationMaster == null)
            {
                return NotFound();
            }

            _context.NotificationMasters.Remove(notificationMaster);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NotificationMasterExists(int id)
        {
            return _context.NotificationMasters.Any(e => e.NotificationId == id);
        }
    }
}
