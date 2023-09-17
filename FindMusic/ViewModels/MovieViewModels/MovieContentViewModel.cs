using System.ComponentModel.DataAnnotations;

namespace FindMusic.ViewModels.MovieViewModels
{
    public class MovieContentViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Release Date")]
        public DateTime ReleaseDate { get; set; }

        public ContributorViewModel? Contributor { get; set; }
        public PosterViewModel Poster { get; set; }
    }
}
