using FindMusic.Models;

namespace FindMusic.Repository
{
    public class SeasonRepository : ISeasonRepository
    {
        public Task<Season> Add(Season season)
        {
            throw new NotImplementedException();
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
