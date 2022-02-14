using HrApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrApp.Controllers
{
    public class AccountController : Controller
    {
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
        public IActionResult Register(RegisterViewModel registerData)
        {
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
