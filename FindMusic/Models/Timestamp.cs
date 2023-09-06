using System.ComponentModel.DataAnnotations;

namespace FindMusic.Models
{
    public class Timestamp
    {
        public int Id { get; set; }

        [Required]
        public int Hour { get; set; }

        [Required]
        public int Minute { get; set; }

        [Required]
        public int Second { get; set; } = 0;

        public string? ApplicationUserId { get; set; }
        public ApplicationUser? User { get; set; }

        public int? EpisodeId { get; set; }
        public Episode? Episode { get; set; }

        public int? MovieId { get; set; }
        public Movie? Movie { get; set; }

        public ICollection<Track> Tracks { get; set; }
    }
}
