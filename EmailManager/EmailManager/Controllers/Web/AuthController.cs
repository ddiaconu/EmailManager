using EmailManager.Models;
using EmailManager.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailManager.Controllers.Web
{
    public class AuthController : Controller
    {
        private SignInManager<User> _signInManager;

        public AuthController(SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
        }

        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "App");
            }
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var signinResult = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);
                if (signinResult.Succeeded)
                {
                    return RedirectToAction("Index", "App");
                }
            }

            // Just say Login failed on all errors
            ModelState.AddModelError("", "Login Failed");

            return View();
        }

        public async Task<ActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                await _signInManager.SignOutAsync();
            }
            return RedirectToAction("Index", "App");
        }
    }
}
