using FastFood.DAL.DbModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FastFoot.Web.UI.Controllers
{
    public class RestaurantDetailsController : Controller
    {
        private readonly AppDbContext db;
        public RestaurantDetailsController(AppDbContext db)
        {
            this.db = db;
        }
        public IActionResult Index( int id)
        {

            var model = db.restaurants
               .Include(p => p.foods).Include(p=>p.RestaurantsImages).Include(p => p.Cities).FirstOrDefault(p => p.Id == id);

            return View(model);
        }
    }
}
