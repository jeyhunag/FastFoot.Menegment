using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FastFoot.Web.UI.ViewComponents
{
    public class FooterViewComponent :ViewComponent 
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
