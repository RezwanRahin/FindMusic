namespace FindMusic.ViewModels.SeriesViewModels
{
	public class UpdateSeriesViewModel : SeriesDetailsViewModel
	{
		public string? Slug { get; set; }
		public string? PhotoPath { get; set; }
		public IFormFile? Photo { get; set; }
	}
}
