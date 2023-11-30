using SubtitlesManagementSystem.Web.Models.Comments.BindingModels;
using SubtitlesManagementSystem.Web.Models.SubtitlesCatalogue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubtitlesManagementSystem.Business.Services.Comments
{
    public interface ICommentService
    {
        bool CreateComment(CreateCommentBindingModel createCommentBindingModel, string subtitlesId, string userId);

        IEnumerable<AllCommentsForSubtitlesCatalogueItemViewModel> GetAllCommentsForSubtitlesCatalogueItem(string subtitlesId);
    }
}
