using System.ComponentModel.DataAnnotations;

namespace FindMusic.ViewModels.TrackViewModels
{
    public class TrackFormViewModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Url { get; set; }
    }
}
