using FastFood.DAL.Data;
using FastFood.DAL.DbModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FastFoot.Web.UI.Areas.FoltAdmin.Controllers
{
    [Area("FoltAdmin")]
    public class MetaDescriptionsController : Controller
    {
        private readonly AppDbContext _db;
        public MetaDescriptionsController(AppDbContext db)
        {
            this._db = db;
        }

        // GET: MetaDescriptionsController
        public async Task<IActionResult> Index()
        {
            var meta = _db.MetaDescriptions.Include(p => p.Cities);
            return View(await meta.ToListAsync());
        }



        // GET: MetaDescriptionsController/Create
        public ActionResult Create()
        {
            ViewData["CitiesId"] = new SelectList(_db.cities, "Id", "Name");
            return View();
        }

        // POST: MetaDescriptionsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MetaDescription metaDescription)
        {

            if (ModelState.IsValid)
            {

                await _db.MetaDescriptions.AddAsync(metaDescription);
                await _db.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            ViewData["CitiesId"] = new SelectList(_db.cities, "Id", "Name", metaDescription.CitiesId);
            return View(metaDescription);
        }


        // GET: MetaDescriptionsController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {

            var meta = await _db.MetaDescriptions.FindAsync(id);
            ViewData["CitiesId"] = new SelectList(_db.cities, "Id", "Name");
            return View(meta);
        }

        // POST: MetaDescriptionsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(MetaDescription metaDescription)
        {

            //if (ModelState.IsValid)
            //{
            _db.Update(metaDescription);

            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
            //}

            ViewData["CitiesId"] = new SelectList(_db.cities, "Id", "Name", metaDescription.CitiesId);

            return View(metaDescription);
        }

        // GET: MetaDescriptionsController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null || _db.MetaDescriptions == null)
            {
                return NotFound();
            }

            var meta = await _db.MetaDescriptions
                .Include(p => p.Cities)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (meta == null)
            {
                return NotFound();
            }

            return View(meta);
        }

        // POST: MetaDescriptionsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int Id, MetaDescription metaDescription)
        {

            var meta = await _db.MetaDescriptions.FindAsync(Id);
            if (metaDescription != null)
            {
                metaDescription.DeletedDate = DateTime.Now;
                _db.MetaDescriptions.Remove(meta);
            }

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
    }
}
