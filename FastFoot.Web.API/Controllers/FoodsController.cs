using FastFood.DAL.Data;
using FastFood.DAL.DbModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FastFoot.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FoodsController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<IEnumerable<Foods>>> GetFoods()
        {
            return await _context.foods.ToListAsync();
        }

        // GET: api/Foods/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Foods>> GetFoods(int id)
        {
            var food = await _context.foods.FindAsync(id);

            if (food == null)
            {
                return NotFound();
            }

            return food;
        }
    }
}
