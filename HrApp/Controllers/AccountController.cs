using HrApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrApp.Controllers
{
    public class AccountController : Controller
    {
        UserManager<IdentityUser> _userManager;
        SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult LoginWithUserName()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> LoginWithUserName(LoginUsernameViewModel user)
        {
            if (ModelState.IsValid)
            {
                var identityuser = await _userManager.FindByNameAsync(user.UserName);
                if (identityuser != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(identityuser, user.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid login attempt.");
                    }
                }
            }

            return View(user);
        }

        [HttpGet]
        public IActionResult LoginWithEmail()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> LoginWithEmail(LoginEmailViewModel user)
        {
            if (ModelState.IsValid)
            {
                var identityuser = await _userManager.FindByEmailAsync(user.Email);
                if (identityuser != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(identityuser, user.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid login attempt.");
                    }
                }
            }

            return View(user);
        }



        public IActionResult Login()
        {

            return View();
        }
        #region Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerData)
        {
            if (ModelState.IsValid)
            {

                var identityUser = new IdentityUser() { UserName = registerData.UserName, Email = registerData.Email};
                var result = await _userManager.CreateAsync(identityUser, registerData.Password);
                if (result.Succeeded)
                {
                    return View("Login");
                }
                else
                {
                    
                }
            }
            return View();
        }
        #endregion
        public IActionResult Logout()
        {
            return View();
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
