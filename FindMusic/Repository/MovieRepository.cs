using FindMusic.Models;

namespace FindMusic.Repository
{
	public class MovieRepository : IMovieRepository
	{
		public Task<Movie> Add(Movie movie)
		{
			throw new NotImplementedException();
		}

		public Task<Movie> Delete(Movie movie)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<Movie>> GetAllMovies()
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<Movie>> GetLatestMovies()
		{
			throw new NotImplementedException();
		}

		public Task<Movie?> GetMovieById(int id)
		{
			throw new NotImplementedException();
		}

		public Task<Movie?> GetMovieBySlug(string slug)
		{
			throw new NotImplementedException();
		}

		public Task<Movie?> GetMovieWithRelatedData(string slug)
		{
			throw new NotImplementedException();
		}

		public Task<Movie> Update(Movie modifiedMovie)
		{
			throw new NotImplementedException();
		}
	}
}
