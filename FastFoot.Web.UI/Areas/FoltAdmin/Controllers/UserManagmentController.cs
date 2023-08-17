using FastFood.DAL.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FastFoot.Web.UI.Areas.FoltAdmin.Controllers
{
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

            if (!ModelState.IsValid)
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
                    Img = viewModel.Img,
                    DateOfBirth = viewModel.DateOfBirth,
                    Email = viewModel.Email,
                    Gender = viewModel.Gender
                };
                IdentityResult result = await _userManager.CreateAsync(user, viewModel.Password);


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
                user.Img = viewModel.Img;
                user.DateOfBirth = viewModel.DateOfBirth;
                user.Email = viewModel.Email;
                user.UserName = viewModel.UserName;
                user.Gender = viewModel.Gender;

                IdentityResult result = await _userManager.UpdateAsync(user);


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
    }
}
