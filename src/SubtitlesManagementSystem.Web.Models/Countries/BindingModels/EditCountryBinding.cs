using SubtitlesManagementSystem.Common.GlobalConstants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubtitlesManagementSystem.Web.Models.Countries.BindingModels
{
    public class EditCountryBindingModel
    {
        public string Id { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 2,
            ErrorMessage = ValidationConstants.CountryNameMinimumLengthValidationMessage)]
        public string Name { get; set; }
    }
}
