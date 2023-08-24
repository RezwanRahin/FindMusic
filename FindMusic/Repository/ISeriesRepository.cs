using FindMusic.Models;

namespace FindMusic.Repository
{
	public interface ISeriesRepository
	{
		Task<Series> Add(Series series);
		Task<Series?> GetSeriesById(int id);
		Task<Series?> GetSeriesBySlug(string slug);
		Task<Series?> GetSeriesBySlugWithRelatedData(string slug);
		Task<IEnumerable<Series>> GetAllSeries();
		Task<IEnumerable<Series>> GetLatestSeries();
		Task<Series> Update(Series modifiedSeries);
		Task<Series> Delete(Series series);
	}
}
