using FastFood.DAL.Data;
using FastFood.DAL.DbModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FastFoot.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrdersController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<IEnumerable<Orders>>> GetFoods()
        {
            return await _context.orders.ToListAsync();
        }

        // GET: api/Order/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Orders>> GetFoods(int id)
        {
            var order = await _context.orders.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }
    }
}
