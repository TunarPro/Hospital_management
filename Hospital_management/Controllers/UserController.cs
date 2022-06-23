using Hospital_management.CustomClasses;
using Hospital_management.CustomModels;
using Hospital_management.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Hospital_management.Controllers
{
    public class UserController : Controller
    {
        private readonly Hospital_DbContext _db;

        public UserController(Hospital_DbContext _db)
        {
            this._db = _db;
        }

        [HttpGet]
        public IActionResult Index(string result)
        {
            if (result != null)
                ViewBag.Result = result;

            return View();
        }

        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(SigninUser customUser)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Views/User/Index.cshtml");
            }

            var u = _db.Users
                .Include(x => x.UsersStatus)
                .SingleOrDefault(x => x.UsersName.ToLower() == customUser.UserName.ToLower() && x.UsersPassword == Helper.CreateMD5(customUser.Password));

            if (u != null)
            {
                var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, u.UsersName),
                        new Claim("Id", u.UsersId.ToString()),
                        new Claim(ClaimTypes.Role, u.UsersStatus.StatusName)
                    };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                var props = new AuthenticationProperties();

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, props).Wait();

                if (u.UsersStatusId == 1)
                {
                    return RedirectToAction("Index", "Admin");
                }
                else if (u.UsersStatusId == 3)
                {
                    return RedirectToAction("Index", "Doctor");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError("Password", "İstifadəçi adı və ya şifrə yanlışdır");
            return View("~/Views/User/Index.cshtml");
        }


        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Exit()
        {
            HttpContext.SignOutAsync().Wait();
            return RedirectToAction("Index", "User");
        }
    }
}
