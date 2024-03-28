

using NZWalksDev.DataAccess.Models.Domain;

namespace NZWalksDev.DataAccess.Repositories.Walks
{
    public interface IWalkRepository
    {
        Task<List<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true,
            int pageNumber = 1, int pageSize = 1000);
        Task<Walk?> GetByIdAsync(Guid id);
        Task<Walk> AddAsync(Walk walk);
        Task<Walk> UpdateAsync(Guid Id, Walk walk);
        Task<Walk?> DeleteAsync(Guid id);

    }
}



