using System.ComponentModel.DataAnnotations;

namespace FindMusic.ViewModels.TimestampViewModels
{
    public class UpdateTimestampViewModel : CreateTimestampViewModel
    {
        [Required]
        public int Id { get; set; }
    }
}
