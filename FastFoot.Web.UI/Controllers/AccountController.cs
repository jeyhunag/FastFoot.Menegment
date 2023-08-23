using FastFood.DAL.Data;
using FastFoot.Web.UI.Areas.Controllers;
using FastFoot.Web.UI.Areas.FoltAdmin.ViewModels;
using FastFoot.Web.UI.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FastFoot.Web.UI.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<AppUser> _userManager;
        private SignInManager<AppUser> _signInManager;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> SignIn(HomeViewModel homeViewModel, SignInViewModel signInViewModel)
        {
            signInViewModel = homeViewModel.SignInViewModel;

            string UserName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;

            var appUser = await _userManager.FindByNameAsync(signInViewModel.UserName);
            if (appUser == null)
            {
                ModelState.AddModelError("", "Invalid username or password");
                return View(homeViewModel);
            }

            var result = await _signInManager.PasswordSignInAsync(appUser, signInViewModel.Password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                string? redirect = Request.Query["returnUrl"];
                if (string.IsNullOrWhiteSpace(redirect))
                    return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Invalid username or password");

            }
            return RedirectToAction("Index", "Home", homeViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(HomeViewModel homeViewModel, SignUpViewModel model)
        {
            model = homeViewModel.SignUpViewModel;

            //if (ModelState.IsValid)
            //{
            AppUser user = new AppUser { UserName = model.UserName, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {


                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            //}

            return View(homeViewModel);
        }


        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
