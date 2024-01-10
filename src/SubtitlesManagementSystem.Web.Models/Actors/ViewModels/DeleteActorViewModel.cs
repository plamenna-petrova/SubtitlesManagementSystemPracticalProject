using SubtitlesManagementSystem.Common.GlobalConstants;
using System.ComponentModel;

namespace SubtitlesManagementSystem.Web.Models.Actors.ViewModels
{
    public class DeleteActorViewModel
    {
        [DisplayName(DisplayConstants.FirstNameDisplayName)]
        public string FirstName { get; set; }

        [DisplayName(DisplayConstants.LastNameDisplayName)]
        public string LastName { get; set; }
    }
}
