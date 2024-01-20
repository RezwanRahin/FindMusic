using FindMusic.Models;

namespace FindMusic.ViewModels.TrackViewModels
{
    public class TrackDetailsViewModel : TrackContentViewModel
    {
        public int Id { get; set; }

        public ICollection<Vote> Votes { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
