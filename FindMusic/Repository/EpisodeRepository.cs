using FindMusic.Models;

namespace FindMusic.Repository
{
    public class EpisodeRepository : IEpisodeRepository
    {
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
