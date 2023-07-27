using FastFood.DAL.Data;
using FastFood.DAL.DbModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FastFoot.Web.UI.Areas.Controllers  
{
    [Area("FoltAdmin")]
    public class CouriersController : Controller
    {
        private readonly AppDbContext _db;
        public CouriersController(AppDbContext db)
        {
            this._db = db;
        }

        // GET: CitiesController
        public async Task<IActionResult> Index()
        {
            var courries = _db.couriers.Include(p => p.Cities);
            return View(await courries.ToListAsync());
        }



        // GET: CitiesController/Create
        public ActionResult Create()
        {
            ViewData["CitiesId"] = new SelectList(_db.cities, "Id", "Name");
            return View();
        }

        // POST: CitiesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Courier courier)
        {

            //if (ModelState.IsValid)
            //{

            await _db.couriers.AddAsync(courier);
            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
            //}
            ViewData["CitiesId"] = new SelectList(_db.cities, "Id", "Name", courier.CitiesId);
            return View(courier);
        }


        // GET: CitiesController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {

            var courier = await _db.couriers.FindAsync(id);
            ViewData["CitiesId"] = new SelectList(_db.categories, "Id", "Name");
            return View(courier);
        }

        // POST: CitiesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Courier courier)
        {

            //if (ModelState.IsValid)
            //{
            _db.Update(courier);

            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
            //}

            ViewData["CitiesId"] = new SelectList(_db.cities, "Id", "Name", courier.CitiesId);

            return View(courier);
        }

        // GET: CitiesController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null || _db.couriers == null)
            {
                return NotFound();
            }

            var courier = await _db.couriers
                .Include(p => p.Cities)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (courier == null)
            {
                return NotFound();
            }

            return View(courier);
        }

        // POST: CitiesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int Id, Courier courier)
        {

            var couriers = await _db.couriers.FindAsync(Id);
            if (courier != null)
            {
                courier.DeletedDate = DateTime.Now;
                _db.couriers.Remove(courier);
            }

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
    }
}
