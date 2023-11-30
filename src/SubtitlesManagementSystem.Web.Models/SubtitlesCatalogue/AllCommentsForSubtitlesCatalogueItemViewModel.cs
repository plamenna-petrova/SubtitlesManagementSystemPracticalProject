using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubtitlesManagementSystem.Web.Models.SubtitlesCatalogue
{
    public class AllCommentsForSubtitlesCatalogueItemViewModel
    {
        public string Username { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Content { get; set; }
    }
}
