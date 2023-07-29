using FastFood.DAL.Data;
using FastFood.DAL.DbModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace FastFoot.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

    public class CouriersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CouriersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Courier
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Courier>>> GetCouriers()
        {
            return await _context.couriers.ToListAsync();
        }

        // GET: api/Courier/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Courier>> GetCourier(int id)
        {
            var courier = await _context.couriers.FindAsync(id);

            if (courier == null)
            {
                return NotFound();
            }

            return courier;
        }

    }
}
