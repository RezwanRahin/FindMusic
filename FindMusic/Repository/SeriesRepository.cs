using FindMusic.Context;
using FindMusic.Models;
using Microsoft.EntityFrameworkCore;

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

		public async Task<IEnumerable<Series>> GetAllSeries()
		{
			return await _context.Series.ToListAsync();
		}

		public async Task<IEnumerable<Series>> GetLatestSeries()
		{
			return await _context.Series
						.Include(s => s.Seasons)
						.Select(s => new Series
						{
							Name = s.Name,
							Slug = s.Slug,
							PhotoPath = s.PhotoPath,
							Seasons = s.Seasons.OrderBy(s => s.Year).ToList()
						})
						.Take(10)
						.ToListAsync();
		}

		public async Task<Series?> GetSeriesById(int id)
		{
			return await _context.Series.FindAsync(id);
		}

		public async Task<Series?> GetSeriesBySlug(string slug)
		{
			try
			{
				return await _context.Series.SingleAsync(s => s.Slug == slug);
			}
			catch (Exception)
			{
				return null;
			}
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
