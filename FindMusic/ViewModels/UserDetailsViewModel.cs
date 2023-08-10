using System.ComponentModel.DataAnnotations;
using FindMusic.Models;

namespace FindMusic.ViewModels
{
    public class UserDetailsViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public Gender Gender { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }
        
        public string PhotoPath { get; set; }

        public string GetPhoto(Gender gender, string? photoPath)
        {
            var photo = "male.jpg";
            if (gender == Gender.Female)
            {
                photo = "female.jpg";
            }

            return "~/images/" + (photoPath ?? photo);
        }
    }
}