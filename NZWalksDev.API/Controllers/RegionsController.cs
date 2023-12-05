using Microsoft.AspNetCore.Mvc;
using NZWalksDev.DataAccess.Data;
using NZWalksDev.DataAccess.Models.Domain;
using NZWalksDev.DataAccess.Models.DTO;

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
            // Get Data From Dtabase - Domain models
            var regionsDomain = _dbContext.Regions.ToList();

            // Map Domain Models to DTOs
            var regionsDto = new List<RegionDto>();
            foreach (var region in regionsDomain)
            {
                regionsDto.Add(new RegionDto()
                {
                    Id = region.Id,
                    Code = region.Code,
                    Name = region.Name,
                    RegionImageUrl = region.RegionImageUrl,
                });
            }

            // Return DTOs
            return Ok(regionsDto);
        }

        [HttpGet("{id:guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            // Get data from Database - Domain Models
            var regionDomain = _dbContext.Regions.FirstOrDefault(r => r.Id == id);

            if (regionDomain == null)
            {
                return NotFound();
            }

            // Map Doain Model To Dto
            var regionDto = new RegionDto()
            {
                Id = regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                RegionImageUrl = regionDomain.RegionImageUrl,
            };

            // Return DTO
            return Ok(regionDto);
        }

        [HttpPost]
        public IActionResult AddRegion([FromBody] RegionDtoRequest regionDtoRequest)
        {
            // Map or Convert DTO To Domain Model
            var regionDomain = new Region()
            {
                Code = regionDtoRequest.Code,
                Name = regionDtoRequest.Name,
                RegionImageUrl = regionDtoRequest.RegionImageUrl,
            };

            // Use Domain Model to Create region
            _dbContext.Regions.Add(regionDomain);
            _dbContext.SaveChanges();

            // Map Domain model back to DTO
            var regionDto = new RegionDto()
            {
                Id = regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                RegionImageUrl = regionDomain.RegionImageUrl,
            };

            return CreatedAtAction(nameof(GetById), new {id = regionDto.Id} ,regionDto);

        }

        [HttpPut("{id:guid}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateRegionDtoRequest updateRegionDtoRequest) 
        { 
            var regionDomain =  _dbContext.Regions.FirstOrDefault(r => r.Id == id);

            if (regionDomain == null)
            {
                return NotFound();
            }

            // Map DTO To Domain Model
            regionDomain.Code = updateRegionDtoRequest.Code;
            regionDomain.Name = updateRegionDtoRequest.Name;
            regionDomain.RegionImageUrl = updateRegionDtoRequest.RegionImageUrl;

            _dbContext.SaveChanges();

            // Convert Domain Model to DTO
            var regionDto = new RegionDto()
            {
                Id = regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                RegionImageUrl = regionDomain.RegionImageUrl,
            };

            return Ok(regionDto);
        }

        [HttpDelete("{id:guid}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            var regionDomain = _dbContext.Regions.FirstOrDefault(x => x.Id == id);

            if (regionDomain == null)
            {
                return NotFound();
            }

            _dbContext.Regions.Remove(regionDomain);
            _dbContext.SaveChanges();

            return Ok();
        }

    }
}
