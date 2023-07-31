using FastFood.DAL.Data;
using FastFood.DAL.DbModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FastFoot.Web.UI.Areas.Controllers
{
    [Area("FoltAdmin")]
    public class CategoriesController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _imgPath = @"img/";
        public CategoriesController(AppDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            this._db = db;
            this._webHostEnvironment = webHostEnvironment;
        }
        // GET: CitiesController
        public async Task<IActionResult> Index()
        {
            return View(await _db.categories.ToListAsync());
        }



        // GET: CitiesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CitiesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Categories categories, IFormFile imageFile)
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                var imagePath = _imgPath + imageFile.FileName;
                var fullPath = Path.Combine(_webHostEnvironment.WebRootPath, imagePath);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                    categories.ImagePath = imagePath;
                }
            }

            if (!ModelState.IsValid)
            {
                await _db.categories.AddAsync(categories);
                await _db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(categories);
        }


        // GET: CitiesController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {

            var categories = await _db.categories.FindAsync(id);

            return View(categories);
        }

        // POST: CitiesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Categories categories, IFormFile imageFile)
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                var imagePath = _imgPath + imageFile.FileName;
                var fullPath = Path.Combine(_webHostEnvironment.WebRootPath, imagePath);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                    categories.ImagePath = imagePath;
                }
            }

            //if (ModelState.IsValid)
            //{
                _db.Update(categories);
                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            //}
            return View(categories);
        }

        // GET: CitiesController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var categories = await _db.categories.FindAsync(id);
            return View(categories);
        }

        // POST: CitiesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int Id, Categories categories)
        {


            await _db.categories.FindAsync(Id);
            if (categories != null)
            {
                categories.DeletedDate = DateTime.Now;
                _db.categories.Remove(categories);
            }
            _db.SaveChanges();
            return RedirectToAction("index");

        }   
    }
}
