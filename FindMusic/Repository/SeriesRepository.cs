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

		public async Task<Series?> GetSeriesBySlugWithRelatedData(string slug)
		{
			try
			{
				return await _context.Series
							.Include(s => s.User)
							.Include(s => s.Seasons)
							.Select(s => new Series
							{
								Id = s.Id,
								Name = s.Name,
								Slug = s.Slug,
								PhotoPath = s.PhotoPath,
								User = s.User,
								Seasons = s.Seasons.OrderBy(s => s.Number).ToList()
							})
							.SingleAsync(s => s.Slug == slug);
			}
			catch (Exception)
			{
				return null;
			}
		}

		public async Task<Series> Update(Series modifiedSeries)
		{
			_context.Series.Update(modifiedSeries);
			await _context.SaveChangesAsync();
			return modifiedSeries;
		}
	}
}
