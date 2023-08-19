using System.ComponentModel.DataAnnotations;

namespace FindMusic.Models
{
	public class Donation
	{
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }

		[Required]
		public float Amount { get; set; }

		[Required]
		public DateTime DateTime { get; set; }
	}
}
