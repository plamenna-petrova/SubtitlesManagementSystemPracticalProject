using SubtitlesManagementSystem.Common.GlobalConstants;
using SubtitlesManagementSystem.Web.Models.Mapping;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SubtitlesManagementSystem.Web.Models.Screenwriters.BindingModels
{
    public class CreateScreenwriterBindingModel
    {
        [Required]
        [StringLength(25, MinimumLength = 2,
            ErrorMessage = ValidationConstants.ScreenwriterFirstNameMinimumLengthValidationMessage)]
        [DisplayName(DisplayConstants.FirstNameDisplayName)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(25, MinimumLength = 2,
            ErrorMessage = ValidationConstants.ScreenwriterLastNameMinimumLengthValidationMessage)]
        [DisplayName(DisplayConstants.LastNameDisplayName)]
        public string LastName { get; set; }

        public IEnumerable<AssignedFilmProductionDataViewModel>? AssignedFilmProductions { get; set; }
    }
}
