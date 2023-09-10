using FastFood.DAL.DbModel;
using FastFoot.Web.UI.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FastFoot.Web.UI.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly AppDbContext db;
        public HeaderViewComponent(AppDbContext db)
        {
            this.db = db;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var vm=new HeaderViewModel();
            vm.Site = db.SiteInfos.FirstOrDefault();
            vm.socialLinks = db.socialLinks.ToList();
            return View(vm);
        }
    }
}
