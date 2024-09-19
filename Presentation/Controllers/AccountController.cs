using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Presentation.Controllers.Base;
using Presentation.Models;

namespace Presentation.Controllers
{
    public class AccountController : BaseController
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user != null && user.UserName != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, false, false);

                    if (result.Succeeded)
                    {
                        PopUpInfo($"{User.Identity?.Name} logged in");
                        return RedirectToAction(nameof(HomeController.Index), "Home");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                        return View(model);
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid email or password.");
                    return View(model);
                }
            }

            ModelState.AddModelError(string.Empty, "Error, password or email is not the default");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            var username = User.Identity?.Name;

            await _signInManager.SignOutAsync();
            PopUpInfo($"User {username} successfully logged out");
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = model.Username, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent : false);
                    PopUpSuccess($"User {user.UserName} created successfully");
                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }
                ModelState.AddModelError(string.Empty, "Error in registration");
            }
            return View(model);
        }
    }
}
