using FastFood.DAL.Data;
using FastFood.DAL.DbModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Signing;
using static NuGet.Packaging.PackagingConstants;

namespace FastFoot.Web.UI.Areas.Controllers
{
    [Area("FoltAdmin")]
    public class OrdersController : Controller
    {
        private readonly AppDbContext _db;
        public OrdersController(AppDbContext db)
        {
            this._db = db;
        }

        // GET: CitiesController
        public async Task<IActionResult> Index()
        {
            var order = _db.orders.Include(p => p.Cities)
                .Include(p => p.Restaurants)
                .Include(p => p.Courier)
                .Include(p => p.Foods);
            return View(await order.ToListAsync());
        }



        // GET: CitiesController/Create
        public ActionResult Create()
        {
            ViewData["CitiesId"] = new SelectList(_db.cities, "Id", "Name");
            ViewData["RestaurantsId"] = new SelectList(_db.restaurants, "Id", "Name");
            ViewData["FoodsId"] = new SelectList(_db.foods, "Id", "Name");
            ViewData["CourierId"] = new SelectList(_db.couriers, "Id", "Name");

            return View();
        }

        // POST: CitiesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Orders orders)
        {

            //if (ModelState.IsValid)
            //{

                await _db.orders.AddAsync(orders);
                await _db.SaveChangesAsync();

                int newOrderId = orders.Id;
            return RedirectToAction("Index");
            //}
            ViewData["CitiesId"] = new SelectList(_db.cities, "Id", "Name", orders.CitiesId);
            ViewData["RestaurantsId"] = new SelectList(_db.restaurants, "Id", "Name", orders.RestaurantsId);
            ViewData["FoodsId"] = new SelectList(_db.foods, "Id", "Name", orders.FoodsId);
            ViewData["CourierId"] = new SelectList(_db.couriers, "Id", "Name", orders.CourierId);
            return View(orders);
        }


        // GET: CitiesController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {

            var order = await _db.orders.FindAsync(id);
            ViewData["CitiesId"] = new SelectList(_db.cities, "Id", "Name");
            ViewData["RestaurantsId"] = new SelectList(_db.restaurants, "Id", "Name");
            ViewData["FoodsId"] = new SelectList(_db.foods, "Id", "Name");
            ViewData["CourierId"] = new SelectList(_db.couriers, "Id", "Name");
            return View(order);
        }

        // POST: CitiesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Orders orders)
        {

            //if (ModelState.IsValid)
            //{
            _db.Update(orders);

            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
            //}

            ViewData["CitiesId"] = new SelectList(_db.cities, "Id", "Name", orders.CitiesId);
            ViewData["RestaurantsId"] = new SelectList(_db.restaurants, "Id", "Name", orders.RestaurantsId);
            ViewData["FoodsId"] = new SelectList(_db.foods, "Id", "Name", orders.FoodsId);
            ViewData["CourierId"] = new SelectList(_db.couriers, "Id", "Name", orders.CourierId);

            return View(orders);
        }

        // GET: CitiesController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null || _db.orders == null)
            {
                return NotFound();
            }

            var order = await _db.orders
                .Include(p => p.Cities)
                .Include(p => p.Restaurants)
                .Include(p => p.Courier)
                .Include(p => p.Foods)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: CitiesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int Id, Orders orders)
        {

            var order = await _db.orders.FindAsync(Id);
            if (order != null)
            {
                orders.DeletedDate = DateTime.Now;
                _db.orders.Remove(orders);
            }

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
    }
}
