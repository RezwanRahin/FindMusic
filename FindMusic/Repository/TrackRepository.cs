using FindMusic.Context;
using FindMusic.Models;
using Microsoft.EntityFrameworkCore;

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

        public async Task<Track?> GetTrack(int id)
        {
            try
            {
                return await _context.Tracks.FindAsync(id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Track?> GetTrackWithEpisodeData(int id)
        {
            try
            {
                return await _context.Tracks
                        .Include(t => t.Timestamp)
                        .ThenInclude(t => t.Episode)
                        .ThenInclude(e => e!.Season)
                        .ThenInclude(s => s.Series)
                        .SingleAsync(t => t.Id == id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Track?> GetTrackWithMovieData(int id)
        {
            return await _context.Tracks
                        .Include(t => t.Timestamp)
                        .ThenInclude(t => t.Movie)
                        .SingleAsync(t => t.Id == id);
        }

        public async Task<Track?> GetTrackWithRelatedData(int id)
        {
            try
            {
                return await _context.Tracks
                        .Include(t => t.Timestamp)
                        .ThenInclude(t => t.Episode)
                        .ThenInclude(e => e!.Season)
                        .ThenInclude(s => s.Series)
                        .Include(t => t.Timestamp)
                        .ThenInclude(t => t.Movie)
                        .SingleAsync(t => t.Id == id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Track> Update(Track modifiedTrack)
        {
            _context.Tracks.Update(modifiedTrack);
            await _context.SaveChangesAsync();
            return modifiedTrack;
        }
    }
}
