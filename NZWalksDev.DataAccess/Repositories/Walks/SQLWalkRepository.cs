﻿

using NZWalksDev.DataAccess.Data;
using NZWalksDev.DataAccess.Models.Domain;

namespace NZWalksDev.DataAccess.Repositories.Walks
{
    public class SQLWalkRepository: IWalkRepository
    {
        private readonly NZWalksDbContext _dbContext;

        public SQLWalkRepository(NZWalksDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Walk> AddAsync(Walk walk)
        {
            await _dbContext.Walks.AddAsync(walk);
            await _dbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<List<Walk>> GetAllAsync()
        {
            var walks = _dbContext.Walks.ToList();
            return walks;
        }

        public Task<Walk> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}