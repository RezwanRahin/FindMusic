using System.ComponentModel.DataAnnotations;

namespace FindMusic.Models
{
	public class Episode
	{
		public int Id { get; set; }

		[Required]
		public int Number { get; set; }

		public string? Name { get; set; }

		public int SeasonId { get; set; }
		public Season Season { get; set; }

		public string? ApplicationUserId { get; set; }
		public ApplicationUser? User { get; set; }

		public ICollection<Timestamp> Timestamps { get; set; }
	}
}
