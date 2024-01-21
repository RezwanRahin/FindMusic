using System.ComponentModel.DataAnnotations;

namespace FindMusic.ViewModels.TrackViewModels
{
    public class CreateTrackViewModel : TrackFormViewModel
    {
        [Required]
        public RelatedTimestampViewModel Timestamp { get; set; }

        [Required]
        public ContentDetailsViewModel Content { get; set; }
    }
}
