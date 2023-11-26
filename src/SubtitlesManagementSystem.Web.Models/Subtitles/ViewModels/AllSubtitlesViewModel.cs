using SubtitlesManagementSystem.Web.Models.FilmProductions.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubtitlesManagementSystem.Web.Models.Subtitles.ViewModels
{
    public class AllSubtitlesViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public FilmProductionConciseInformationViewModel RelatedFilmProduction { get; set; }
    }
}
