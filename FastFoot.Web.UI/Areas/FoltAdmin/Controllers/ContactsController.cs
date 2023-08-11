using FastFood.DAL.Data;
using FastFood.DAL.DbModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FastFoot.Web.UI.Areas.FoltAdmin.Controllers
{
    [Area("FoltAdmin")]
    public class ContactsController : Controller
    {
        private readonly AppDbContext _db;

        public ContactsController(AppDbContext db)
        {
            this._db = db;
        }
        // GET: CitiesController
        public async Task<IActionResult> Index()
        {
            return View(await _db.Contacts.ToListAsync());
        }


        // GET: CitiesController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var contact = await _db.Contacts.FindAsync(id);
            return View(contact);
        }

        // POST: CitiesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int Id, Contact contact)
        {


            await _db.Contacts.FindAsync(Id);
            if (contact != null)
            {
                contact.DeletedDate = DateTime.Now;
                _db.Contacts.Remove(contact);
            }
            _db.SaveChanges();
            return RedirectToAction("index");

        }
    }
}
