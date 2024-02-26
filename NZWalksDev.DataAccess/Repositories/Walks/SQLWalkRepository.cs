

using Microsoft.EntityFrameworkCore;
using NZWalksDev.DataAccess.Data;
using NZWalksDev.DataAccess.Models.Domain;
using NZWalksDev.DataAccess.Models.DTO;

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

        public async Task<Walk?> GetByIdAsync(Guid id)
        {
            var walk = await _dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);

            if (walk == null)
            {
                return null;
            }

            return walk;
        }

        public async Task<Walk> UpdateAsync(Guid Id, Walk walk)
        {
            var existingWalk = await _dbContext.Walks.FirstOrDefaultAsync(x => x.Id == Id);

            if (existingWalk == null)
            {
                return null;
            }

            existingWalk.Name = walk.Name;
            existingWalk.Description = walk.Description;
            existingWalk.LengthInKm = walk.LengthInKm;
            existingWalk.WalkImageUrl = walk.WalkImageUrl;

            await _dbContext.SaveChangesAsync();

            return existingWalk;

        }

        public async Task<Walk?> DeleteAsync(Guid id)
        {
            var walk = await _dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);

            if (walk == null)
            {
                return null;
            }

            _dbContext.Walks.Remove(walk);
            await _dbContext.SaveChangesAsync();

            return walk;
        }
    }
}
