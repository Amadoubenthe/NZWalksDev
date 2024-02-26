

using NZWalksDev.DataAccess.Models.Domain;

namespace NZWalksDev.DataAccess.Repositories.Walks
{
    public interface IWalkRepository
    {
        Task<List<Walk>> GetAllAsync();
        Task<Walk?> GetByIdAsync(Guid id);
        Task<Walk> AddAsync(Walk walk);
        Task<Walk> UpdateAsync(Guid Id, Walk walk);
        Task<Walk?> DeleteAsync(Guid id);
    }
}
