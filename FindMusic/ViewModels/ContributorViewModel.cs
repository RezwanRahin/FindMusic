using FindMusic.Models;

namespace FindMusic.ViewModels
{
    public class ContributorViewModel
    {
        private const string AnonymousName = "Anonymous";
        private const string AnonymousPhoto = "anonymous.png";


        public string Name { get; set; }
        public string PhotoPath { get; set; }

        public ContributorViewModel(ApplicationUser? user)
        {
            if (user == null)
            {
                Name = AnonymousName;
                PhotoPath = "~/images/" + AnonymousPhoto;
            }
            else
            {
                Name = user.UserName;
                PhotoPath = GetPhotoPath(user);
            }
        }

        private static string GetPhotoPath(ApplicationUser user)
        {
            var photo = "male.jpg";
            if (user.Gender == Gender.Female)
            {
                photo = "female.jpg";
            }
            return "~/images/" + (user.PhotoPath ?? photo);
        }
    }
}
