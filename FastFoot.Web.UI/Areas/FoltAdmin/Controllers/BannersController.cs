using FastFood.DAL.Data;
using FastFood.DAL.DbModel;
using FastFoot.Web.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Signing;

namespace FastFoot.Web.UI.Areas.FoltAdmin.Controllers
{
    [Area("FoltAdmin")]
    public class BannersController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment env;

        public BannersController(AppDbContext db, IWebHostEnvironment webHostEnvironment, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            this._db = db;
            _webHostEnvironment = webHostEnvironment;
            this.env = env;
        }
        // GET: BannersController
        public async Task<IActionResult> Index()
        {
            return View(await _db.Banners.ToListAsync());
        }



        // GET: BannersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BannersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Obsolete]
        public async Task<IActionResult> Create(Banner banner)
        {
            banner.BannersImages = new List<BannersImage>();
            foreach (var item in banner.Files)
            {

                banner.BannersImages.Add(new BannersImage
                {
                    IsMain = item.IsMain,
                    ImagePath = ImageHelper.Add(item.File, env)

                });
            }

            foreach (var item in banner.BannersImages)
            {
                if (item.IsMain == true)
                {
                    banner.ImagePath = item.ImagePath;
                }
            }

            if (!ModelState.IsValid)
            {
                await _db.Banners.AddAsync(banner);
                await _db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(banner);
        }


        // GET: BannersController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {

            var banner = await _db.Banners.FindAsync(id);

            return View(banner);
        }

        // POST: BannersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit( Banner banner, IFormFile imageFile)
        {

            //if (imageFile != null && imageFile.Length > 0)
            //{
            //    var imagePath = _imgPath + imageFile.FileName;
            //    var fullPath = Path.Combine(_webHostEnvironment.WebRootPath, imagePath);
            //    using (var stream = new FileStream(fullPath, FileMode.Create))
            //    {
            //        await imageFile.CopyToAsync(stream);
            //        banner.ImagePath = imagePath;
            //    }
            //}

            //if (ModelState.IsValid)
            //{
                _db.Update(banner);
                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            //}

            return View(banner);
        }


        // GET: BannersController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var banner = await _db.Banners.FindAsync(id);
            return View(banner);
        }

        // POST: BannersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int Id, Banner banner)
        {


            await _db.Banners.FindAsync(Id);
            if (banner != null)
            {
                banner.DeletedDate = DateTime.Now;
                _db.Banners.Remove(banner);
            }
            _db.SaveChanges();
            return RedirectToAction("index");

        }
    }
}
