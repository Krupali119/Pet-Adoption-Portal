using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetAdoptionMVC.Models;
using System.Linq;

namespace PetAdoptionMVC.Controllers
{
    public class PetMedicalRecordsController : Controller
    {
        private readonly PetAdoptionContext _context;

        public PetMedicalRecordsController(PetAdoptionContext context)
        {
            _context = context;
        }

        // GET: View all medical records
        public IActionResult Index()
        {
            var medicalRecords = _context.PetMedicalRecords.Include(p => p.PetMaster).ToList();
            return View(medicalRecords);
        }

        // GET: Create medical record form
        public IActionResult Create()
        {
            ViewBag.Pets = _context.PetMasters.ToList(); // Fetch all pets for dropdown
            return View();
        }

        // POST: Save medical record to database
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PetMedicalRecords medicalRecord)
        {
            if (ModelState.IsValid)
            {
                _context.PetMedicalRecords.Add(medicalRecord);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Pets = _context.PetMasters.ToList();
            return View(medicalRecord);
        }

        // GET: Edit medical record
        public IActionResult Edit(int id)
        {
            var medicalRecord = _context.PetMedicalRecords.Find(id);
            if (medicalRecord == null)
            {
                return NotFound();
            }
            ViewBag.Pets = _context.PetMasters.ToList();
            return View(medicalRecord);
        }

        // POST: Update medical record
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(PetMedicalRecords medicalRecord)
        {
            if (ModelState.IsValid)
            {
                _context.PetMedicalRecords.Update(medicalRecord);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Pets = _context.PetMasters.ToList();
            return View(medicalRecord);
        }

        // GET: Delete confirmation
        public IActionResult Delete(int id)
        {
            var medicalRecord = _context.PetMedicalRecords.Include(p => p.PetMaster).FirstOrDefault(m => m.MedicalRecordId == id);
            if (medicalRecord == null)
            {
                return NotFound();
            }
            return View(medicalRecord);
        }

        // POST: Delete medical record
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var medicalRecord = _context.PetMedicalRecords.Find(id);
            if (medicalRecord != null)
            {
                _context.PetMedicalRecords.Remove(medicalRecord);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
