using System.ComponentModel.DataAnnotations;

namespace FindMusic.ViewModels.MovieViewModels
{
    public class CreateMovieViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Release Date")]
        public DateTime ReleaseDate { get; set; }

        [Required]
        public IFormFile Photo { get; set; }
    }
}
