using Microsoft.AspNetCore.Mvc;
using NZWalksDev.DataAccess.Data;

namespace NZWalksDev.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext _dbContext;
        public RegionsController(NZWalksDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var regions = _dbContext.Regions.ToList();

            return Ok(regions);
        }
    }
}
