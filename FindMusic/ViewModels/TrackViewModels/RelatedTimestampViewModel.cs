using System.ComponentModel.DataAnnotations;

namespace FindMusic.ViewModels.TrackViewModels
{
    public class RelatedTimestampViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int Hour { get; set; }

        [Required]
        public int Minute { get; set; }

        [Required]
        public int Second { get; set; }
    }
}
