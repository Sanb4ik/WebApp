using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using System.Linq;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {

        private DatabaseContext db;

        public HomeController(DatabaseContext context)
        {
            db = context;
        }

        [Authorize]
        public IActionResult Index()
        {

            if (db.Users.Any(u => u.Email == User.Identity.Name))
            {

                if (db.Users.First(u => u.Email == User.Identity.Name).Status != "blocked")
                    return View(db.Users);
                else
                    return RedirectToAction("Login", "Account");

            }
            else
            {
                HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return RedirectToAction("Login", "Account");
            }

            
        }

        public IActionResult Delete(int[] Ids)
        {

            if (IsUserInvalid(User.Identity.Name))
                return RedirectToAction("Login", "Account");

            foreach (int id in Ids)
            {
                var ObjectToDelete = db.Users.Find(id);
                db.Users.Remove(ObjectToDelete);
            }

            db.SaveChanges();

            return Redirect("~/");

        }

        public bool IsUserInvalid(string Email)
        {

            User user = db.Users.First(u => u.Email == Email);

            if (user.Status == "ok")
                return false;
            else
                return true;

        }

        public IActionResult Block(int[] Ids)
        {

            if (IsUserInvalid(User.Identity.Name))
                return RedirectToAction("Login", "Account");

            foreach (int id in Ids)
            {
                var ObjectToDelete = db.Users.Find(id);
                db.Users.Find(id).Status = "blocked";
            }

            db.SaveChanges();

            return Redirect("~/");

        }

        public IActionResult Unblock(int[] Ids)
        {

            if (IsUserInvalid(User.Identity.Name))
                return RedirectToAction("Login", "Account");

            foreach (int id in Ids)
            {
                var ObjectToDelete = db.Users.Find(id);
                db.Users.Find(id).Status = "ok";
            }

            db.SaveChanges();

            return Redirect("~/");

        }

    }
}
