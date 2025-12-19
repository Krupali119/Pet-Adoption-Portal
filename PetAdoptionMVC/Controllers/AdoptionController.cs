using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using PetAdoptionMVC.Models;

namespace PetAdoptionMVC.Controllers
{
    public class AdoptionController : Controller
    {
        private readonly PetAdoptionContext _context;

        public AdoptionController(PetAdoptionContext context)
        {
            _context = context;
        }
        public IActionResult MyAdoptionRequests()
        {
           
            // Fetch the adoption requests for the logged-in user.
            var adoptionRequests = _context.AdoptionRequests
                .Include(r => r.Pet) // Include Pet details
                .Where(r => r.UserId == (int)HttpContext.Session.GetInt32("Id"))
                .OrderByDescending(r => r.RequestDate)
                .ToList();

            return View(adoptionRequests); // Pass the data to the View.
        }



        [HttpGet]
        public IActionResult CreateAdoptionRequest(int petId)
        {
           


            var adoptionRequest = new AdoptionRequest
            {
                RequestDate = DateTime.Now,
                Status = "Pending Approval",
                ActiveFlag = true,
                UserId = (int)HttpContext.Session.GetInt32("Id"),
                PetId = petId
            };

            _context.AdoptionRequests.Add(adoptionRequest);
            _context.SaveChanges();

            NotifyAdmin(adoptionRequest);

            return RedirectToAction("RequestSuccess");
        }

        private void NotifyAdmin(AdoptionRequest request)
        {
            // Notification logic to admin
            Console.WriteLine($"Admin notified of new adoption request: Request ID = {request.RequestId}");
        }

        public IActionResult RequestSuccess()
        {
            return View(); // Display success message
        }

    }
}
