using System.ComponentModel.DataAnnotations;

namespace FindMusic.Models
{
	public class Track
	{
		public int Id { get; set; }

		[Required]
		public string Title { get; set; }

		[Required]
		public string Url { get; set; }

		public string? ApplicationUserId { get; set; }
		public ApplicationUser? User { get; set; }

		public int TimestampId { get; set; }
		public Timestamp Timestamp { get; set; }
	}
}
