using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubtitlesManagementSystem.Web.Models.Subtitles.BindingModels
{
    public class CreateSubtitlesBindingModel
    {
        public string Name { get; set; }

        public string FilmProductionId { get; set; }

        public List<IFormFile> Files { get; set; }
    }
}
