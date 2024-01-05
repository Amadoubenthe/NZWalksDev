
using NZWalksDev.DataAccess.Models.Domain;

namespace NZWalksDev.DataAccess.Repositories
{
    public interface IRegionRepository
    {
        Task<List<Region>> GetAllAsync();
    }
}
