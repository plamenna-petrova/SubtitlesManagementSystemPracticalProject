using SubtitlesManagementSystem.Web.Models.FilmProductions.ViewModels;

namespace SubtitlesManagementSystem.Web.Models.Countries.ViewModels
{
    public class AllCountriesViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<FilmProductionConciseInformationViewModel> RelatedFilmProductions { get; set; }
    }
}
