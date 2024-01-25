using System.ComponentModel.DataAnnotations;
using FindMusic.Models;

namespace FindMusic.Utilities
{
    public class ExclusiveForeignKeyAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var model = (Timestamp)validationContext.ObjectInstance;

            if ((model.EpisodeId == null && model.MovieId == null) || (model.EpisodeId != null && model.MovieId != null))
            {
                return new ValidationResult("Timestamp must not refer both Episode & Movie.");
            }

            return ValidationResult.Success;
        }
    }
}