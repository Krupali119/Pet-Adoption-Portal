using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetAdoptionMVC.Models;
using System;
using System.Linq;

namespace PetAdoptionMVC.Controllers
{
    public class PaymentController : Controller
    {
        private readonly PetAdoptionContext _context;

        public PaymentController(PetAdoptionContext context)
        {
            _context = context;
        }

        // GET: PaymentOptions - Show available payment options
        [HttpGet]
        public IActionResult PaymentOptions(int requestId)
        {
            var adoptionRequest = _context.AdoptionRequests
                .Include(r => r.Pet) // Include the PetMaster entity
                .FirstOrDefault(r => r.RequestId == requestId && r.Status == "Approved");

            if (adoptionRequest == null)
                return NotFound("Adoption request not found or not approved.");

            // Ensure the pet exists
            var pet = adoptionRequest.Pet;
            if (pet == null)
                return NotFound("Pet associated with the adoption request not found.");

            // Create the PaymentOptionsViewModel
            var paymentViewModel = new PaymentOptionsViewModel
            {
                RequestId = requestId,
                Amount = pet.Price, // Fetch the price from PetMaster
                PaymentMethods = new List<string> { "Card", "WhatsApp" }
            };

            return View(paymentViewModel);
        }
        [HttpPost]
        public IActionResult ProcessPayment(int requestId, string paymentMethod, decimal amount, CardPayment cardPayment = null, WhatsAppPayment whatsappPayment = null)
        {
            // Get the adoption request to verify if it's valid
            var adoptionRequest = _context.AdoptionRequests.FirstOrDefault(r => r.RequestId == requestId);

            if (adoptionRequest == null || adoptionRequest.Status != "Approved")
                return BadRequest("Adoption request is not approved or doesn't exist.");

            // Create the Payment entry
            Payment payment = new Payment
            {
                PaymentDate = DateTime.Now,
                PaymentAmount = amount,
                TransactionId = Guid.NewGuid().ToString(),
                PaymentMethod = paymentMethod,
                PaymentStatus = "Pending", // Initially set to Pending
                ActiveFlag = true
            };

            // Process payment based on selected method (Card or WhatsApp)
            if (paymentMethod == "Card")
            {
                var cardPaymentEntry = new CardPayment
                {
                    CardHolderName = cardPayment.CardHolderName,
                    CardNumber = cardPayment.CardNumber,
                    ExpirationDate = cardPayment.ExpirationDate,
                    CVV = cardPayment.CVV,
                    SaveCard = cardPayment.SaveCard,
                    
                };

                // Save card payment details
                _context.CardPayments.Add(cardPaymentEntry);
                _context.SaveChanges();
                payment.CardPaymentId = cardPaymentEntry.CardPaymentId;
                payment.PaymentStatus = "Completed"; // Mark as completed after successful card processing
            }
            else if (paymentMethod == "WhatsApp")
            {
                // Automatically generate WhatsApp Transaction ID
                string generatedTransactionId = "WA-" + Guid.NewGuid().ToString(); // Simple random generation

                var whatsappPaymentEntry = new WhatsAppPayment
                {
                    UserPhoneNumber = whatsappPayment.UserPhoneNumber,
                    WhatsAppTransactionId = generatedTransactionId,
                    
                };

                // Save WhatsApp payment details
                _context.WhatsAppPayments.Add(whatsappPaymentEntry);
                _context.SaveChanges();
                payment.WhatsAppPaymentId = whatsappPaymentEntry.WhatsAppPaymentId;
                payment.PaymentStatus = "Completed"; // Mark as completed after successful WhatsApp payment
            }

            // Save the payment
            _context.Payments.Add(payment);

            // Create and save the AdoptionMaster record
            var adoption = new AdoptionMaster
            {
                RequestId = requestId,
                AdoptionDate = DateTime.Now,
                FinalStatus = "Completed",
                AdopterId = (int)HttpContext.Session.GetInt32("Id")
            };

            _context.AdoptionMasters.Add(adoption);

            // Update the adoption request status to "Completed" and set ActiveFlag to false
            adoptionRequest.Status = "Completed";
            adoptionRequest.ActiveFlag = true;

            // Save changes to the database
            _context.SaveChanges();

            // Return a view that confirms payment success
            return View("PaymentSuccess");
        }
    }
}
