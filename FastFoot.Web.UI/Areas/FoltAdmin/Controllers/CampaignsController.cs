using FastFood.DAL.Data;
using FastFood.DAL.DbModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FastFoot.Web.UI.Areas.FoltAdmin.Controllers
{
    [Area("FoltAdmin")]
    public class CampaignsController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _imgPath = @"img/";

        public CampaignsController(AppDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            this._db = db;
            _webHostEnvironment = webHostEnvironment;
        }
        // GET: CampaignsController
        public async Task<IActionResult> Index()
        {
            var cam = _db.Campaigns.Include(p => p.Foods).
                Include(p => p.Restaurants);
            return View(await cam.ToListAsync());
        }



        // GET: CampaignsController/Create
        public ActionResult Create()
        {
            ViewData["FoodsId"] = new SelectList(_db.foods, "Id", "Name");
            ViewData["RestaurantsId"] = new SelectList(_db.restaurants, "Id", "Name");
            return View();
        }

        // POST: CampaignsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Campaign campaign, IFormFile imageFile)
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                var imagePath = _imgPath + imageFile.FileName;
                var fullPath = Path.Combine(_webHostEnvironment.WebRootPath, imagePath);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                    campaign.ImagePath = imagePath;
                }
            }
            if (!ModelState.IsValid)
            {

                await _db.Campaigns.AddAsync(campaign);
                await _db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            ViewData["CategoriesId"] = new SelectList(_db.foods, "Id", "Name", campaign.FoodsId);
            ViewData["RestaurantsId"] = new SelectList(_db.restaurants, "Id", "Name", campaign.RestaurantsId);
            return View(campaign);
        }


        // GET: CampaignsController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {

            var com = await _db.Campaigns.FindAsync(id);
            ViewData["FoodsId"] = new SelectList(_db.foods, "Id", "Name");
            ViewData["RestaurantsId"] = new SelectList(_db.restaurants, "Id", "Name");
            return View(com);
        }

        // POST: CampaignsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Campaign campaign, IFormFile imageFile)
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                var imagePath = _imgPath + imageFile.FileName;
                var fullPath = Path.Combine(_webHostEnvironment.WebRootPath, imagePath);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                    campaign.ImagePath = imagePath;
                }
            }


            //if (ModelState.IsValid)
            //{
            //    _db.Update(campaign);

                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            //}

            ViewData["CategoriesId"] = new SelectList(_db.foods, "Id", "Name", campaign.FoodsId);
            ViewData["RestaurantsId"] = new SelectList(_db.restaurants, "Id", "Name", campaign.RestaurantsId);

            return View(campaign);
        }

        // GET: CampaignsController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null || _db.Campaigns == null)
            {
                return NotFound();
            }

            var cam = await _db.Campaigns
                .Include(p => p.Foods)
                .Include(p => p.Restaurants)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cam == null)
            {
                return NotFound();
            }

            return View(cam);
        }

        // POST: CampaignsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int Id, Campaign campaign)
        {

            var cam = await _db.Campaigns.FindAsync(Id);
            if (cam != null)
            {
                campaign.DeletedDate = DateTime.Now;
                _db.Campaigns.Remove(campaign);
            }

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
    }
}
