using FastFood.DAL.DbModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FastFoot.Web.UI.ViewComponents
{
    public class FoodsMenuViewComponent : ViewComponent
    {
        private readonly AppDbContext db;
        public FoodsMenuViewComponent(AppDbContext db)
        {
            this.db = db;
                
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model=db.foods.Include(p=>p.Categories).Include(p => p.Restaurants).ToList();

            return View(model);
        }
    }
}
