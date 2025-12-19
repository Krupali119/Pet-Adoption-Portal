using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PetAdoptionMVC.Models;
using System;

namespace PetAdoptionMVC.Controllers
{
    public class UserMastersController : Controller
    {
        private readonly string apiUrl = @"http://localhost:5161/swagger/index.html";

        public List<UserMaster> UserMasters { get; set; } = new List<UserMaster>();
        private readonly PetAdoptionContext _context;

        public UserMastersController(PetAdoptionContext context)
        {
            _context = context;

        }

        // GET: UserMastersController
        public ActionResult Index()
        {
            var users = _context.UserMasters.Include(o => o.RoleMaster).ToList();
            return View(users);
        }

        // GET: UserMastersController
        [HttpPost]
        public async Task<ActionResult> Index(string RoleName = null)
        {


            try
            {
                List<UserMaster> userMasters = new List<UserMaster>();
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.GetAsync(apiUrl + "UserMasters?RoleName=" + RoleName);
                    if (response.IsSuccessStatusCode)
                    {
                        var userMasterJson = await response.Content.ReadAsStringAsync();
                        userMasters = JsonConvert.DeserializeObject<List<UserMaster>>(userMasterJson);
                    }
                    else
                    {
                        // Handle unsuccessful API call
                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                    }
                }
                return View(userMasters);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                ModelState.AddModelError(string.Empty, "Error occurred while processing your request.");
                return View(new List<UserMaster>());
            }
        }


        // GET: UserMastersController/Details/5
        public ActionResult Details(int id)
        {
            UserMaster prod = new UserMaster();
            using (var http = new HttpClient())
            {
                var response = http.GetAsync(apiUrl + "UserMasters/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    var readTask = response.Content.ReadAsStringAsync();
                    readTask.Wait();
                    prod = JsonConvert.DeserializeObject<UserMaster>(readTask.Result);
                }
            }
            return View(prod);
        }

        // GET: UserMastersController/Register
        public ActionResult Register()
        {
            return View(new UserMaster());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserMaster usermaster,string roleSelect)
        {
            // Fixed RoleId for all users
            usermaster.RoleId = 2; // Assign RoleId 2 for all users

            
                // Check if the email already exists in the database
                var existingUser = _context.UserMasters.FirstOrDefault(u => u.Email == usermaster.Email);
                if (existingUser != null)
                {
                    ModelState.AddModelError("Email", "This email is already registered.");
                    return View(usermaster);
                }

                // Set default ActiveFlag to true
                usermaster.ActiveFlag = true;

                // Save the new user to the database
                _context.Add(usermaster);
                await _context.SaveChangesAsync();

                // Redirect to a "Registration Success" page or Login
                return RedirectToAction("Login" , "Home");  // Or another view
            

            // If the model state is invalid, return to the registration view with validation errors
            return View(usermaster);
        }

        // GET: UserMastersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserMastersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserMastersController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserMastersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserMastersController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var userMaster = await _context.UserMasters
                .FirstOrDefaultAsync(u => u.UserId == id);

            if (userMaster == null)
            {
                return NotFound();
            }

            return View(userMaster);
        }


        // POST: UserMastersController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userMaster = await _context.UserMasters.FindAsync(id);

            if (userMaster != null)
            {
                _context.UserMasters.Remove(userMaster);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

    }
}

