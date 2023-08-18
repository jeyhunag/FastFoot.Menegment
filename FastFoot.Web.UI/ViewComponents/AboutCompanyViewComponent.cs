using FastFood.DAL.DbModel;
using Microsoft.AspNetCore.Mvc;

namespace FastFoot.Web.UI.ViewComponents
{
    public class AboutCompanyViewComponent : ViewComponent
    {
        private readonly AppDbContext db;
        public AboutCompanyViewComponent(AppDbContext db)
        {
            this.db = db;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = db.restaurants.ToList();
            return View(model);
        }
    }
}
