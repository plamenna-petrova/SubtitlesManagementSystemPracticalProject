using Data.DataModels.Entities;
using SubtitlesManagementSystem.Web.Models.Subtitles.BindingModels;
using SubtitlesManagementSystem.Web.Models.Subtitles.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubtitlesManagementSystem.Business.Services.Subtitles
{
    public interface ISubtitlesService
    {
        List<Data.DataModels.Entities.Subtitles> GetAllToList();

        IEnumerable<AllSubtitlesViewModel> GetAllSubtitles();

        SubtitlesDetailsViewModel GetSubtitlesDetails(string subtitlesId);

        bool CreateSubtitles(CreateSubtitlesBindingModel createSubtitlesBindingModel, string userId);

        EditSubtitlesBindingModel GetSubtitlesEditingDetails(string subtitlesId);

        bool EditSubtitles(EditSubtitlesBindingModel editSubtitlesBindingModel, string userId);

        DeleteSubtitlesViewModel GetSubtitlesDeletionDetails(string subtitlesId);

        void DeleteSubtitles(Data.DataModels.Entities.Subtitles subtitles);

        List<SubtitlesFiles> GetSubtitlesFilesBySubtitlesId(string id);

        Data.DataModels.Entities.Subtitles FindSubtitles(string subtielsId);
    }
}
