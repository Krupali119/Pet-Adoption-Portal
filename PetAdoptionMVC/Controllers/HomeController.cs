
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using PetAdoptionMVC.Models;

namespace PetAdoptionMVC.Controllers
{

    public class HomeController : Controller
    {

        private readonly PetAdoptionContext context;


        public HomeController(PetAdoptionContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("UserRole") == "user")
            {
                var userSession = HttpContext.Session.GetString("UserSession");
                if (!string.IsNullOrEmpty(userSession))
                {
                    var user = JsonConvert.DeserializeObject<UserMaster>(userSession);
                    ViewBag.User = user;
                }
            }
            else
            {
                return RedirectToAction("Login");
            }
            return View();
        }


        public IActionResult about()
        {
            return View();
        }
        public IActionResult vet()
        {
            return View();
        }
        public IActionResult contact()
        {
            return View();
        }
        public IActionResult gallery()
        {
            return View();
        }

        public IActionResult blog()
        {
            return View();
        }
        public IActionResult services()
        {
            return View();
        }

        public IActionResult Admin()
        {
            if (HttpContext.Session.GetString("UserRole") == "admin")
            {
                ViewBag.MySession = HttpContext.Session.GetString("UserSession").ToString();
            }
            else
            {
                return RedirectToAction("Login");
            }
            return View();
        }



        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(UserMaster login)
        {
            if (login.Email == "krupali9@gmail.com" && login.Password == "kuku1909")
            {
                HttpContext.Session.SetString("UserSession", "admin");
                HttpContext.Session.SetString("UserRole", "admin");
                return RedirectToAction("Admin");
            }
            var asd = context.UserMasters.Include(u => u.RoleMaster).Where(s => s.Email == login.Email && s.Password == login.Password).FirstOrDefault();

            if (asd != null)
            {
                string user = JsonConvert.SerializeObject(asd);
                HttpContext.Session.SetInt32("Id", asd.UserId);
                HttpContext.Session.SetString("UserSession", user);
                if (asd.RoleMaster != null)
                {
                    HttpContext.Session.SetString("UserRole", asd.RoleMaster.RoleName); 

                    if (asd.RoleMaster.RoleName == "user")
                    {
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    return RedirectToAction("Login");
          

                }
            }
            else
            {
               
                ModelState.AddModelError(string.Empty, "Invalid email or password.");

            }
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }


        public IActionResult Profile()
        {
            var userSession = HttpContext.Session.GetString("UserSession");
            if (!string.IsNullOrEmpty(userSession))
            {
                var user = JsonConvert.DeserializeObject<UserMaster>(userSession);
                return View(user);
            }
            return RedirectToAction("Login");
        }

        [HttpPost]
        public IActionResult Profile(UserMaster user)
        {
            var userSession = HttpContext.Session.GetString("UserSession");
            if (!string.IsNullOrEmpty(userSession))
            {
                var sessionUser = JsonConvert.DeserializeObject<UserMaster>(userSession);
                var dbUser = context.UserMasters.Find(sessionUser.UserId);
                if (dbUser != null)
                {
                    dbUser.FullName = user.FullName;
                    dbUser.ContactNo = user.ContactNo;
                    context.SaveChanges();

                    var jsonSettings = new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    };
                    string updatedUserSession = JsonConvert.SerializeObject(dbUser, jsonSettings);
                    HttpContext.Session.SetString("UserSession", updatedUserSession);
                    ViewBag.User = dbUser;
                    ViewBag.Message = "Profile updated successfully.";
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "User not found.");
                }
            }
            else
            {
                return RedirectToAction("Login");
            }

            return View(user);
        }

        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userSession = HttpContext.Session.GetString("UserSession");
                if (!string.IsNullOrEmpty(userSession))
                {
                    var sessionUser = JsonConvert.DeserializeObject<UserMaster>(userSession);
                    var dbUser = context.UserMasters.Find(sessionUser.UserId);

                    if (dbUser != null)
                    {
                        if (dbUser.Password == model.CurrentPassword)
                        {
                            dbUser.Password = model.NewPassword;
                            context.SaveChanges();

                            var jsonSettings = new JsonSerializerSettings
                            {
                                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                            };
                            string updatedUserSession = JsonConvert.SerializeObject(dbUser, jsonSettings);
                            HttpContext.Session.SetString("UserSession", updatedUserSession);

                            ViewBag.Message = "Password changed successfully.";
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Current password is incorrect.");
                            ViewBag.ErrorMessage = "Current password is incorrect.";
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "User not found.");
                        ViewBag.ErrorMessage = "User not found.";
                    }
                }
                else
                {
                    return RedirectToAction("Login");
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = context.UserMasters.SingleOrDefault(u => u.Email == model.Email);
                if (user != null)
                {
                    var token = GenerateResetToken();
                    HttpContext.Session.SetString("ResetToken", token);
                    HttpContext.Session.SetString("ResetEmail", user.Email);

                    var resetLink = Url.Action("ResetPassword", "Home", new { token = token, email = user.Email }, Request.Scheme);
                    SendResetEmail(user.Email, resetLink);
                }

                ViewBag.Message = "If your email exists in our system, you will receive a password reset link.";
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult ResetPassword(string token, string email)
        {
            var sessionToken = HttpContext.Session.GetString("ResetToken");
            var sessionEmail = HttpContext.Session.GetString("ResetEmail");

            if (sessionToken == token && sessionEmail == email)
            {
                var model = new ResetPasswordViewModel { Token = token, Email = email };
                return View(model);
            }

            return RedirectToAction("Login");
        }

        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var sessionToken = HttpContext.Session.GetString("ResetToken");
                var sessionEmail = HttpContext.Session.GetString("ResetEmail");

                if (sessionToken == model.Token && sessionEmail == model.Email)
                {
                    var user = context.UserMasters.SingleOrDefault(u => u.Email == model.Email);
                    if (user != null)
                    {
                        user.Password = model.NewPassword;
                        context.SaveChanges();

                        HttpContext.Session.Remove("ResetToken");
                        HttpContext.Session.Remove("ResetEmail");

                        ViewBag.Message = "Password has been reset successfully.";
                        return RedirectToAction("Login");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid token or token expired.");
                }
            }

            return View(model);
        }

        private string GenerateResetToken()
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                var tokenData = new byte[32];
                rng.GetBytes(tokenData);
                return Convert.ToBase64String(tokenData);
            }
        }

        private void SendResetEmail(string email, string resetLink)
        {
            using (var message = new MailMessage("krupalidhodiya9gmail.com", email))
            {
                message.Subject = "Password Reset";
                message.Body = $"Please reset your password by clicking <a href=\"{resetLink}\">here</a>.";
                message.IsBodyHtml = true;

                using (var client = new SmtpClient("smtp.gmail.com", 587)) // Update SMTP server and port
                {
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential("krupalidhodiya9gmail.com", "kuku@1909");
                    client.EnableSsl = true;  // Use SSL/TLS for secure communication
                    client.Send(message);


                }
            }
        }



        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
