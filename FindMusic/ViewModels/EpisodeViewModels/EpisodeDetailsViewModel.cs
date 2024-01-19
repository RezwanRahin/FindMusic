using FindMusic.ViewModels.TimestampViewModels;

namespace FindMusic.ViewModels.EpisodeViewModels
{
    public class EpisodeDetailsViewModel : EpisodeContentViewModel
    {
        public ICollection<TimestampDetailsViewModel>? Timestamps { get; set; }
    }
}
