using SubtitlesManagementSystem.Common.GlobalConstants;
using System.ComponentModel.DataAnnotations;

namespace SubtitlesManagementSystem.Web.Models.Genres.BindingModels
{
    public class CreateGenreBindingModel
    {
        [Required]
        [StringLength(15, MinimumLength = 5,
            ErrorMessage = ValidationConstants.GenreNameMinimumLengthValidationMessage)]
        public string Name { get; set; }
    }
}
