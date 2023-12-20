using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubtitlesManagementSystem.Web.Models.SubtitlesCatalogue
{
    public class AllSubtitlesForCatalogueViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string UploaderUserName { get; set; }

        public DateTime? UploadedOn { get; set; }

        public FilmProductionForCatalogueViewModel RelatedFilmProduction { get; set; }
    }
}
