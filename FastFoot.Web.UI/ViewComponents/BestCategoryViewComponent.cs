using FastFood.DAL.DbModel;
using Microsoft.AspNetCore.Mvc;

namespace FastFoot.Web.UI.ViewComponents
{
    public class BestCategoryViewComponent : ViewComponent
    {
        private readonly AppDbContext db;
        public BestCategoryViewComponent(AppDbContext db)
        {
                this.db = db;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = db.categories.ToList();
            return View(model);
        }
    }
}
