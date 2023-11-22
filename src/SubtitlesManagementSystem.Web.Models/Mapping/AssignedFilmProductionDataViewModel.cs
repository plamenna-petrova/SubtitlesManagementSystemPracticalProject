using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubtitlesManagementSystem.Web.Models.Mapping
{
    public class AssignedFilmProductionDataViewModel
    {
        public string FilmProductionId { get; set; }

        public string Title { get; set; }

        public bool IsAssigned { get; set; }
    }
}
