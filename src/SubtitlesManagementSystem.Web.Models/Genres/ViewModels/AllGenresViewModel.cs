using SubtitlesManagementSystem.Web.Models.FilmProductions.ViewModels;

namespace SubtitlesManagementSystem.Web.Models.Genres.ViewModels
{
    public class AllGenresViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<FilmProductionConciseInformationViewModel> RelatedFilmProductions { get; set; }
    }
}
