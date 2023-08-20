using FastFood.DAL.Data;
using FastFood.DAL.DbModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FastFoot.Web.UI.Areas.FoltAdmin.Controllers
{
    [Area("FoltAdmin")]
    public class ServicesController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _imgPath = @"img/";
        public ServicesController(AppDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            this._db = db;
            this._webHostEnvironment = webHostEnvironment;
        }
        // GET: ServicesController
        public async Task<IActionResult> Index()
        {
            return View(await _db.Services.ToListAsync());
        }



        // GET: ServicesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ServicesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Services services, IFormFile imageFile)
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                var imagePath = _imgPath + imageFile.FileName;
                var fullPath = Path.Combine(_webHostEnvironment.WebRootPath, imagePath);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                    services.Icon = imagePath;
                }
            }

            if (!ModelState.IsValid)
            {
                await _db.Services.AddAsync(services);
                await _db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(services);
        }


        // GET: ServicesController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {

            var service = await _db.Services.FindAsync(id);

            return View(service);
        }

        // POST: ServicesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Services services, IFormFile imageFile)
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                var imagePath = _imgPath + imageFile.FileName;
                var fullPath = Path.Combine(_webHostEnvironment.WebRootPath, imagePath);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                    services.Icon = imagePath;
                }
            }

            //if (ModelState.IsValid)
            //{
            _db.Update(services);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
            //}
            return View(services);
        }

        // GET: ServicesController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var service = await _db.Services.FindAsync(id);
            return View(service);
        }

        // POST: ServicesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int Id, Services services)
        {


            await _db.Services.FindAsync(Id);
            if (services != null)
            {
                services.DeletedDate = DateTime.Now;
                _db.Services.Remove(services);
            }
            _db.SaveChanges();
            return RedirectToAction("index");

        }
    }
}
