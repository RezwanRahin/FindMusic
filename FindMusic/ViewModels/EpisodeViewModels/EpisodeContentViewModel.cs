namespace FindMusic.ViewModels.EpisodeViewModels
{
    public class EpisodeContentViewModel
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string? Name { get; set; }

        public ContributorViewModel? Contributor { get; set; }
        public RelatedSeriesViewModel Series { get; set; }
        public int SeasonNumber { get; set; }
    }
}
