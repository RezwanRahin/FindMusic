using FindMusic.Models;

namespace FindMusic.Repository
{
    public interface IEpisodeRepository
    {
        Task<Episode> Add(Episode episode);
        Task<Episode?> GetEpisode(int number, int season, string seriesSlug);
        Task<Episode?> GetEpisodeWithRelatedData(int id);
        Task<Episode?> GetEpisodeWithRelatedData(int number, int season, string seriesSlug);
        Task<Episode> Update(Episode episode);
        Task<Episode> Delete(Episode episode);
    }
}
