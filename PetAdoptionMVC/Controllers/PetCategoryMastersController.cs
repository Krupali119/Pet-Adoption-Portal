using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PetAdoptionMVC.Models;

namespace PetAdoptionMVC.Controllers
{
    public class PetCategoryMastersController : Controller
    {
        private readonly PetAdoptionContext _context;
       
        public PetCategoryMastersController(PetAdoptionContext context)
        {
            _context = context;
        }

        // GET: PetCategoryMasters
        public async Task<IActionResult> Index()
        {
              return View(await _context.PetCategoryMasters.ToListAsync());
        }

        // GET: PetCategoryMasters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PetCategoryMasters == null)
            {
                return NotFound();
            }

            var petCategoryMaster = await _context.PetCategoryMasters
                .FirstOrDefaultAsync(m => m.CategoryId == id);
            if (petCategoryMaster == null)
            {
                return NotFound();
            }

            return View(petCategoryMaster);
        }

        // GET: PetCategoryMasters/Create
        
        public IActionResult Create()
        {
            return View();
        }

        // POST: PetCategoryMasters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,CategoryName,ActiveFlag")] PetCategoryMaster petCategoryMaster)
        {
            if (ModelState.IsValid)
            {
                _context.Add(petCategoryMaster);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(petCategoryMaster);
        }

        // GET: PetCategoryMasters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PetCategoryMasters == null)
            {
                return NotFound();
            }

            var petCategoryMaster = await _context.PetCategoryMasters.FindAsync(id);
            if (petCategoryMaster == null)
            {
                return NotFound();
            }
            return View(petCategoryMaster);
        }

        // POST: PetCategoryMasters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryId,CategoryName,ActiveFlag")] PetCategoryMaster petCategoryMaster)
        {
            if (id != petCategoryMaster.CategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(petCategoryMaster);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PetCategoryMasterExists(petCategoryMaster.CategoryId))
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
            return View(petCategoryMaster);
        }

        // GET: PetCategoryMasters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PetCategoryMasters == null)
            {
                return NotFound();
            }

            var petCategoryMaster = await _context.PetCategoryMasters
                .FirstOrDefaultAsync(m => m.CategoryId == id);
            if (petCategoryMaster == null)
            {
                return NotFound();
            }

            return View(petCategoryMaster);
        }

        // POST: PetCategoryMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PetCategoryMasters == null)
            {
                return Problem("Entity set 'PetAdoptionContext.PetCategoryMasters'  is null.");
            }
            var petCategoryMaster = await _context.PetCategoryMasters.FindAsync(id);
            if (petCategoryMaster != null)
            {
                _context.PetCategoryMasters.Remove(petCategoryMaster);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PetCategoryMasterExists(int id)
        {
          return _context.PetCategoryMasters.Any(e => e.CategoryId == id);
        }
    }
}
