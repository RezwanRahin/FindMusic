using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FindMusic.Models
{
    [Table("Series")]
    [Index(nameof(Slug), IsUnique = true)]
    public class Series
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Slug { get; set; }

        [Required]
        public string PhotoPath { get; set; }

        public string? ApplicationUserId { get; set; }
        public ApplicationUser? User { get; set; }

        public ICollection<Season> Seasons { get; set; } = new List<Season>();
    }
}
