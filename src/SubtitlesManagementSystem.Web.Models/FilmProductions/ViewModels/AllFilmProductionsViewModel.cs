using SubtitlesManagementSystem.Web.Models.Countries.ViewModels;
using SubtitlesManagementSystem.Web.Models.Languages.ViewModels;
using System.ComponentModel;

namespace SubtitlesManagementSystem.Web.Models.FilmProductions.ViewModels
{
    public class AllFilmProductionsViewModel
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public int Duration { get; set; }

        [DisplayName("Release Date")]
        public DateTime ReleaseDate { get; set; }

        [DisplayName("Language")]
        public LanguageConciseInformationViewModel RelatedLanguage { get; set; }

        [DisplayName("Country")]
        public CountryConciseInformationViewModel RelatedCountry { get; set; }
    }
}
