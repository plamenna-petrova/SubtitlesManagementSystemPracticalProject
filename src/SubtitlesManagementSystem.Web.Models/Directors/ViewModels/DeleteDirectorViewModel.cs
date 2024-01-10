using SubtitlesManagementSystem.Common.GlobalConstants;
using System.ComponentModel;

namespace SubtitlesManagementSystem.Web.Models.Directors.ViewModels
{
    public class DeleteDirectorViewModel
    {
        [DisplayName(DisplayConstants.FirstNameDisplayName)]
        public string FirstName { get; set; }

        [DisplayName(DisplayConstants.LastNameDisplayName)]
        public string LastName { get; set; }
    }
}
