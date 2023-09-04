using FastFood.DAL.Data;
using FastFood.DAL.DbModel;
using FastFoot.Web.UI.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FastFoot.Web.UI.ViewComponents
{
    public class UserRestaurantsViewComponent:ViewComponent
    {
        private readonly AppDbContext _db;

        public UserRestaurantsViewComponent(AppDbContext _db)
        {
              this._db = _db;
        }
        public async Task<IViewComponentResult> InvokeAsync(string id)
        {
            var vm = new UserRestaurantsViewModel();
            var user=await _db.Users.FirstOrDefaultAsync(x => x.Id == id);
            var rest = await _db.restaurants.FirstOrDefaultAsync(x => x.UserId == id);
            var foods = await _db.foods.Where(p => p.RestaurantsId == rest.Id).ToListAsync();
            vm.AppUser = user;
            vm.Restaurants = rest;
            vm.Foods = foods;
            vm.Profile=new ProfileViewModel();
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
