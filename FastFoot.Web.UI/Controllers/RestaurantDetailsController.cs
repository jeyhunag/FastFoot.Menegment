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
        public IActionResult Index( int Id)
        {

            var model = db.foods
               .Include(p => p.Restaurants).ToList();

            if (Id > 0)
            {
                model = model.Where(p => p.RestaurantsId == Id).ToList();
            }
            return View(model);
        }
    }
}
