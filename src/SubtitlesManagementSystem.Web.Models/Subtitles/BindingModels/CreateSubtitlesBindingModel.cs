using Microsoft.AspNetCore.Http;

namespace SubtitlesManagementSystem.Web.Models.Subtitles.BindingModels
{
    public class CreateSubtitlesBindingModel
    {
        public string? Name { get; set; } = null;

        public string FilmProductionId { get; set; }

        public List<IFormFile> Files { get; set; }
    }
}
