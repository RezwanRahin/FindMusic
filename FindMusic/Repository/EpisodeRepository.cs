using FindMusic.Context;
using FindMusic.Models;

namespace FindMusic.Repository
{
    public class EpisodeRepository : IEpisodeRepository
    {
        private readonly AppDbContext _context;

        public EpisodeRepository(AppDbContext context)
        {
            _context = context;
        }

        public Task<Episode> Add(Episode episode)
        {
            throw new NotImplementedException();
        }

        public Task<Episode> Delete(Episode episode)
        {
            throw new NotImplementedException();
        }

        public Task<Episode?> GetEpisode(int number, int season, string seriesSlug)
        {
            throw new NotImplementedException();
        }

        public Task<Episode?> GetEpisodeWithRelatedData(int id)
        {
            throw new NotImplementedException();
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