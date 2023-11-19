namespace FindMusic.ViewModels
{
	public class PosterViewModel
	{
		public virtual string PhotoPath { get; set; }

		public PosterViewModel()
		{

		}

		public PosterViewModel(string? photoPath)
		{
			PhotoPath = GetPhoto(photoPath);
		}

		public static string GetPhoto(string? photoPath)
		{
			var noImage = "noimage.jpg";
			return "~/images/" + (photoPath ?? noImage);
		}
	}
}
