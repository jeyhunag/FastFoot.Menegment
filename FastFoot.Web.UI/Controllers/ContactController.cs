using Microsoft.AspNetCore.Mvc;

namespace FastFoot.Web.UI.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
