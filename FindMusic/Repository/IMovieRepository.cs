using FindMusic.Models;

namespace FindMusic.Repository
{
	public interface IMovieRepository
	{
		Task<Movie> Add(Movie movie);
		Task<Movie?> GetMovieById(int id);
		Task<Movie?> GetMovieBySlug(string slug);
		Task<Movie?> GetMovieWithRelatedData(string slug);
		Task<IEnumerable<Movie>> GetAllMovies();
		Task<IEnumerable<Movie>> GetLatestMovies();
		Task<Movie> Update(Movie modifiedMovie);
		Task<Movie> Delete(Movie movie);
	}
}
