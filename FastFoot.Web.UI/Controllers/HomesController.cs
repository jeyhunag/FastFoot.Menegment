using Microsoft.AspNetCore.Mvc;

namespace FastFoot.Web.UI.Controllers
{
    public class HomesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
