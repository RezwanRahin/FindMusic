using System.ComponentModel.DataAnnotations;
using FindMusic.ViewModels.SeriesViewModels;

namespace FindMusic.ViewModels.SeasonViewModels
{
    public class CreateSeasonViewModel : SeasonFormViewModel
    {
        [Required]
        public string SeriesSlug { get; set; }

        public string SeriesName { get; set; }
    }
}
