using FindMusic.Context;
using FindMusic.Models;

namespace FindMusic.Repository
{
    public class TrackRepository : ITrackRepository
    {
        private readonly AppDbContext _context;

        public TrackRepository(AppDbContext context)
        {
            _context = context;
        }

        public Task<Track> Add(Track track)
        {
            throw new NotImplementedException();
        }

        public Task<Track> Delete(Track track)
        {
            throw new NotImplementedException();
        }

        public Task<Track?> GetTrack(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Track?> GetTrackWithEpisodeData(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Track?> GetTrackWithMovieData(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Track?> GetTrackWithRelatedData(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Track> Update(Track modifiedTrack)
        {
            throw new NotImplementedException();
        }
    }
}
