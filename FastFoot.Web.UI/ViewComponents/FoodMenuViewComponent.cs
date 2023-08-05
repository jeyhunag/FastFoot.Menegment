using Microsoft.AspNetCore.Mvc;

namespace FastFoot.Web.UI.ViewComponents
{
    public class FoodMenuViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
