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
    public class WhatsAppPaymentsController : ControllerBase
    {
        private readonly PetAdoptionContext _context;

        public WhatsAppPaymentsController(PetAdoptionContext context)
        {
            _context = context;
        }

        // GET: api/WhatsAppPayments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WhatsAppPayment>>> GetWhatsAppPayments()
        {
            return await _context.WhatsAppPayments.ToListAsync();
        }

        // GET: api/WhatsAppPayments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WhatsAppPayment>> GetWhatsAppPayment(int id)
        {
            var whatsAppPayment = await _context.WhatsAppPayments.FindAsync(id);

            if (whatsAppPayment == null)
            {
                return NotFound();
            }

            return whatsAppPayment;
        }

        // PUT: api/WhatsAppPayments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWhatsAppPayment(int id, WhatsAppPayment whatsAppPayment)
        {
            if (id != whatsAppPayment.WhatsAppPaymentId)
            {
                return BadRequest();
            }

            _context.Entry(whatsAppPayment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WhatsAppPaymentExists(id))
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

        // POST: api/WhatsAppPayments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<WhatsAppPayment>> PostWhatsAppPayment(WhatsAppPayment whatsAppPayment)
        {
            _context.WhatsAppPayments.Add(whatsAppPayment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWhatsAppPayment", new { id = whatsAppPayment.WhatsAppPaymentId }, whatsAppPayment);
        }

        // DELETE: api/WhatsAppPayments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWhatsAppPayment(int id)
        {
            var whatsAppPayment = await _context.WhatsAppPayments.FindAsync(id);
            if (whatsAppPayment == null)
            {
                return NotFound();
            }

            _context.WhatsAppPayments.Remove(whatsAppPayment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WhatsAppPaymentExists(int id)
        {
            return _context.WhatsAppPayments.Any(e => e.WhatsAppPaymentId == id);
        }
    }
}
