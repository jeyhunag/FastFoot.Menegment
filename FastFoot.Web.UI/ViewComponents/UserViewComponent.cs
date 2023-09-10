using FastFood.DAL.DbModel;
using FastFoot.Web.UI.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FastFoot.Web.UI.ViewComponents
{
    public class UserViewComponent:ViewComponent
    {
        private readonly AppDbContext _db;

        public UserViewComponent(AppDbContext _db)
        {
            this._db = _db;
        }
        public async Task<IViewComponentResult> InvokeAsync(string id)
        {
            var vm = new ProfileViewModel();
            var user = await _db.Users.FirstOrDefaultAsync(x => x.Id == id);
            vm.AppUser = user;
            vm.WhatsappNumber = user.WhatsappNumber;
            vm.Fincode = user.Fincode;
            vm.Name = user.Name;
            vm.Surname = user.Surname;
            vm.Name = user.Name;
            vm.UserName = user.UserName;
            vm.Email = user.Email;
            vm.Img = user.Img;
            vm.DateOfBirth = user.DateOfBirth;
            vm.Gender = user.Gender;
            vm.Country = user.Country;
            return View(vm);
        }
    }
}
