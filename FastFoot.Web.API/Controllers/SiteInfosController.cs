using FastFood.DAL.Data;
using FastFood.DAL.DbModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FastFoot.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SiteInfosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SiteInfosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SiteInfo>>> GetSiteInfos()
        {
            return await _context.SiteInfos.ToListAsync();
        }

        // GET: api/SiteInfo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SiteInfo>> GetSiteInfos(int id)
        {
            var site = await _context.SiteInfos.FindAsync(id);

            if (site == null)
            {
                return NotFound();
            }

            return site;
        }
    }
}
