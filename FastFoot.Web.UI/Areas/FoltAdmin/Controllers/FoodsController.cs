using FastFood.DAL.Data;
using FastFood.DAL.DbModel;
using FastFoot.Web.UI.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Signing;
using System.Drawing;

namespace FastFoot.Web.UI.Areas.Controllers
{
    [Area("FoltAdmin")]
    public class FoodsController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment env;

        public FoodsController(AppDbContext db, IWebHostEnvironment webHostEnvironment, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            this._db = db;
            _webHostEnvironment = webHostEnvironment;
            this.env = env; 
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
        [Obsolete]
        public async Task<IActionResult> Create(Foods foods)
        {
            foods.ProductImages = new List<ProductImages>();
            foreach (var item in foods.Files)
            {

                foods.ProductImages.Add(new ProductImages
                {
                    IsMain = item.IsMain,
                    ImagePath = ImageHelper.Add(item.File, env)

                });
            }

            foreach (var item in foods.ProductImages)
            {
                if (item.IsMain == true)
                {
                    foods.Image = item.ImagePath;
                }
            }

            if (!ModelState.IsValid)
            {

                await _db.foods.AddAsync(foods);
                await _db.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            ViewData["CategoriesId"] = new SelectList(_db.categories, "Id", "Name", foods.CategoriesId);
            ViewData["RestaurantsId"] = new SelectList(_db.restaurants, "Id", "Name", foods.RestaurantsId);
            return View(foods);
        }


        // GET: CitiesController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {

            var foods = await _db.foods.Include(p=>p.ProductImages).FirstOrDefaultAsync(p=>p.Id==id);
            ViewData["CategoriesId"] = new SelectList(_db.categories, "Id", "Name");
            ViewData["RestaurantsId"] = new SelectList(_db.restaurants, "Id", "Name");
            return View(foods);
        }

        // POST: CitiesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Obsolete]
        public async Task<IActionResult> Edit(Foods foods)
        {
            foods.ProductImages = new List<ProductImages>();

            var images = _db.ProductImages.Where(pi => pi.FoodId == foods.Id).ToList();
            foreach (var item in images)
            {
                if (foods.Files.Any(f => f.File == null && string.IsNullOrWhiteSpace(f.TempPath) && f.Id == item.Id))
                {
                    _db.ProductImages.Remove(item);
                    ImageHelper.Delete(item.ImagePath, env);

                }
                else if (foods.Files.Any(f => f.Id == item.Id && f.IsMain))
                {
                    item.IsMain = true;

                }

                else
                {
                    item.IsMain = false;
                }

            }



            foreach (var item in foods.Files.Where(f => f.File != null))
            {
                foods.ProductImages.Add(new ProductImages
                {
                    IsMain = item.IsMain,
                    ImagePath = ImageHelper.Add(item.File, env)
                });
            }

            foreach (var image in foods.ProductImages)
            {
                if (image.IsMain == true)
                {
                    foods.Image = image.ImagePath;
                }
            }

            if (foods.ProductImages.Count == 0)
            {
                foreach (var item in foods.Files)
                {
                    if (item.IsMain == true)
                    {
                        foods.Image = item.TempPath.ToString();
                    }
                }
            }

            _db.Update(foods);

                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));

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
                .Include(p => p.Restaurants)
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
