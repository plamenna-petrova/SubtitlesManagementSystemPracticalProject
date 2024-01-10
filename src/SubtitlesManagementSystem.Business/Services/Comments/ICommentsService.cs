using SubtitlesManagementSystem.Web.Models.Comments.BindingModels;
using SubtitlesManagementSystem.Web.Models.SubtitlesCatalogue;

namespace SubtitlesManagementSystem.Business.Services.Comments
{
    public interface ICommentService
    {
        bool CreateComment(CreateCommentBindingModel createCommentBindingModel, string subtitlesId, string userId);

        IEnumerable<AllCommentsForSubtitlesCatalogueItemViewModel> GetAllCommentsForSubtitlesCatalogueItem(string subtitlesId);
    }
}
