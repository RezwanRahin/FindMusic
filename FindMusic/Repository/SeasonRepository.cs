using FindMusic.Context;
using FindMusic.Models;

namespace FindMusic.Repository
{
    public class SeasonRepository : ISeasonRepository
    {
        private readonly AppDbContext _context;

        public SeasonRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Season> Add(Season season)
        {
            await _context.Seasons.AddAsync(season);
            await _context.SaveChangesAsync();
            return season;
        }

        public Task<Season> Delete(Season season)
        {
            throw new NotImplementedException();
        }

        public Task<Season?> GetSeason(int number, string seriesSlug)
        {
            throw new NotImplementedException();
        }

        public Task<Season?> GetSeasonWithRelatedData(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Season?> GetSeasonWithRelatedData(int number, string seriesSlug)
        {
            throw new NotImplementedException();
        }

        public Task<Season> Update(Season season)
        {
            throw new NotImplementedException();
        }
    }
}
