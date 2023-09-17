namespace FindMusic.ViewModels.MovieViewModels
{
    public class UpdateMovieViewModel : MovieContentViewModel
    {
        public string? Slug { get; set; }
        public string? PhotoPath { get; set; }
        public IFormFile? Photo { get; set; }
    }
}
