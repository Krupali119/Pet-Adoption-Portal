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
    public class CardPaymentsController : ControllerBase
    {
        private readonly PetAdoptionContext _context;

        public CardPaymentsController(PetAdoptionContext context)
        {
            _context = context;
        }

        // GET: api/CardPayments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CardPayment>>> GetCardPayments()
        {
            return await _context.CardPayments.ToListAsync();
        }

        // GET: api/CardPayments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CardPayment>> GetCardPayment(int id)
        {
            var cardPayment = await _context.CardPayments.FindAsync(id);

            if (cardPayment == null)
            {
                return NotFound();
            }

            return cardPayment;
        }

        // PUT: api/CardPayments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCardPayment(int id, CardPayment cardPayment)
        {
            if (id != cardPayment.CardPaymentId)
            {
                return BadRequest();
            }

            _context.Entry(cardPayment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CardPaymentExists(id))
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

        // POST: api/CardPayments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CardPayment>> PostCardPayment(CardPayment cardPayment)
        {
            _context.CardPayments.Add(cardPayment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCardPayment", new { id = cardPayment.CardPaymentId }, cardPayment);
        }

        // DELETE: api/CardPayments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCardPayment(int id)
        {
            var cardPayment = await _context.CardPayments.FindAsync(id);
            if (cardPayment == null)
            {
                return NotFound();
            }

            _context.CardPayments.Remove(cardPayment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CardPaymentExists(int id)
        {
            return _context.CardPayments.Any(e => e.CardPaymentId == id);
        }
    }
}
