using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace FindMusic.Models
{
	[Index(nameof(Slug), IsUnique = true)]
	public class Movie
	{
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }

		[Required]
		public DateTime ReleaseDate { get; set; }

		[Required]
		public string Slug { get; set; }

		[Required]
		public string PhotoPath { get; set; }

		public string? ApplicationUserId { get; set; }
		public ApplicationUser? User { get; set; }

		public ICollection<Timestamp> Timestamps { get; set; }
	}
}
