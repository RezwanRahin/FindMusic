using FindMusic.Models;

namespace FindMusic.Repository
{
    public interface ISeasonRepository
    {
        Task<Season> Add(Season season);
        Task<Season?> GetSeason(int number, string seriesSlug);
        Task<Season?> GetSeasonWithRelatedData(int id);
        Task<Season?> GetSeasonWithRelatedData(int number, string seriesSlug);
        Task<Season> Update(Season season);
        Task<Season> Delete(Season season);
    }
}
