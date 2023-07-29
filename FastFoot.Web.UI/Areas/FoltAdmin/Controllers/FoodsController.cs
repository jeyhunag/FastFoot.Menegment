using FastFood.DAL.Data;
using FastFood.DAL.DbModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Signing;

namespace FastFoot.Web.UI.Areas.Controllers
{
    [Area("FoltAdmin")]
    public class FoodsController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _imgPath = @"img/";

        public FoodsController(AppDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            this._db = db;
            _webHostEnvironment = webHostEnvironment;
        }
        // GET: CitiesController
        public async Task<IActionResult> Index()
        {
            var food = _db.foods.Include(p => p.Categories).
                Include(p => p.Restaurants);
            return View(await food.ToListAsync());
        }



        // GET: CitiesController/Create
        public ActionResult Create()
        {
            ViewData["CategoriesId"] = new SelectList(_db.categories, "Id", "Name");
            ViewData["RestaurantsId"] = new SelectList(_db.restaurants, "Id", "Name");
            return View();
        }

        // POST: CitiesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Foods foods, IFormFile imageFile)
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                var imagePath = _imgPath + imageFile.FileName;
                var fullPath = Path.Combine(_webHostEnvironment.WebRootPath, imagePath);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                    foods.Image = imagePath;
                }
            }
            //if (ModelState.IsValid)
            //{

            await _db.foods.AddAsync(foods);
                await _db.SaveChangesAsync();

                return RedirectToAction("Index");
            //}
            ViewData["CategoriesId"] = new SelectList(_db.categories, "Id", "Name", foods.CategoriesId);
            ViewData["RestaurantsId"] = new SelectList(_db.restaurants, "Id", "Name", foods.RestaurantsId);
            return View(foods);
        }


        // GET: CitiesController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {

            var foods = await _db.foods.FindAsync(id);
            ViewData["CategoriesId"] = new SelectList(_db.categories, "Id", "Name");
            ViewData["RestaurantsId"] = new SelectList(_db.restaurants, "Id", "Name");
            return View(foods);
        }

        // POST: CitiesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Foods foods, IFormFile imageFile)
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                var imagePath = _imgPath + imageFile.FileName;
                var fullPath = Path.Combine(_webHostEnvironment.WebRootPath, imagePath);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                    foods.Image = imagePath;
                }
            }


            //if (ModelState.IsValid)
            //{
                _db.Update(foods);

                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            //}

            ViewData["CategoriesId"] = new SelectList(_db.categories, "Id", "Name", foods.CategoriesId);
            ViewData["RestaurantsId"] = new SelectList(_db.restaurants, "Id", "Name", foods.RestaurantsId);

            return View(foods);
        }

        // GET: CitiesController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null || _db.foods == null)
            {
                return NotFound();
            }

            var foods = await _db.foods
                .Include(p => p.Categories)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (foods == null)
            {
                return NotFound();
            }

            return View(foods);
        }

        // POST: CitiesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int Id, Foods foods)
        {

            var food = await _db.foods.FindAsync(Id);
            if (foods != null)
            {
                foods.DeletedDate = DateTime.Now;
                _db.foods.Remove(foods);
            }

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
    }
}
