using FindMusic.Context;
using FindMusic.Models;
using Microsoft.EntityFrameworkCore;

namespace FindMusic.Repository
{
    public class TimestampRepository : ITimestampRepository
    {
        private readonly AppDbContext _context;

        public TimestampRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Timestamp> Add(Timestamp timestamp)
        {
            await _context.Timestamps.AddAsync(timestamp);
            await _context.SaveChangesAsync();
            return timestamp;
        }

        public async Task<Timestamp> Delete(Timestamp timestamp)
        {
            _context.Timestamps.Remove(timestamp);
            await _context.SaveChangesAsync();
            return timestamp;
        }

        public async Task<Timestamp?> GetTimestamp(int id)
        {
            return await _context.Timestamps.FindAsync(id);
        }

        public async Task<Timestamp?> GetTimestampWithEpisodeData(int id)
        {
            try
            {
                return await _context.Timestamps
                            .Include(t => t.Episode)
                            .ThenInclude(e => e!.Season)
                            .ThenInclude(s => s.Series)
                            .SingleAsync(t => t.Id == id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Task<Timestamp?> GetTimestampWithMovieData(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Timestamp?> GetTimestampWithRelatedData(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Timestamp> Update(Timestamp modifiedTimestamp)
        {
            throw new NotImplementedException();
        }
    }
}
