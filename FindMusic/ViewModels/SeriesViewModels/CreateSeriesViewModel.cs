using System.ComponentModel.DataAnnotations;

namespace FindMusic.ViewModels.SeriesViewModels
{
	public class CreateSeriesViewModel
	{
		[Required]
		public string Name { get; set; }

		[Required]
		public IFormFile Photo { get; set; }
	}
}
