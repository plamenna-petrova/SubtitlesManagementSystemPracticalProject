using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubtitlesManagementSystem.Web.Models.FilmProductions.ViewModels
{
    public class DeleteFilmProductionViewModel
    {
        public string Title { get; set; }

        [DisplayName("Release Date")]
        public DateTime ReleaseDate { get; set; }

        [DisplayName("Image")]
        public string ImageName { get; set; }
    }
}
