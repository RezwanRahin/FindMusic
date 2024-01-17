using System.ComponentModel.DataAnnotations;

namespace FindMusic.ViewModels.TimestampViewModels
{
    public class CreateTimestampViewModel : TimestampFormViewModel
    {
        [Required]
        public string Type { get; set; }

        [Required]
        public ContentDetailsViewModel Content { get; set; }
    }
}
