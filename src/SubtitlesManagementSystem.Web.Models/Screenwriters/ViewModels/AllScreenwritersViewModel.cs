using SubtitlesManagementSystem.Common.GlobalConstants;
using SubtitlesManagementSystem.Web.Models.FilmProductions.ViewModels;
using System.ComponentModel;

namespace SubtitlesManagementSystem.Web.Models.Screenwriters.ViewModels
{
    public class AllScreenwritersViewModel
    {
        public string Id { get; set; }

        [DisplayName(DisplayConstants.FirstNameDisplayName)]
        public string FirstName { get; set; }

        [DisplayName(DisplayConstants.LastNameDisplayName)]
        public string LastName { get; set; }

        public IEnumerable<FilmProductionConciseInformationViewModel> RelatedFilmProductions { get; set; }
    }
}
