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
        public async Task<IViewComponentResult> InvokeAsync(int category)
        {

            var model=db.foods.Include(p=>p.Categories).Include(p => p.Restaurants).ToList();
            if (category > 0 )
            {
                model = model.Where(p => p.CategoriesId == category).ToList();
            }
            return View(model);
        }
    }
}
