
using AutoMapper;
using NZWalksDev.DataAccess.Models.Domain;
using NZWalksDev.DataAccess.Models.DTO;

namespace NZWalksDev.DataAccess.Mappings
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Region, RegionDto>().ReverseMap();
            CreateMap<RegionDtoRequest, Region>().ReverseMap();
            CreateMap<UpdateRegionDtoRequest, Region>().ReverseMap();
            // Walk
            CreateMap<WalkDtoRequest, Walk>().ReverseMap();
            CreateMap<WalkDto, Walk>().ReverseMap();
            CreateMap<UpdateWalkRequestDto, Walk>().ReverseMap();
        }
    }
}
