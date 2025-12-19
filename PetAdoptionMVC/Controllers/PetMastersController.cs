using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PetAdoptionMVC.Models;

namespace PetAdoptionMVC.Controllers
{
    public class PetMastersController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly PetAdoptionContext _context;


        public PetMastersController(PetAdoptionContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: PetMasters
        public async Task<IActionResult> AllPetDetails()
        {
            var pets = await _context.PetMasters
                                     .Include(p => p.PetCategoryMaster)
                                     .Include(p => p.PetBehaviorProfile) // Include Behavior Profile
                                     .Include(p => p.AdoptionRequests)  // Include Adoption Requests
                                     .ToListAsync();

            return View(pets);
        }

        // Details Action to View Pet Details
        public async Task<IActionResult> PetDetails(int id)
        {
            var pet = await _context.PetMasters
                                     .Include(p => p.PetCategoryMaster)
                                     .Include(p => p.PetBehaviorProfile)
                                     .Include(p => p.AdoptionRequests)
                                     .FirstOrDefaultAsync(p => p.PetId == id);

            if (pet == null) return NotFound();

            return View(pet);
        }

        public async Task<IActionResult> Index()
        {
            var petAdoptionContext = _context.PetMasters.Include(p => p.PetCategoryMaster);
            return View(await petAdoptionContext.ToListAsync());
        }

        // GET: PetMasters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PetMasters == null)
            {
                return NotFound();
            }

            var petMaster = await _context.PetMasters
                .Include(p => p.PetCategoryMaster)
                .FirstOrDefaultAsync(m => m.PetId == id);
            if (petMaster == null)
            {
                return NotFound();
            }

            return View(petMaster);
        }

        // GET: PetMasters/Create
        public IActionResult Create()
        {
            ViewBag.PetCategoryId = new SelectList(_context.PetCategoryMasters, "CategoryId", "CategoryName");
            ViewBag.GenderOptions = new SelectList(new[] { "Male", "Female", "Others" });
            ViewBag.StatusOptions = new SelectList(new[] { "Available", "Not Available" });

            return View(new PetMaster());
        }

        // POST: PetMasters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PetMaster petMaster)
        {
            //if (ModelState.IsValid)
            //{
            //    string uniqueFileName = UploadedFile(petMaster);
            //    petMaster.PetImage = uniqueFileName;
            //    _context.Attach(petMaster);
            //    _context.Entry(petMaster).State = EntityState.Added;
            //    _context.SaveChanges();
            //    return RedirectToAction(nameof(Index));
            //}
            //ViewData["PetCategoryId"] = new SelectList(_context.PetCategoryMasters, "CategoryId", "CategoryName", petMaster.PetCategoryId);
            //return View(petMaster);
            string uniqueFileName = UploadedFile(petMaster);
            petMaster.PetImage = uniqueFileName;
            _context.Attach(petMaster);
            _context.Entry(petMaster).State = EntityState.Added;
            _context.SaveChanges();
            ViewBag.PetCategoryId = new SelectList(_context.PetCategoryMasters, "CategoryId", "CategoryName", petMaster.PetCategoryId);
            ViewBag.GenderOptions = new SelectList(new[] { "Male", "Female", "Others" }, petMaster.Gender);
            ViewBag.StatusOptions = new SelectList(new[] { "Available", "Not Available" }, petMaster.Status);
            return RedirectToAction(nameof(Index));
        }
        private string UploadedFile(PetMaster petMaster)
        {
            string uniqueFileName = null;
            if (petMaster.ImageFile != null)
            {
                string uploadsfolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + petMaster.ImageFile.FileName;
                string filePath = Path.Combine(uploadsfolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    petMaster.ImageFile.CopyTo(fileStream);
                }
            }
            return uniqueFileName;

        }

        // GET: PetMasters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var petMaster = await _context.PetMasters.FindAsync(id);
            if (petMaster == null)
            {
                return NotFound();
            }

            ViewBag.PetCategoryId = new SelectList(_context.PetCategoryMasters, "CategoryId", "CategoryName", petMaster.PetCategoryId);
            ViewBag.GenderOptions = new SelectList(new[] { "Male", "Female", "Others" }, petMaster.Gender);
            ViewBag.StatusOptions = new SelectList(new[] { "Available", "Not Available" }, petMaster.Status);

            return View(petMaster);
        }

        // POST: PetMasters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PetMaster petMaster)
        {
            if (id != petMaster.PetId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string uniqueFileName = UploadedFile(petMaster);
                    if (!string.IsNullOrEmpty(uniqueFileName))
                    {
                        petMaster.PetImage = uniqueFileName;
                    }

                    _context.Update(petMaster);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PetMasterExists(petMaster.PetId))
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

            ViewBag.PetCategoryId = new SelectList(_context.PetCategoryMasters, "CategoryId", "CategoryName", petMaster.PetCategoryId);
            ViewBag.GenderOptions = new SelectList(new[] { "Male", "Female", "Others" }, petMaster.Gender);
            ViewBag.StatusOptions = new SelectList(new[] { "Available", "Not Available" }, petMaster.Status);

            return View(petMaster);
        }

        // GET: PetMasters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PetMasters == null)
            {
                return NotFound();
            }

            var petMaster = await _context.PetMasters
                .Include(p => p.PetCategoryMaster)
                .FirstOrDefaultAsync(m => m.PetId == id);
            if (petMaster == null)
            {
                return NotFound();
            }

            return View(petMaster);
        }

        // POST: PetMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PetMasters == null)
            {
                return Problem("Entity set 'PetAdoptionContext.PetMasters'  is null.");
            }
            var petMaster = await _context.PetMasters.FindAsync(id);
            if (petMaster != null)
            {
                _context.PetMasters.Remove(petMaster);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PetMasterExists(int id)
        {
            return _context.PetMasters.Any(e => e.PetId == id);
        }

        [HttpPost]
        public IActionResult SearchByCategory(int PetCategoryId)
        {
            List<PetMaster> pets;
            if (PetCategoryId == 0)
            {
                // If 0, fetch all products
                pets = _context.PetMasters.ToList();
            }
            else
            {
                // Fetch products by selected category
                pets = _context.PetMasters.Where(p => p.PetCategoryId == PetCategoryId).ToList();
            }

            ViewData["PetCategoryId"] = new SelectList(_context.PetMasters, "PetCategoryId", "CategoryName");
            return View("Index1", pets);
        }

        [HttpGet("ByCategory/{categoryId}")]
        public async Task<IActionResult> GetProductsByCategory(int PetCategoryId)
        {
            var products = await _context.PetMasters.Where(p => p.PetCategoryId == PetCategoryId).ToListAsync();
            return Ok(Index);


        }

        [HttpGet]
        public IActionResult Search(string CategoryName, string searchTerm)
        {
            var pets = _context.PetMasters.Include(o => o.PetCategoryMaster).AsQueryable();

            if (!string.IsNullOrEmpty(CategoryName))
            {
                pets = pets.Where(p => p.PetCategoryMaster.CategoryName == CategoryName);
            }

            if (!string.IsNullOrEmpty(searchTerm))
            {
                pets = pets.Where(p => p.PetName.Contains(searchTerm));
            }

            var result = pets.ToList();

            return View(result);
        }
    }
}
