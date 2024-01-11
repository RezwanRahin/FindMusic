using System.ComponentModel.DataAnnotations;

namespace FindMusic.ViewModels.SeasonViewModels
{
    public class UpdateSeasonViewModel : CreateSeasonViewModel
    {
        [Required]
        public int Id { get; set; }
    }
}
