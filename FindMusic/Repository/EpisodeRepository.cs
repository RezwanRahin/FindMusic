using FindMusic.Context;
using FindMusic.Models;
using Microsoft.EntityFrameworkCore;

namespace FindMusic.Repository
{
    public class EpisodeRepository : IEpisodeRepository
    {
        private readonly AppDbContext _context;

        public EpisodeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Episode> Add(Episode episode)
        {
            await _context.Episodes.AddAsync(episode);
            await _context.SaveChangesAsync();
            return episode;
        }

        public async Task<Episode> Delete(Episode episode)
        {
            _context.Episodes.Remove(episode);
            await _context.SaveChangesAsync();
            return episode;
        }

        public async Task<Episode?> GetEpisode(int number, int season, string seriesSlug)
        {
            try
            {
                return await _context.Episodes
                            .Include(e => e.User)
                            .Include(e => e.Season)
                            .ThenInclude(s => s.Series)
                            .SingleAsync(e => e.Number == number && e.Season.Number == season && e.Season.Series.Slug == seriesSlug);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Episode?> GetEpisodeWithRelatedData(int id)
        {
            try
            {
                return await _context.Episodes
                            .Include(e => e.Season)
                            .ThenInclude(s => s.Series)
                            .SingleAsync(e => e.Id == id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Task<Episode?> GetEpisodeWithRelatedData(int number, int season, string seriesSlug)
        {
            throw new NotImplementedException();
        }

        public Task<Episode> Update(Episode episode)
        {
            throw new NotImplementedException();
        }
    }
}
