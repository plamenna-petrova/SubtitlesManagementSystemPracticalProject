using SubtitlesManagementSystem.Common.GlobalConstants;
using System.ComponentModel.DataAnnotations;

namespace SubtitlesManagementSystem.Web.Models.Languages.BindingModels
{
    public class CreateLanguageBindingModel
    {
        [Required]
        [StringLength(15, MinimumLength = 5,
            ErrorMessage = ValidationConstants.LanguageNameMinimumLengthValidationMessage)]
        public string Name { get; set; }
    }
}
