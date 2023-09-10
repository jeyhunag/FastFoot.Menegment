using FastFood.DAL.Data;
using FastFood.DAL.DbModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FastFoot.Web.UI.Areas.FoltAdmin.Controllers
{
    [Area("FoltAdmin")]
    public class SocialLinksController : Controller
    {
        private readonly AppDbContext _db;
        public SocialLinksController(AppDbContext db)
        {
            this._db = db;
        }
        // GET: Social Links Controller
        public async Task<IActionResult> Index()
        {
            return View(await _db.socialLinks.ToListAsync());
        }



        // GET: Social Links Controller/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Social Links Controller/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SocialLink social)
        {


            if (ModelState.IsValid)
            {
                await _db.socialLinks.AddAsync(social);
                await _db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(social);
        }


        // GET: Social Links Controller/Edit/5
        public async Task<IActionResult> Edit(int id)
        {

            var social = await _db.socialLinks.FindAsync(id);

            return View(social);
        }

        // POST: Social Links Controller/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SocialLink social)
        {

            //if (ModelState.IsValid)
            //{
            _db.Update(social);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
            //}
            return View(social);
        }

        // GET: Social Links Controller/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var social = await _db.socialLinks.FindAsync(id);
            return View(social);
        }

        // POST: Social Links Controller/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int Id, SocialLink social)
        {


            await _db.socialLinks.FindAsync(Id);
            if (social != null)
            {
                social.DeletedDate = DateTime.Now;
                _db.socialLinks.Remove(social);
            }
            _db.SaveChanges();
            return RedirectToAction("index");

        }
    }
}
