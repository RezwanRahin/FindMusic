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

        public async Task<Track> Add(Track track)
        {
            await _context.Tracks.AddAsync(track);
            await _context.SaveChangesAsync();
            return track;
        }

        public async Task<Track> Delete(Track track)
        {
            _context.Tracks.Remove(track);
            await _context.SaveChangesAsync();
            return track;
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
