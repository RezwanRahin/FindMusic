using System.ComponentModel.DataAnnotations;

namespace FindMusic.Models
{
	public class Vote
	{
		public int Id { get; set; }

		[Required]
		public bool Type { get; set; }

		public string? ApplicationUserId { get; set; }
		public ApplicationUser? User { get; set; }

		public int TrackId { get; set; }
		public Track Track { get; set; }
	}
}
