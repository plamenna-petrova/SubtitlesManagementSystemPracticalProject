using SubtitlesManagementSystem.Web.Models.SubtitlesCatalogue;

namespace SubtitlesManagementSystem.Business.Services.SubtitlesCatalogue
{
    public interface ISubtitlesCatalogueService
    {
        CatalogueItemsViewModel GetAllSubtitlesForCatalogue();

        SubtitlesCatalogueItemDetailsViewModel GetSubtitlesCatalogueItemDetails(string subtitlesId);
    }
}
