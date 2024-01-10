using SubtitlesManagementSystem.Web.Models.FilmProductions.ViewModels;

namespace SubtitlesManagementSystem.Web.Models.Languages.ViewModels
{
    public class AllLanguagesViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<FilmProductionConciseInformationViewModel> RelatedFilmProductions { get; set; }
    }
}
