using FindMusic.Context;
using FindMusic.Models;

namespace FindMusic.Repository
{
    public class TimestampRepository : ITimestampRepository
    {
        private readonly AppDbContext _context;

        public TimestampRepository(AppDbContext context)
        {
            _context = context;
        }

        public Task<Timestamp> Add(Timestamp timestamp)
        {
            throw new NotImplementedException();
        }

        public Task<Timestamp> Delete(Timestamp timestamp)
        {
            throw new NotImplementedException();
        }

        public Task<Timestamp?> GetTimestamp(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Timestamp?> GetTimestampWithEpisodeData(int id)
        {
            throw new NotImplementedException();
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
