using HrApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using HrApp.Services;

namespace HrApp.Controllers
{
    public class AccountController : Controller
    {
        IdentityRepositoryInterface _repo;
        public AccountController(IdentityRepositoryInterface repo)
        {

            _repo = repo;
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
                var identityuser = await _repo.LoginAsync(user.UserName, null, user.Password);
                if (identityuser.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in identityuser.Errors)
                    {
                        ModelState.AddModelError("",error);
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
                var identityuser = await _repo.LoginAsync(null, user.Email, user.Password);
                if (identityuser.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in identityuser.Errors)
                    {
                        ModelState.AddModelError("", error);
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
                var registerresult = await _repo.RegisterAsync(registerData);

                if (registerresult.Succeeded)
                {
                    return View("Login");
                }
                else
                {
                    foreach (var error in registerresult.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }
            }
            return View(registerData);
        }
        #endregion
        public IActionResult Logout()
        {
            _repo.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
