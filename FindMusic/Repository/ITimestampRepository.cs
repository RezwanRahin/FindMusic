using FindMusic.Models;

namespace FindMusic.Repository
{
    public interface ITimestampRepository
    {
        Task<Timestamp> Add(Timestamp timestamp);
        Task<Timestamp?> GetTimestamp(int id);
        Task<Timestamp?> GetTimestampWithRelatedData(int id);
        Task<Timestamp?> GetTimestampWithEpisodeData(int id);
        Task<Timestamp?> GetTimestampWithMovieData(int id);
        Task<Timestamp> Update(Timestamp modifiedTimestamp);
        Task<Timestamp> Delete(Timestamp timestamp);
    }
}
