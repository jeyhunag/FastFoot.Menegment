using FastFood.DAL.DbModel;
using FastFoot.Web.UI.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace FastFoot.Web.UI.ViewComponents
{
    public class SpecialFoodViewComponent : ViewComponent
    {
        private readonly AppDbContext db;
        public SpecialFoodViewComponent(AppDbContext db)
        {
            this.db = db;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var vm = new CampaignBannerViewModel();
            vm.Banners = db.Banners.ToList();
            vm.Campaigns = db.Campaigns.ToList();

            return View(vm);
        }
    }
}
