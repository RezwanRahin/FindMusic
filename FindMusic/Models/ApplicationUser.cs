using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FindMusic.Models
{
    [Index(nameof(Email), IsUnique = true)]
    [Index(nameof(UserName), IsUnique = true)]
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public Gender Gender { get; set; }

        [Required]
        public DateTime DOB { get; set; }

        public string? PhotoPath { get; set; }

        public ICollection<Movie> Movies { get; set; } = new List<Movie>();
        public ICollection<Series> Series { get; set; } = new List<Series>();
        public ICollection<Season> Seasons { get; set; } = new List<Season>();
        public ICollection<Episode> Episodes { get; set; } = new List<Episode>();
        public ICollection<Timestamp> Timestamps { get; set; } = new List<Timestamp>();
        public ICollection<Track> Tracks { get; set; } = new List<Track>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<Vote> Votes { get; set; } = new List<Vote>();
    }
}
