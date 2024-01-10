using SubtitlesManagementSystem.Web.Models.Comments.ViewModels;
using SubtitlesManagementSystem.Web.Models.Favourites;

namespace SubtitlesManagementSystem.Web.Models.SubtitlesCatalogue
{
    public class CatalogueItemsViewModel
    {
        public IEnumerable<AllSubtitlesForCatalogueViewModel> AllSubtitlesForCatalogue { get; set; }

        public IEnumerable<LatestCommentViewModel> LatestComments { get; set; }

        public IEnumerable<TopSubtitlesViewModel> TopSubtitles { get; set; }
    }
}
