using FastFood.DAL.Data;
using FastFood.DAL.DbModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FastFoot.Web.UI.Areas.Controllers
{
    [Area("FoltAdmin")]
    public class CitiesController : Controller
    {
        private readonly AppDbContext _db;

        public CitiesController(AppDbContext db)
        {
            this._db = db;
        }
        // GET: CitiesController
        public async Task<IActionResult> Index()
        {
            return View(await _db.cities.ToListAsync());
        }



        // GET: CitiesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CitiesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Cities cities)
        {

            if (ModelState.IsValid)
            {

                _db.cities.Add(cities);
                await _db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(cities);
        }


        // GET: CitiesController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {

            var cities = await _db.cities.FindAsync(id);

            return View(cities);
        }

        // POST: CitiesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Cities cities)
        {

            if (ModelState.IsValid)
            {
                    _db.Update(cities);
                    await _db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(cities);
        }

        // GET: CitiesController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var cities =await _db.cities.FindAsync(id);
            return View(cities);
        }

        // POST: CitiesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int Id,Cities cities)
        {
            
      
                await _db.cities.FindAsync(Id);
                if (cities != null)
                {
                    cities.DeletedDate = DateTime.Now;
                    _db.cities.Remove(cities);
                }
                _db.SaveChanges();
                return RedirectToAction("index");

        }
    }
}
