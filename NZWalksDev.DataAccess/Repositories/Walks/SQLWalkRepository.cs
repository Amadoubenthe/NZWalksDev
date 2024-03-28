

using Microsoft.EntityFrameworkCore;
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
        /*Task<List<Walk>> IWalkRepository.GetAllAsync(string? filterOn, string? filterQuery, string? sortBy, bool isAscending, int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }*/

        public async Task<List<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 1000)
        {
            var walks = _dbContext.Walks.AsQueryable();

            // Filtring
            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Name.Contains(filterQuery));
                }
            }

            // Sorting
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.Name) : walks.OrderByDescending(x => x.Name);
                }

                if (sortBy.Equals("Length", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.LengthInKm) : walks.OrderByDescending(x => x.LengthInKm);
                }
            }

            // Pagination
            var skipResults = (pageNumber - 1) * pageSize;

            return await walks.Skip(skipResults).Take(pageSize).ToListAsync();
            // var walks = await _dbContext.Walks.ToListAsync();
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

        Task<Walk?> IWalkRepository.GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        Task<Walk> IWalkRepository.AddAsync(Walk walk)
        {
            throw new NotImplementedException();
        }

        Task<Walk> IWalkRepository.UpdateAsync(Guid Id, Walk walk)
        {
            throw new NotImplementedException();
        }

        Task<Walk?> IWalkRepository.DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
