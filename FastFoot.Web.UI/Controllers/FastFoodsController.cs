using Microsoft.AspNetCore.Mvc;

namespace FastFoot.Web.UI.Controllers
{
    public class FastFoodsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
