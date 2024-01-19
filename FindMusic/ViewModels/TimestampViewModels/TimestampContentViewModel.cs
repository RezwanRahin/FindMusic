namespace FindMusic.ViewModels.TimestampViewModels
{
    public class TimestampContentViewModel
    {
        public virtual int Hour { get; set; }
        public virtual int Minute { get; set; }
        public virtual int Second { get; set; }

        public ContributorViewModel Contributor { get; set; }

    }
}
