namespace FindMusic.ViewModels.SeriesViewModels
{
	public class SeriesContentViewModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Slug { get; set; }

		public ContributorViewModel? Contributor { get; set; }
		public PosterViewModel Poster { get; set; }
	}
}
