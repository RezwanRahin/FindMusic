namespace FindMusic.ViewModels.SeasonViewModels
{
    public class SeasonContentViewModel
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public int Year { get; set; }

        public ContributorViewModel? Contributor { get; set; }
        public RelatedSeriesViewModel Series { get; set; }
    }
}
