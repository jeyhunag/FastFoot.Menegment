using FastFood.DAL.Data;
using FastFoot.Web.UI.Areas.Controllers;
using FastFoot.Web.UI.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FastFoot.Web.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _imgPath = @"img/";
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
               IWebHostEnvironment webHostEnvironment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> SignIn()
        {
       
            return View();
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
                return RedirectToAction("Index", "Homes");

             
            }
            else
            {
                ModelState.AddModelError("", "Invalid username or password");

            }
            return RedirectToAction("Index", "Homes", homeViewModel);
        }

        public async Task<IActionResult> SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(HomeViewModel homeViewModel, SignUpViewModel model)
        {
            model = homeViewModel.SignUpViewModel;

        
            AppUser user = new AppUser { UserName = model.UserName, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {


                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction(nameof(HomeController.Index), "Homes");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
          

            return View(homeViewModel);
        }

        public async Task<IActionResult> ProfileSettings(string id)
        {
            var roleName = "";
            AppUser user = await _userManager.FindByIdAsync(id);

            var users = await _userManager.GetUserAsync(HttpContext.User);

            if (users != null)
            {
                // Check if the user has any roles
                var roles = await _userManager.GetRolesAsync(users);
                if (roles.Count()>0)
                {
                    roleName = roles.FirstOrDefault(); 
                }
                if (roles.Any())
                {
                    // For simplicity, assume the user has only one role
                    var roleId = await _userManager.GetRolesAsync(users);

                    
                }
                else
                {
                    // Handle the case where the user doesn't have any roles
                    // ...
                }
            }
            ViewBag.role = roleName;
            if (user == null)
            {
                return NotFound();
            }

            ProfileViewModel viewModel = new ProfileViewModel
            {
                Id = user.Id,
                Name = user.Name,
                WhatsappNumber = user.WhatsappNumber,
                Fincode = user.Fincode,
                Surname = user.Surname,
                Country = user.Country,
                Img = user.Img,
                DateOfBirth = user.DateOfBirth,
                Email = user.Email,
                UserName = user.UserName,
                Gender = user.Gender,
            };

            HomeViewModel homeViewModel = new HomeViewModel
            {
                ProfileViewModel = viewModel
            };
            return View(homeViewModel);
        }



        [HttpPost]
        public async Task<IActionResult> ProfileSettings(HomeViewModel homeViewModel, ProfileViewModel viewModel, IFormFile imageFile)
        {
            viewModel = homeViewModel.ProfileViewModel;

            if (imageFile != null && imageFile.Length > 0)
            {
                var imagePath = _imgPath + imageFile.FileName;
                var fullPath = Path.Combine(_webHostEnvironment.WebRootPath, imagePath);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                    viewModel.Img = imagePath;
                }
            }


            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                // Update user properties
                user.Name = viewModel.Name;
                user.Fincode = viewModel.Fincode;
                user.WhatsappNumber = viewModel.WhatsappNumber;
                user.Surname = viewModel.Surname;
                user.Country = viewModel.Country;
                user.Img = viewModel.Img;
                user.DateOfBirth = viewModel.DateOfBirth;
                user.Email = viewModel.Email;
                user.UserName = viewModel.UserName;
                user.Gender = viewModel.Gender;



                if (!string.IsNullOrEmpty(viewModel.NewPassword))
                {
                    // Check if the current password is correct
                    if (await _userManager.CheckPasswordAsync(user, viewModel.Password))
                    {
                        // Update the password
                        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                        var result = await _userManager.ResetPasswordAsync(user, token, viewModel.NewPassword);

                        if (!result.Succeeded)
                        {
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError(string.Empty, error.Description);
                            }
                            return View(viewModel);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Incorrect current password.");
                        return View(viewModel);
                    }
                }

                var updateResult = await _userManager.UpdateAsync(user);
                if (!updateResult.Succeeded)
                {
                    foreach (var error in updateResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View(viewModel);
                }

                return RedirectToAction("Index", "Homes");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "User not found.");
            }

            return View(homeViewModel);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Homes");
        }
    }
}
