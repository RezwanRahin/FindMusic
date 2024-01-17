using System.ComponentModel.DataAnnotations;

namespace FindMusic.ViewModels.TimestampViewModels
{
    public class TimestampFormViewModel
    {
        [Required]
        public int Hour { get; set; }

        [Required]
        public int Minute { get; set; }

        [Required]
        public int Second { get; set; }
    }
}
