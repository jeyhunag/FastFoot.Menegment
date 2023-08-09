using FastFood.DAL.DbModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FastFoot.Web.UI.Controllers
{
    public class DetailsController : Controller
    {
        private readonly AppDbContext db;
        public DetailsController(AppDbContext db)
        {
            this.db = db;
        }
        public IActionResult Index(int id)
        {
            var model=db.foods.Include(p=>p.Categories)
                .Include(p => p.Restaurants)
                .Include(p => p.ProductImages)
                .Include(p => p.Campaigns)
                .FirstOrDefault(p=>p.Id==id);
            return View(model);
        }
    }
}
