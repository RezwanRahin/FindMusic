﻿using FindMusic.Context;
using FindMusic.Models;
using Microsoft.EntityFrameworkCore;

namespace FindMusic.Repository
{
	public class MovieRepository : IMovieRepository
	{
		private readonly AppDbContext _context;

		public MovieRepository(AppDbContext context)
		{
			_context = context;
		}

		public async Task<Movie> Add(Movie movie)
		{
			await _context.Movies.AddAsync(movie);
			await _context.SaveChangesAsync();
			return movie;
		}

		public async Task<Movie> Delete(Movie movie)
		{
			_context.Movies.Remove(movie);
			await _context.SaveChangesAsync();
			return movie;
		}

		public async Task<IEnumerable<Movie>> GetAllMovies()
		{
			return await _context.Movies.ToListAsync();
		}

		public async Task<IEnumerable<Movie>> GetLatestMovies()
		{
			return await _context.Movies
						.OrderBy(m => m.ReleaseDate)
						.Take(10)
						.ToListAsync();
		}

		public async Task<Movie?> GetMovieById(int id)
		{
			try
			{
				return await _context.Movies.FindAsync(id);
			}
			catch (Exception)
			{
				return null;
			}
		}

		public async Task<Movie?> GetMovieBySlug(string slug)
		{
			try
			{
				return await _context.Movies.SingleAsync(s => s.Slug == slug);

			}
			catch (Exception)
			{
				return null;
			}
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
