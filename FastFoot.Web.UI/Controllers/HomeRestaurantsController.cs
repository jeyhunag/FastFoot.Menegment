using Microsoft.AspNetCore.Mvc;

namespace FastFoot.Web.UI.Controllers
{
    public class HomeRestaurantsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
