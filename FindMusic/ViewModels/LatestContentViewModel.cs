namespace FindMusic.ViewModels
{
    public class LatestContentViewModel
    {
        public ICollection<CardViewModel> Movies { get; set; }
        public ICollection<CardViewModel> Series { get; set; }
    }
}
