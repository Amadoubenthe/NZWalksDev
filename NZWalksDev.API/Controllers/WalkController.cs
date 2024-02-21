using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalksDev.DataAccess.Models.Domain;
using NZWalksDev.DataAccess.Models.DTO;
using NZWalksDev.DataAccess.Repositories.Walks;

namespace NZWalksDev.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalkController : ControllerBase
    {
        private readonly IWalkRepository _walkRepository;
        private readonly IMapper _mapper;

        public WalkController(IWalkRepository walkRepository, IMapper mapper)
        {
            _walkRepository = walkRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> AddWalk([FromBody] WalkDtoRequest walkDtoRequest)
        {
            // Map Dto to domain Model
            var walkDomainModel = _mapper.Map<Walk>(walkDtoRequest);

            await _walkRepository.AddAsync(walkDomainModel);

            // Map Domain Model to Dto
            var walkDto = _mapper.Map<WalkDto>(walkDomainModel);

            return Ok(walkDto);
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var walksDomain = await _walkRepository.GetAllAsync();

            /*var walksDto = new List<Walk>();

            foreach (var walk in walksDomain)
            {
                walksDto.Add(new Walk()
                {
                    Id = walk.Id,
                    Name = walk.Name,
                    Description = walk.Description,
                });
            }*/

            var walksDto = _mapper.Map<List<WalkDto>>(walksDomain);

            return Ok(walksDto);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walkDomain = await _walkRepository.GetByIdAsync(id);

            if (walkDomain == null)
            {
                return NotFound();
            }

            var walkDto = _mapper.Map<WalkDto>(walkDomain);

            return Ok(walkDto);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateWalk([FromRoute] Guid id, UpdateWalkRequestDto updateWalkRequestDto)
        {
            // Map DTO to Domain Model
            var walkDomainModel = _mapper.Map<Walk>(updateWalkRequestDto);

            walkDomainModel = await _walkRepository.UpdateAsync(id, walkDomainModel);

            if (walkDomainModel == null)
            {
                return NotFound(); 
            }

            var walkDto = _mapper.Map<WalkDto>(walkDomainModel);

            return Ok(walkDto);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var walkDeletedDomainModel = await _walkRepository.DeleteAsync(id);

            if (walkDeletedDomainModel == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<WalkDto>(walkDeletedDomainModel));
        }
    }
}
