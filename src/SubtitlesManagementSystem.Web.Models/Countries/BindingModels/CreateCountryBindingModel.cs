using SubtitlesManagementSystem.Common.GlobalConstants;
using System.ComponentModel.DataAnnotations;

namespace SubtitlesManagementSystem.Web.Models.Countries.BindingModels
{
    public class CreateCountryBindingModel
    {
        [Required]
        [StringLength(20, MinimumLength = 2,
            ErrorMessage = ValidationConstants.CountryNameMinimumLengthValidationMessage)]
        public string Name { get; set; }
    }
}
