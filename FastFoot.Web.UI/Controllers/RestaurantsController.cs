using FastFood.DAL.Data;
using FastFood.DAL.DbModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FastFoot.Web.UI.Controllers
{
    public class RestaurantsController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _imgPath = @"img/";

        public RestaurantsController(AppDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            this._db = db;
            _webHostEnvironment = webHostEnvironment;
        }
        // GET: CitiesController
        public async Task<IActionResult> Index()
        {
            return View(await _db.restaurants.ToListAsync());
        }



        // GET: CitiesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CitiesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Restaurants restaurants, IFormFile imageFile)
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                var imagePath = _imgPath + imageFile.FileName;
                var fullPath = Path.Combine(_webHostEnvironment.WebRootPath, imagePath);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                    restaurants.Image = imagePath;
                }
            }
            //if (ModelState.IsValid)
            //{

            await _db.restaurants.AddAsync(restaurants);
            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
            //}

            return View(restaurants);
        }


        // GET: CitiesController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {

            var rest = await _db.restaurants.FindAsync(id);
            return View(rest);
        }

        // POST: CitiesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Restaurants restaurants, IFormFile imageFile)
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                var imagePath = _imgPath + imageFile.FileName;
                var fullPath = Path.Combine(_webHostEnvironment.WebRootPath, imagePath);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                    restaurants.Image = imagePath;
                }
            }


            //if (ModelState.IsValid)
            //{
            _db.Update(restaurants);

            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
            //}

            return View(restaurants);
        }

        // GET: CitiesController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var rest = await _db.restaurants.FindAsync(id);
            return View(rest);
        }

        // POST: CitiesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int Id, Restaurants restaurants)
        {

            var rest = await _db.restaurants.FindAsync(Id);
            if (restaurants != null)
            {
                restaurants.DeletedDate = DateTime.Now;
                _db.restaurants.Remove(restaurants);
            }

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
    }
}
