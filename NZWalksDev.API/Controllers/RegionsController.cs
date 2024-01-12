﻿using Microsoft.AspNetCore.Mvc;
using NZWalksDev.DataAccess.Data;
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

        public RegionsController(IRegionRepository regionRepository)
        {
           /* _dbContext = dbContext;*/
            _regionRepository = regionRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var regionsDomain = await _regionRepository.GetAllAsync();

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
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
        {
            var regionDomain = await _regionRepository.GetByIdAsync(id);

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
        public async Task<IActionResult> AddRegion([FromBody] RegionDtoRequest regionDtoRequest)
        {
            // Map or Convert DTO To Domain Model
            var regionDomainModel = new Region()
            {
                Code = regionDtoRequest.Code,
                Name = regionDtoRequest.Name,
                RegionImageUrl = regionDtoRequest.RegionImageUrl,
            };

            regionDomainModel = await _regionRepository.CreateAsync(regionDomainModel);

            // Map Domain model back to DTO
            var regionDto = new RegionDto()
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl,
            };

            return CreatedAtAction(nameof(GetByIdAsync), new {id = regionDto.Id} ,regionDto);

        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionDtoRequest updateRegionDtoRequest)
        {

            var regionDomainModel = new Region
            {
                Code = updateRegionDtoRequest.Code,
                Name = updateRegionDtoRequest.Name,
                RegionImageUrl = updateRegionDtoRequest.RegionImageUrl,
            };

            regionDomainModel = await _regionRepository.UpdateAsync(id, regionDomainModel);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            // Convert Domain Model to DTO
            var regionDto = new RegionDto()
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl,
            };

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
            var regionDto = new RegionDto
            {

                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl,
            };

            return Ok(regionDto);
        }

    }
}
