using FindMusic.ViewModels.TimestampViewModels;

namespace FindMusic.ViewModels.MovieViewModels
{
    public class MovieDetailsViewModel : MovieContentViewModel
    {
        public ICollection<TimestampDetailsViewModel>? Timestamps { get; set; }
    }
}
