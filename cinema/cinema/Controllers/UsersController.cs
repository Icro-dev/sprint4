using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using cinema.Models;
using cinema.ViewModels;

namespace cinema.Controllers
{
    public class UsersController : Controller
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsersController(UserManager<IdentityUser> userManager
            , SignInManager<IdentityUser> signInManager
            , RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }
        
        [AllowAnonymous]
        public IActionResult AccessDenied(string returnUrl)
        {
            return new ObjectResult("Foei " + User?.Identity?.Name + "! Daar mag jij niet komen!");
            // betere mogelijkheid:
            //    //return RedirectToAction(returnUrl);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View("Users/Register");
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = new IdentityUser()
                {
                    UserName = model.Name,
                    Email = model.Email
                };

                IdentityResult result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("index", "home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View("Users/Register" ,model);
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View("Users/Login");
        }



        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                Microsoft.AspNetCore.Identity.SignInResult result
                    = await _signInManager.PasswordSignInAsync(
                        model.Name,
                        model.Password,
                        isPersistent: false, // aka: remember me?
                        lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Login failed.");
            }

            return View("Users/Login", model);
        }

    }
}
