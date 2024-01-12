﻿
using NZWalksDev.DataAccess.Models.Domain;

namespace NZWalksDev.DataAccess.Repositories
{
    public interface IRegionRepository
    {
        Task<List<Region>> GetAllAsync();
        Task<Region> CreateAsync(Region region);
        Task<Region?> GetByIdAsync(Guid id);
        Task<Region?> UpdateAsync(Guid id, Region region);
        Task<Region?> DeleteAsync(Guid id);
    }
}
