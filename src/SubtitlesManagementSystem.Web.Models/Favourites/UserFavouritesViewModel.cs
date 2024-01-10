using SubtitlesManagementSystem.Web.Models.FilmProductions.ViewModels;

namespace SubtitlesManagementSystem.Web.Models.Favourites
{
    public class UserFavouritesViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string UploaderUserName { get; set; }

        public FilmProductionConciseInformationViewModel RelatedFilmProduction { get; set; }
    }
}
