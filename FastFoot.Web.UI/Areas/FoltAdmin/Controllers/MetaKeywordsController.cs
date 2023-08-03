using FastFood.DAL.Data;
using FastFood.DAL.DbModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FastFoot.Web.UI.Areas.FoltAdmin.Controllers
{
    [Area("FoltAdmin")]
    public class MetaKeywordsController : Controller
    {
        private readonly AppDbContext _db;
        public MetaKeywordsController(AppDbContext db)
        {
            this._db = db;
        }

        // GET: MetaKeywordsController
        public async Task<IActionResult> Index()
        {
            var meta = _db.MetaKeywords.Include(p => p.Cities);
            return View(await meta.ToListAsync());
        }



        // GET: MetaKeywordsController/Create
        public ActionResult Create()
        {
            ViewData["CitiesId"] = new SelectList(_db.cities, "Id", "Name");
            return View();
        }

        // POST: MetaKeywordsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MetaKeyword metaKeyword)
        {

            if (ModelState.IsValid)
            {

                await _db.MetaKeywords.AddAsync(metaKeyword);
                await _db.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            ViewData["CitiesId"] = new SelectList(_db.cities, "Id", "Name", metaKeyword.CitiesId);
            return View(metaKeyword);
        }


        // GET: MetaKeywordsController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {

            var meta = await _db.MetaKeywords.FindAsync(id);
            ViewData["CitiesId"] = new SelectList(_db.cities, "Id", "Name");
            return View(meta);
        }

        // POST: MetaKeywordsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(MetaKeyword metaKeyword)
        {


            _db.Update(metaKeyword);

            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

            ViewData["CitiesId"] = new SelectList(_db.cities, "Id", "Name", metaKeyword.CitiesId);

            return View(metaKeyword);
        }

        // GET: MetaKeywordsController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null || _db.MetaKeywords == null)
            {
                return NotFound();
            }

            var meta = await _db.MetaKeywords
                .Include(p => p.Cities)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (meta == null)
            {
                return NotFound();
            }

            return View(meta);
        }

        // POST: MetaKeywordsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int Id, MetaKeyword metaKeyword)
        {

            var meta = await _db.MetaKeywords.FindAsync(Id);
            if (metaKeyword != null)
            {
                meta.DeletedDate = DateTime.Now;
                _db.MetaKeywords.Remove(meta);
            }

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
    }
}
