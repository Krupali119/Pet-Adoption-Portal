using BillingSystemMVC.Helpers;
using Humanizer.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PetAdoptionMVC.Models;

public class AdminController : Controller
{
    private readonly PetAdoptionContext _context;
    private readonly IConfiguration _configuration;

    public AdminController(PetAdoptionContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    [HttpGet]
    public IActionResult ReviewRequests()
    {
        var requests = _context.AdoptionRequests
                               .Where(r => r.Status == "Pending Approval" && r.ActiveFlag)
                               .Include(r => r.User)
                               .Include(r => r.Pet)
                               .ToList();

        return View(requests);
    }

    [HttpPost]
    public IActionResult ApproveAdoptionRequest(int requestId, int userId)
    {
        var adoptionRequest = _context.AdoptionRequests.FirstOrDefault(r => r.RequestId == requestId);

        if (adoptionRequest == null || !adoptionRequest.ActiveFlag)
            return BadRequest("Adoption request not found or already processed.");

        adoptionRequest.Status = "Approved";
        adoptionRequest.ActiveFlag = false;

        _context.SaveChanges();

        var emailHelper = new EmailHelper(_configuration);
        var user = _context.UserMasters.FirstOrDefault(u => u.UserId == 1);
        if (user != null)
        {
            var subject = "Adoption Request Approved";
            var body = $"<p>Dear {user.FullName},</p><p>Your adoption request for has been approved. Congratulations!</p>";
            emailHelper.SendEmail(user.Email, subject, body);

        }

        return RedirectToAction("ReviewRequests");
    }

    [HttpPost]
    public IActionResult RejectAdoptionRequest(int requestId , int userId)
    {
        var adoptionRequest = _context.AdoptionRequests.FirstOrDefault(r => r.RequestId == requestId);

        if (adoptionRequest == null || !adoptionRequest.ActiveFlag)
            return BadRequest("Adoption request not found or already processed.");

        adoptionRequest.Status = "Rejected";
        adoptionRequest.ActiveFlag = false;

        _context.SaveChanges();
        var emailHelper = new EmailHelper(_configuration);
        var user = _context.UserMasters.FirstOrDefault(u => u.UserId == 1);
        if (user != null)
        {
            var subject = "Adoption Request Rejected";
            var body = $"<p>Dear {user.FullName},</p><p>We regret to inform you that your adoption request for has been Rejected. Congratulations!</p>";
            emailHelper.SendEmail(user.Email, subject, body);

        }

        NotifyUserOnRejection(adoptionRequest);

        return RedirectToAction("ReviewRequests");
    }

    private void NotifyUserOnRejection(AdoptionRequest request)
    {
        var user = _context.UserMasters.FirstOrDefault(u => u.UserId == request.UserId);
        if (user != null)
        {
            // Notify user via email or other mechanism
            Console.WriteLine($"Email sent to {user.Email}: Your adoption request (ID: {request.RequestId}) has been rejected.");
        }
    }
}
