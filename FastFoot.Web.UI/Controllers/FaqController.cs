using Microsoft.AspNetCore.Mvc;

namespace FastFoot.Web.UI.Controllers
{
    public class FaqController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
