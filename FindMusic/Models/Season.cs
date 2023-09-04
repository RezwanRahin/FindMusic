using System.ComponentModel.DataAnnotations;

namespace FindMusic.Models
{
    public class Season
    {
        public int Id { get; set; }

        [Required]
        public int Number { get; set; }

        [Required]
        public DateTime Year { get; set; }

        public int SeriesId { get; set; }
        public Series Series { get; set; }

        public string? ApplicationUserId { get; set; }
        public ApplicationUser? User { get; set; }

        public ICollection<Episode> Episodes { get; set; } = new List<Episode>();
    }
}
