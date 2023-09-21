using System.ComponentModel.DataAnnotations;

namespace FindMusic.Models
{
	public class Comment
	{
		public int Id { get; set; }

		public string Name { get; set; }

		[Required]
		public string Text { get; set; }

		[Required]
		public DateTime Time { get; set; }

		public string? ApplicationUserId { get; set; }
		public ApplicationUser? User { get; set; }

		public int TrackId { get; set; }
		public Track Track { get; set; }
	}
}
