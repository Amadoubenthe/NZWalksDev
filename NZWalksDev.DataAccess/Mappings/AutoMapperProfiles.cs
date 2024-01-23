
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
        }
    }
}
