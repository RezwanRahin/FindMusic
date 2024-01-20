using FindMusic.ViewModels.TrackViewModels;

namespace FindMusic.ViewModels.TimestampViewModels
{
    public class TimestampDetailsViewModel : TimestampContentViewModel
    {
        public int Id { get; set; }
        public ICollection<TrackDetailsViewModel>? Tracks { get; set; }
    }
}
