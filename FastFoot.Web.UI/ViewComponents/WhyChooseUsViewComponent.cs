using FastFood.DAL.DbModel;
using Microsoft.AspNetCore.Mvc;

namespace FastFoot.Web.UI.ViewComponents
{
    public class WhyChooseUsViewComponent : ViewComponent
    {
        private readonly AppDbContext db;
        public WhyChooseUsViewComponent(AppDbContext db)
        {
            this.db = db;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = db.Services.ToList();
            return View(model);
        }
    }
}
