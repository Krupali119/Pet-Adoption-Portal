using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PetAdoptionMVC.Models;

namespace PetAdoptionMVC.Controllers
{
    public class FeedbackMastersController : Controller
    {
        private readonly PetAdoptionContext _context;

        public FeedbackMastersController(PetAdoptionContext context)
        {
            _context = context;
        }

        // GET: FeedbackMasters
        public async Task<IActionResult> Index()
        {
            var petAdoptionContext = _context.FeedbackMasters.Include(f => f.UserMaster);
            return View(await petAdoptionContext.ToListAsync());
        }

        // GET: FeedbackMasters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.FeedbackMasters == null)
            {
                return NotFound();
            }

            var feedbackMaster = await _context.FeedbackMasters
                .Include(f => f.UserMaster)
                .FirstOrDefaultAsync(m => m.FeedbackId == id);
            if (feedbackMaster == null)
            {
                return NotFound();
            }

            return View(feedbackMaster);
        }

        // GET: FeedbackMasters/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.UserMasters, "UserId", "FullName");
            ViewData["RatingOptions"] = new SelectList(Enumerable.Range(1, 5));
            return View();
        }

        // POST: FeedbackMasters/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FeedbackId,Feedback,Rating,UserId,ActiveFlag")] FeedbackMaster feedbackMaster)
        {
            if (ModelState.IsValid)
            {
                feedbackMaster.CreatedDate = DateTime.Now; // Automatically set CreatedDate
                _context.Add(feedbackMaster);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.UserMasters, "UserId", "FullName", feedbackMaster.UserId);
            ViewData["RatingOptions"] = new SelectList(Enumerable.Range(1, 5), feedbackMaster.Rating);
            return View(feedbackMaster);
        }

        // GET: FeedbackMasters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.FeedbackMasters == null)
            {
                return NotFound();
            }

            var feedbackMaster = await _context.FeedbackMasters.FindAsync(id);
            if (feedbackMaster == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.UserMasters, "UserId", "FullName", feedbackMaster.UserId);
            ViewData["RatingOptions"] = new SelectList(Enumerable.Range(1, 5), feedbackMaster.Rating);
            return View(feedbackMaster);
        }

        // POST: FeedbackMasters/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FeedbackId,Feedback,Rating,UserId,ActiveFlag,CreatedDate")] FeedbackMaster feedbackMaster)
        {
            if (id != feedbackMaster.FeedbackId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(feedbackMaster);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FeedbackMasterExists(feedbackMaster.FeedbackId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.UserMasters, "UserId", "FullName", feedbackMaster.UserId);
            ViewData["RatingOptions"] = new SelectList(Enumerable.Range(1, 5), feedbackMaster.Rating);
            return View(feedbackMaster);
        }

        // GET: FeedbackMasters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.FeedbackMasters == null)
            {
                return NotFound();
            }

            var feedbackMaster = await _context.FeedbackMasters
                .Include(f => f.UserMaster)
                .FirstOrDefaultAsync(m => m.FeedbackId == id);
            if (feedbackMaster == null)
            {
                return NotFound();
            }

            return View(feedbackMaster);
        }

        // POST: FeedbackMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.FeedbackMasters == null)
            {
                return Problem("Entity set 'PetAdoptionContext.FeedbackMasters' is null.");
            }
            var feedbackMaster = await _context.FeedbackMasters.FindAsync(id);
            if (feedbackMaster != null)
            {
                _context.FeedbackMasters.Remove(feedbackMaster);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FeedbackMasterExists(int id)
        {
            return _context.FeedbackMasters.Any(e => e.FeedbackId == id);
        }
    }
}
