using SubtitlesManagementSystem.Web.Models.SubtitlesCatalogue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubtitlesManagementSystem.Business.Services.SubtitlesCatalogue
{
    public interface ISubtitlesCatalogueService
    {
        CatalogueItemsViewModel GetAllSubtitlesForCatalogue();

        SubtitlesCatalogueItemDetailsViewModel GetSubtitlesCatalogueItemDetails(string subtitlesId);
    }
}
