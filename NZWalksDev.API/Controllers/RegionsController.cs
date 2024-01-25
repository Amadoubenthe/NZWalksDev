using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalksDev.DataAccess.Models.Domain;
using NZWalksDev.DataAccess.Models.DTO;
using NZWalksDev.DataAccess.Repositories;

namespace NZWalksDev.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IRegionRepository _regionRepository;
        private readonly IMapper _mapper;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            _regionRepository = regionRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var regionsDomain = await _regionRepository.GetAllAsync();

            // Map Domain Models to DTOs
            var regionsDto = _mapper.Map<List<RegionDto>>(regionsDomain);

            // Return DTOs
            return Ok(regionsDto);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
        {
            var regionDomain = await _regionRepository.GetByIdAsync(id);

            if (regionDomain == null)
            {
                return NotFound();
            }

            // Map Doain Model To Dto
            var regionDto = _mapper.Map<RegionDto>(regionDomain);

            // Return DTO
            return Ok(regionDto);
        }

        [HttpPost]
        public async Task<IActionResult> AddRegion([FromBody] RegionDtoRequest regionDtoRequest)
        {
            // Map or Convert DTO To Domain Model
            var regionDomainModel = new Region()
            {
                Code = regionDtoRequest.Code,
                Name = regionDtoRequest.Name,
                RegionImageUrl = regionDtoRequest.RegionImageUrl,
            };

            regionDomainModel = await _regionRepository.AddAsync(regionDomainModel);

            // Map Domain model back to DTO
            var regionDto = _mapper.Map<RegionDto>(regionDomainModel);

            return Ok(regionDto);

        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionDtoRequest updateRegionDtoRequest)
        {
            var regionDomainModel = _mapper.Map<Region>(updateRegionDtoRequest);

            regionDomainModel = await _regionRepository.UpdateAsync(id, regionDomainModel);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            // Convert Domain Model to DTO
            var regionDto = _mapper.Map<RegionDto>(regionDomainModel);

            return Ok(regionDto);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            /*ctr shift b*/
            var regionDomainModel = await _regionRepository.DeleteAsync(id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            /*Map Domain Model*/
            var regionDto = _mapper.Map<RegionDto>(regionDomainModel);

            return Ok(regionDto);
        }

    }
}
