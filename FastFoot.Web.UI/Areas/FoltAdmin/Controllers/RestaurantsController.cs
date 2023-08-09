using FastFood.DAL.Data;
using FastFood.DAL.DbModel;
using FastFoot.Web.UI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Signing;
using System.Reflection;

namespace FastFoot.Web.UI.Areas.Controllers
{
    [Area("FoltAdmin")]
    public class RestaurantsController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment env;

        public RestaurantsController(AppDbContext db, IWebHostEnvironment webHostEnvironment, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            this._db = db;
            _webHostEnvironment = webHostEnvironment;
            this.env = env;
        }
        // GET: CitiesController
        public async Task<IActionResult> Index()
        {
            var rest = _db.restaurants.Include(p => p.Cities);
            return View(await rest.ToListAsync());
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
        [Obsolete]
        public async Task<IActionResult> Create(Restaurants restaurants)
        {
            restaurants.RestaurantsImages = new List<RestaurantsImage>();
            foreach (var item in restaurants.Files)
            {

                restaurants.RestaurantsImages.Add(new RestaurantsImage
                {
                    IsMain = item.IsMain,
                    ImagePath = ImageHelper.Add(item.File, env)
                });
            }

            foreach (var item in restaurants.RestaurantsImages)
            {
                if (item.IsMain == true)
                {
                    restaurants.Image = item.ImagePath;
                }
            }

            if (!ModelState.IsValid)
            {

                await _db.restaurants.AddAsync(restaurants);
                await _db.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            ViewData["CitiesId"] = new SelectList(_db.cities, "Id", "Name", restaurants.CitiesId);
            return View(restaurants);
        }


        // GET: CitiesController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {

            var rest = await _db.restaurants.FindAsync(id);
            ViewData["CitiesId"] = new SelectList(_db.cities, "Id", "Name");
            return View(rest);
        }

        // POST: CitiesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Restaurants restaurants, IFormFile imageFile)
        {



          
            //if (imageFile != null && imageFile.Length > 0)
            //{
            //    var imagePath = _imgPath + imageFile.FileName;
            //    var fullPath = Path.Combine(_webHostEnvironment.WebRootPath, imagePath);
            //    using (var stream = new FileStream(fullPath, FileMode.Create))
            //    {
            //        await imageFile.CopyToAsync(stream);
            //        restaurants.Image = imagePath;
            //    }
            //}
            _db.Update(restaurants);

            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
         

            ViewData["CitiesId"] = new SelectList(_db.cities, "Id", "Name", restaurants.CitiesId);
            return View(restaurants);
        }

        // GET: CitiesController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var rest = await _db.restaurants
               .Include(p => p.Cities)
               .FirstOrDefaultAsync(m => m.Id == id);
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
