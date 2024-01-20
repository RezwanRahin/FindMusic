using FindMusic.Models;

namespace FindMusic.Repository
{
    public interface ITrackRepository
    {
        Task<Track> Add(Track track);
        Task<Track?> GetTrack(int id);
        Task<Track?> GetTrackWithRelatedData(int id);
        Task<Track?> GetTrackWithEpisodeData(int id);
        Task<Track?> GetTrackWithMovieData(int id);
        Task<Track> Update(Track modifiedTrack);
        Task<Track> Delete(Track track);
    }
}
