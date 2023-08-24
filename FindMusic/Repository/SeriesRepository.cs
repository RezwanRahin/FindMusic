using FindMusic.Context;
using FindMusic.Models;

namespace FindMusic.Repository
{
	public class SeriesRepository : ISeriesRepository
	{
		private readonly AppDbContext _context;

		public SeriesRepository(AppDbContext context)
		{
			_context = context;
		}

		public async Task<Series> Add(Series series)
		{
			await _context.Series.AddAsync(series);
			await _context.SaveChangesAsync();
			return series;
		}

		public async Task<Series> Delete(Series series)
		{
			_context.Series.Remove(series);
			await _context.SaveChangesAsync();
			return series;
		}

		public Task<IEnumerable<Series>> GetAllSeries()
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<Series>> GetLatestSeries()
		{
			throw new NotImplementedException();
		}

		public Task<Series> GetSeriesById(int id)
		{
			throw new NotImplementedException();
		}

		public Task<Series> GetSeriesBySlug(string slug)
		{
			throw new NotImplementedException();
		}

		public Task<Series> GetSeriesBySlugWithRelatedData(string slug)
		{
			throw new NotImplementedException();
		}

		public Task<Series> Update(Series modifiedSeries)
		{
			throw new NotImplementedException();
		}
	}
}
