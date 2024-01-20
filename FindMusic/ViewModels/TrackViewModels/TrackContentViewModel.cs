namespace FindMusic.ViewModels.TrackViewModels
{
    public class TrackContentViewModel
    {
        public virtual string Title { get; set; }
        public virtual string Url { get; set; }

        public ContributorViewModel Contributor { get; set; }
    }
}
