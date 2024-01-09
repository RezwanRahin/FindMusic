using System.ComponentModel.DataAnnotations;

namespace FindMusic.ViewModels.SeriesViewModels
{
    public class SeasonFormViewModel
    {
        [Required]
        public int Number { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Year { get; set; }
    }
}
