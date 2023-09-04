using FastFood.DAL.Data;
using FastFood.DAL.DbModel;
using FastFoot.Web.UI.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FastFoot.Web.UI.ViewComponents
{
     public class UserCourierViewComponent:ViewComponent
    {
        private readonly AppDbContext _db;

        public UserCourierViewComponent(AppDbContext _db)
        {
            this._db = _db;
        }
        public async Task<IViewComponentResult> InvokeAsync(string id)
        {
            var vm = new UserCourierViewModel();
            var user = await _db.Users.FirstOrDefaultAsync(x => x.Id == id);
            vm.AppUser = user;
            vm.Profile = new ProfileViewModel();
            vm.Profile.WhatsappNumber = user.WhatsappNumber;
            vm.Profile.Fincode = user.Fincode;
            vm.Profile.Name = user.Name;
            vm.Profile.Surname = user.Surname;
            vm.Profile.Name = user.Name;
            vm.Profile.Email = user.Email;
            vm.Profile.Img = user.Img;
            vm.Profile.DateOfBirth = user.DateOfBirth;
            vm.Profile.Gender = user.Gender;
            vm.Profile.Country = user.Country;
            return View(vm);
        }
    }
}
