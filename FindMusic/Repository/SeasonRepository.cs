using FindMusic.Context;
using FindMusic.Models;
using Microsoft.EntityFrameworkCore;

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

        public async Task<Season> Delete(Season season)
        {
            _context.Seasons.Remove(season);
            await _context.SaveChangesAsync();
            return season;
        }

        public async Task<Season?> GetSeason(int number, string seriesSlug)
        {
            try
            {
                return await _context.Seasons
                            .Include(s => s.Series)
                            .SingleAsync(s => s.Series.Slug == seriesSlug && s.Number == number);
            }
            catch (Exception)
            {
                return null;
            }
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
