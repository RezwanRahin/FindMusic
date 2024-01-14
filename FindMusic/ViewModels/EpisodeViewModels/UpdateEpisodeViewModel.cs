using System.ComponentModel.DataAnnotations;

namespace FindMusic.ViewModels.EpisodeViewModels
{
    public class UpdateEpisodeViewModel : CreateEpisodeViewModel
    {
        [Required]
        public int Id { get; set; }
    }
}
