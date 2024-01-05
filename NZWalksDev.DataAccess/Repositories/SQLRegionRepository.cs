using Microsoft.EntityFrameworkCore;
using NZWalksDev.DataAccess.Data;
using NZWalksDev.DataAccess.Models.Domain;

namespace NZWalksDev.DataAccess.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext _dbContext;
        public SQLRegionRepository(NZWalksDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public async Task<List<Region>> GetAllAsync()
        {
            var regions = await _dbContext.Regions.ToListAsync();

            return regions;
        }
    }
}
