using Data.DataModels.Entities;
using SubtitlesManagementSystem.Web.Models.Directors.BindingModels;
using SubtitlesManagementSystem.Web.Models.Directors.ViewModels;

namespace SubtitlesManagementSystem.Business.Services.Directors
{
    public interface IDirectorService
    {
        IEnumerable<AllDirectorsViewModel> GetAllDirectors();

        CreateDirectorBindingModel GetDirectorCreatingDetails();

        DirectorDetailsViewModel GetDirectorDetails(string directorId);

        bool CreateDirector(CreateDirectorBindingModel createDirectorBindingModel, string[] selectedFilmProductions, string currentUserName);

        EditDirectorBindingModel GetDirectorEditingDetails(string directorId);

        bool EditDirector(EditDirectorBindingModel editDirectorBindingModel, string[] selectedFilmProductions, string currentUserName);

        DeleteDirectorViewModel GetDirectorDeletionDetails(string directorId);

        void DeleteDirector(Director director);

        Director FindDirector(string directorId);
    }
}
