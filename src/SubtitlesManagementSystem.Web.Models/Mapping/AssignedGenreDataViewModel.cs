using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubtitlesManagementSystem.Web.Models.Mapping
{
    public class AssignedGenreDataViewModel
    {
        public string GenreId { get; set; }

        public string Name { get; set; }

        public bool IsAssigned { get; set; }
    }
}
