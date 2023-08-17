using Microsoft.AspNetCore.Mvc;

namespace FastFoot.Web.UI.Controllers
{
    public class ShopsController : Controller
    {

        public IActionResult Index(int category)
        {
            ViewBag.Category = category;
            return View();
        }
    }
}
