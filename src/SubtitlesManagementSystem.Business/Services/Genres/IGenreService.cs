using Data.DataModels.Entities;
using SubtitlesManagementSystem.Web.Models.Genres.BindingModels;
using SubtitlesManagementSystem.Web.Models.Genres.ViewModels;

namespace SubtitlesManagementSystem.Business.Services.Genres
{
    public interface IGenreService
    {
        IEnumerable<AllGenresViewModel> GetAllGenres();

        GenreDetailsViewModel GetGenreDetails(string genreId);

        bool CreateGenre(CreateGenreBindingModel createGenreBindingModel, string currentUserName);

        EditGenreBindingModel GetGenreEditingDetails(string genreId);

        bool EditGenre(EditGenreBindingModel editGenreBindingModel, string currentUserName);

        DeleteGenreViewModel GetGenreDeletionDetails(string genreId);

        void DeleteGenre(Genre genre);

        Genre FindGenre(string genreId);
    }
}
