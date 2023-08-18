using FastFood.DAL.DbModel;
using Microsoft.AspNetCore.Mvc;

namespace FastFoot.Web.UI.ViewComponents
{
    public class PopularMenuViewComponent :ViewComponent
    {
        private readonly AppDbContext db;
        public PopularMenuViewComponent(AppDbContext db)
        {
            this.db = db;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = db.Campaigns.ToList();
            return View(model);
        }
    }
}
