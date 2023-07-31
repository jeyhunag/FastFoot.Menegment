using FastFood.DAL.Data;
using FastFood.DAL.DbModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FastFoot.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampaignsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CampaignsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Campaign>>> GetCampaigns()
        {
            return await _context.Campaigns.ToListAsync();
        }

        // GET: api/Campaign/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Campaign>> GetCampaigns(int id)
        {
            var cam = await _context.Campaigns.FindAsync(id);

            if (cam == null)
            {
                return NotFound();
            }

            return cam;
        }
    }
}
