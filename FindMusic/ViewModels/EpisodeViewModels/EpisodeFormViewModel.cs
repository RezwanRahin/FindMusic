using System.ComponentModel.DataAnnotations;

namespace FindMusic.ViewModels.EpisodeViewModels
{
    public class EpisodeFormViewModel
    {
        [Required]
        public int Number { get; set; }

        public string? Name { get; set; }
    }
}
