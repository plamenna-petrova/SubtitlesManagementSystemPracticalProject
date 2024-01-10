using SubtitlesManagementSystem.Web.Models.FilmProductions.ViewModels;

namespace SubtitlesManagementSystem.Web.Models.Subtitles.ViewModels
{
    public class AllSubtitlesViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public FilmProductionConciseInformationViewModel RelatedFilmProduction { get; set; }
    }
}
