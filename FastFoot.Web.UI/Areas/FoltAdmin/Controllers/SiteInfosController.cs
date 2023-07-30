using FastFood.DAL.Data;
using FastFood.DAL.DbModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FastFoot.Web.UI.Areas.FoltAdmin.Controllers
{
    [Area("FoltAdmin")]
    public class SiteInfosController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _imgPath = @"img/";

        public SiteInfosController(AppDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            this._db = db;
            _webHostEnvironment = webHostEnvironment;
        }
        // GET: CitiesController
        public async Task<IActionResult> Index()
        {
            var site = _db.SiteInfos.Include(p => p.Cities);
            return View(await site.ToListAsync());
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
        public async Task<IActionResult> Create(SiteInfo siteInfo, IFormFile imageFile, IFormFile iconFile)
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                var imagePath = _imgPath + imageFile.FileName;
                var fullPath = Path.Combine(_webHostEnvironment.WebRootPath, imagePath);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                    siteInfo.Logo = imagePath;
                }
            }

            if (iconFile != null && iconFile.Length > 0)
            {
                var imagePath = _imgPath + iconFile.FileName;
                var fullPath = Path.Combine(_webHostEnvironment.WebRootPath, imagePath);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await iconFile.CopyToAsync(stream);
                    siteInfo.FavIcon = imagePath;
                }
            }
            //if (ModelState.IsValid)
            //{

            await _db.SiteInfos.AddAsync(siteInfo);
            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
            //}
            ViewData["CitiesId"] = new SelectList(_db.cities, "Id", "Name", siteInfo.CitiesId);
            return View(siteInfo);
        }


        // GET: CitiesController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {

            var site = await _db.SiteInfos.FindAsync(id);
            ViewData["CitiesId"] = new SelectList(_db.cities, "Id", "Name");
            return View(site);
        }

        // POST: CitiesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SiteInfo siteInfo, IFormFile imageFile,IFormFile iconFile)
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                var imagePath = _imgPath + imageFile.FileName;
                var fullPath = Path.Combine(_webHostEnvironment.WebRootPath, imagePath);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                    siteInfo.Logo = imagePath;
                }
            }

            if (iconFile != null && iconFile.Length > 0)
            {
                var imagePath = _imgPath + iconFile.FileName;
                var fullPath = Path.Combine(_webHostEnvironment.WebRootPath, imagePath);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await iconFile.CopyToAsync(stream);
                    siteInfo.FavIcon = imagePath;
                }
            }

            //if (ModelState.IsValid)
            //{
            _db.Update(siteInfo);

            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
            //}

            ViewData["CitiesId"] = new SelectList(_db.cities, "Id", "Name", siteInfo.CitiesId);
            return View(siteInfo);
        }

        // GET: CitiesController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null || _db.SiteInfos == null)
            {
                return NotFound();
            }

            var site = await _db.SiteInfos
                .Include(p => p.Cities)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (site == null)
            {
                return NotFound();
            }

            return View(site);
        }

        // POST: CitiesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int Id, SiteInfo siteInfo)
        {

            var site = await _db.SiteInfos.FindAsync(Id);
            if (site != null)
            {
                siteInfo.DeletedDate = DateTime.Now;
                _db.SiteInfos.Remove(siteInfo);
            }

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
    }
}
