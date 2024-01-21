using System.ComponentModel.DataAnnotations;

namespace FindMusic.ViewModels.TrackViewModels
{
    public class UpdateTrackViewModel : CreateTrackViewModel
    {
        [Required]
        public int Id { get; set; }
    }
}
