using FastFood.DAL.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace FastFoot.Web.UI.Areas.FoltAdmin.Controllers
{
    //[Authorize(Roles = "Admin")]
    [Area("FoltAdmin")]
    public class UserManagmentController : Controller
    {
        private UserManager<AppUser> _userManager;
        private RoleManager<AppRole> _roleManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _imgPath = @"img/";


        public UserManagmentController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, IWebHostEnvironment webHostEnvironment)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _webHostEnvironment = webHostEnvironment;
        }


        #region UserOperation

        public IActionResult UserIndex()
        {

            List<AppUser> viewModels = new List<AppUser>();

            List<AppUser> appUsers = _userManager.Users.ToList();
            foreach (var item in appUsers)
            {
                AppUser viewModel = new AppUser
                {
                    Id = item.Id,
                    Name = item.Name,
                    Surname = item.Surname,
                    Country = item.Country,
                    Fincode = item.Fincode,
                    WhatsappNumber = item.WhatsappNumber,
                    Img = item.Img,
                    DateOfBirth = item.DateOfBirth,
                    Email = item.Email,
                    UserName = item.UserName,
                    Gender = item.Gender,

                };
                viewModels.Add(viewModel);
            }

            return View(viewModels);
        }


        public IActionResult UserCreate()
        {

            return View();

        }


        [HttpPost]
        public async Task<IActionResult> UserCreate(AppUser viewModel, IFormFile imageFile)
        {

            if (ModelState.IsValid)
            {

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

                AppUser user = new AppUser()
                {
                    Name = viewModel.Name,
                    Surname = viewModel.Surname,
                    UserName = viewModel.UserName,
                    Country = viewModel.Country,
                    Fincode = viewModel.Fincode,
                    WhatsappNumber = viewModel.WhatsappNumber,
                    Img = viewModel.Img,
                    DateOfBirth = viewModel.DateOfBirth,
                    Email = viewModel.Email,
                    Gender = viewModel.Gender,
                };
                IdentityResult result = await _userManager.CreateAsync(user, viewModel.PasswordHash);

                return RedirectToAction("UserIndex");
            }
            return View(viewModel);

        }


        public async Task<IActionResult> UserUpdate(string id)
        {
            AppUser user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            AppUser viewModel = new AppUser
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Country = user.Country,
                Img = user.Img,
                Fincode = user.Fincode,
                WhatsappNumber = user.WhatsappNumber,
                DateOfBirth = user.DateOfBirth,
                Email = user.Email,
                UserName = user.UserName,
                Gender = user.Gender
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> UserUpdate(AppUser viewModel, IFormFile imageFile)
        {

            //if (ModelState.IsValid)
            //{
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

            AppUser user = await _userManager.FindByIdAsync(viewModel.Id);

            if (user == null)
            {
                return NotFound();
            }

            user.Name = viewModel.Name;
            user.Surname = viewModel.Surname;
            user.Country = viewModel.Country;
            user.WhatsappNumber = viewModel.WhatsappNumber;
            user.Fincode = viewModel.Fincode;
            user.Img = viewModel.Img;
            user.DateOfBirth = viewModel.DateOfBirth;
            user.Email = viewModel.Email;
            user.UserName = viewModel.UserName;
            user.Gender = viewModel.Gender;

            IdentityResult result = await _userManager.UpdateAsync(user);

            return RedirectToAction("UserIndex");
            //}

            return View(viewModel);
        }


        public async Task<IActionResult> UserDelete(string id)
        {
            AppUser user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);

            }
            return RedirectToAction("UserIndex");
        }

        #endregion

        #region RoleOperation


        public async Task<string> UserRole(string id)
        {

            AppUser user = await _userManager.FindByIdAsync(id);
            IList<string> roles = await _userManager.GetRolesAsync(user);

            StringBuilder builder = new StringBuilder();
            foreach (var item in roles)
            {
                builder.Append(item + "; ");
            }
            return builder.ToString();
        }



        public IActionResult RoleIndex()
        {

            List<AppRole> viewModels = new List<AppRole>();

            List<AppRole> appRoles = _roleManager.Roles.ToList();

            foreach (var item in appRoles)
            {
                AppRole viewModel = new AppRole
                {
                    Id = item.Id,
                    Name = item.Name
                };
                viewModels.Add(viewModel);
            }


            return View(viewModels);
        }


        public IActionResult RoleCreate()
        {

            return View();

        }


        [HttpPost]
        public async Task<IActionResult> RoleCreate(AppRole viewModel)
        {
            if (ModelState.IsValid)
            {
                AppRole role = new AppRole()
                {
                    Name = viewModel.Name
                };
                IdentityResult result = await _roleManager.CreateAsync(role);

                return RedirectToAction("RoleIndex");


            }
            return View(viewModel);

        }


        public async Task<IActionResult> RoleUpdate(string id)
        {
            AppRole role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            AppRole viewModel = new AppRole
            {
                Id = role.Id,
                Name = role.Name
            };

            return View(viewModel);
        }


        [HttpPost]
        public async Task<IActionResult> RoleUpdate(AppRole viewModel)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View(viewModel);
            //}

            AppRole role = await _roleManager.FindByIdAsync(viewModel.Id);
            if (role == null)
            {
                return NotFound();
            }

            role.Name = viewModel.Name;
            IdentityResult result = await _roleManager.UpdateAsync(role);


            return RedirectToAction("RoleIndex");


            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(viewModel);
        }


        [HttpGet]
        public async Task<IActionResult> RoleDelete(string id)
        {

            AppRole role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
                IdentityResult result = await _roleManager.DeleteAsync(role);
            }

            return RedirectToAction("RoleIndex");

        }
        #endregion
    }
}
