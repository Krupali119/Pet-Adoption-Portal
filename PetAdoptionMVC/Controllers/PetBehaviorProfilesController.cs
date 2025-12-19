using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PetAdoptionMVC.Models;

namespace PetAdoptionMVC.Controllers
{
    public class PetBehaviorProfilesController : Controller
    {
        private readonly PetAdoptionContext _context;

        public PetBehaviorProfilesController(PetAdoptionContext context)
        {
            _context = context;
        }

        // GET: PetBehaviorProfiles
        public async Task<IActionResult> Index()
        {
            var petAdoptionContext = _context.PetBehaviorProfiles.Include(p => p.PetMaster);
            return View(await petAdoptionContext.ToListAsync());
        }

        // GET: PetBehaviorProfiles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PetBehaviorProfiles == null)
            {
                return NotFound();
            }

            var petBehaviorProfile = await _context.PetBehaviorProfiles
                .Include(p => p.PetMaster)
                .FirstOrDefaultAsync(m => m.BehaviorProfileId == id);
            if (petBehaviorProfile == null)
            {
                return NotFound();
            }

            return View(petBehaviorProfile);
        }

        // GET: PetBehaviorProfiles/Create
        public IActionResult Create()
        {
            ViewBag.TrainingOptions = new SelectList(new[] { "1 month", "2 month", "3 month", "4 month", "5 month", "6 month", "7 month", "8 month", "9 month", "10 month", "11 month", "12 month", "1 year", "2 years", "More than 2 years" });
            ViewBag.PetId = new SelectList(_context.PetMasters, "PetId", "PetName");
            return View();
        }

        // POST: PetBehaviorProfiles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BehaviorProfileId,Behavior,Training,ActiveFlag,PetId")] PetBehaviorProfile petBehaviorProfile)
        {
            if (ModelState.IsValid)
            {
                _context.Add(petBehaviorProfile);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.TrainingOptions = new SelectList(new[] { "1 month", "2 month", "3 month", "4 month", "5 month", "6 month", "7 month", "8 month", "9 month", "10 month", "11 month", "12 month", "1 year", "2 years", "More than 2 years" });
            ViewBag.PetId = new SelectList(_context.PetMasters, "PetId", "PetName", petBehaviorProfile.PetId);
            return View(petBehaviorProfile);
        }

        // GET: PetBehaviorProfiles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PetBehaviorProfiles == null)
            {
                return NotFound();
            }

            var petBehaviorProfile = await _context.PetBehaviorProfiles.FindAsync(id);
            if (petBehaviorProfile == null)
            {
                return NotFound();
            }

            ViewBag.TrainingOptions = new SelectList(new[] { "1 month", "2 month", "3 month", "4 month", "5 month", "6 month", "7 month", "8 month", "9 month", "10 month", "11 month", "12 month", "1 year", "2 years", "More than 2 years" });
            ViewBag.PetId = new SelectList(_context.PetMasters, "PetId", "PetName", petBehaviorProfile.PetId);
            return View(petBehaviorProfile);
        }

        // POST: PetBehaviorProfiles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BehaviorProfileId,Behavior,Training,ActiveFlag,PetId")] PetBehaviorProfile petBehaviorProfile)
        {
            if (id != petBehaviorProfile.BehaviorProfileId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(petBehaviorProfile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PetBehaviorProfileExists(petBehaviorProfile.BehaviorProfileId))
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

            ViewBag.TrainingOptions = new SelectList(new[] { "1 month", "2 month", "3 month", "4 month", "5 month", "6 month", "7 month", "8 month", "9 month", "10 month", "11 month", "12 month", "1 year", "2 years", "More than 2 years" });
            ViewBag.PetId = new SelectList(_context.PetMasters, "PetId", "PetName", petBehaviorProfile.PetId);
            return View(petBehaviorProfile);
        }

        // GET: PetBehaviorProfiles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PetBehaviorProfiles == null)
            {
                return NotFound();
            }

            var petBehaviorProfile = await _context.PetBehaviorProfiles
                .Include(p => p.PetMaster)
                .FirstOrDefaultAsync(m => m.BehaviorProfileId == id);
            if (petBehaviorProfile == null)
            {
                return NotFound();
            }

            return View(petBehaviorProfile);
        }

        // POST: PetBehaviorProfiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PetBehaviorProfiles == null)
            {
                return Problem("Entity set 'PetAdoptionContext.PetBehaviorProfiles' is null.");
            }

            var petBehaviorProfile = await _context.PetBehaviorProfiles.FindAsync(id);
            if (petBehaviorProfile != null)
            {
                _context.PetBehaviorProfiles.Remove(petBehaviorProfile);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PetBehaviorProfileExists(int id)
        {
            return _context.PetBehaviorProfiles.Any(e => e.BehaviorProfileId == id);
        }
    }
}
