using System.ComponentModel.DataAnnotations;

namespace FindMusic.ViewModels.EpisodeViewModels
{
    public class CreateEpisodeViewModel : EpisodeFormViewModel
    {
        [Required]
        public string SeriesSlug { get; set; }

        public string? SeriesName { get; set; }


        [Required]
        public int SeasonNumber { get; set; }
    }
}
