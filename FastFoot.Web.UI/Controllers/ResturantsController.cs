using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FastFoot.Web.UI.Controllers
{
    public class ResturantsController : Controller
    {
        // GET: ResturantsController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ResturantsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ResturantsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ResturantsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ResturantsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ResturantsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ResturantsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
