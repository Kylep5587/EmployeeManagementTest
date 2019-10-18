/* ************************************************************
 *  
 *  **********************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirstDemo.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FirstDemo.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        /* Logs the user out
        **********************************/
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("index", "home"); // Redirects to the index action method of the home controller
        }


        /* Respond to request for login form - returns Login view
        **********************************/
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        /* Handle login form submittal
         * Recieves the LoginViewModel
        **********************************/
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

                // Redirect to home page if login successful
                if (result.Succeeded)
                {
                    return RedirectToAction("index", "home");                       //? Redirect user to index action of home controller
                }

                // Shows error if login fails
                ModelState.AddModelError(string.Empty, "Invalid Username or Password");
            }

            return View(); // Rerender the login form if validation failed
        }


        /* Respond to request for registration form
        **********************************/
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        /* Handle registration form submittal
        **********************************/
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);

                // Executes if user created
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);    //? isPersistent determines how long the login will last
                    return RedirectToAction("index", "home");                       //? Redirect user to index action of home controller
                }

                // Gather errors and add them to the model state
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(); // Rerender the register form if validation failed
        }
    }
}
