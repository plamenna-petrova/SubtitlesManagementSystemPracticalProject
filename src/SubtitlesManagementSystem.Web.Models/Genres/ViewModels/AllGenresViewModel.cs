using SubtitlesManagementSystem.Web.Models.FilmProductions.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubtitlesManagementSystem.Web.Models.Genres.ViewModels
{
    public class AllGenresViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<FilmProductionConciseInformationViewModel> RelatedFilmProductions { get; set; }
    }
}
