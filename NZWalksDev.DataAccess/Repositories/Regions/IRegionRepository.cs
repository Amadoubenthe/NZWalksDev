using NZWalksDev.DataAccess.Models.Domain;

namespace NZWalksDev.DataAccess.Repositories.Regions
{
    public interface IRegionRepository
    {
        Task<List<Region>> GetAllAsync();
        Task<Region> AddAsync(Region region);
        Task<Region?> GetByIdAsync(Guid id);
        Task<Region?> UpdateAsync(Guid id, Region region);
        Task<Region?> DeleteAsync(Guid id);
    }
}
