using Microsoft.AspNetCore.Mvc;
using Data.DataModels.Entities;
using Microsoft.AspNetCore.Authorization;
using SubtitlesManagementSystem.Business.Services.Genres;
using SubtitlesManagementSystem.Business.Transactions.Interfaces;
using SubtitlesManagementSystem.Common.GlobalConstants;
using SubtitlesManagementSystem.Web.Models.Genres.BindingModels;
using SubtitlesManagementSystem.Web.Models.Genres.ViewModels;
using System.Security.Claims;

namespace SubtitlesManagementSystem.Web.Controllers
{
    public class GenresController : BaseController
    {
        private readonly IGenreService _genreService;

        private readonly IUnitOfWork _unitOfWork;

        public GenresController(IGenreService genreService, IUnitOfWork unitOfWork)
        {
            _genreService = genreService;
            _unitOfWork = unitOfWork;
        }

        [Authorize(Roles = "Administrator, Editor")]
        public IActionResult Index()
        {
            IEnumerable<AllGenresViewModel> allGenresViewModel = _genreService.GetAllGenres();

            return View(allGenresViewModel);
        }

        [Authorize(Roles = "Administrator, Editor")]
        public IActionResult Details(string id)
        {
            GenreDetailsViewModel genreDetailsViewModel = _genreService.GetGenreDetails(id);

            if (genreDetailsViewModel == null)
            {
                return NotFound();
            }

            return View(genreDetailsViewModel);
        }

        [Authorize(Roles = "Administrator, Editor")]
        public ViewResult Create()
        {
            return View(new CreateGenreBindingModel());
        }

        [HttpPost]
        [Authorize(Roles = "Administrator, Editor")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateGenreBindingModel createGenreBindingModel)
        {
            if (!ModelState.IsValid)
            {
                return View(createGenreBindingModel);
            }

            bool isNewGenreCreated = _genreService.CreateGenre(
                createGenreBindingModel, User.FindFirstValue(ClaimTypes.Name)
            );

            if (!isNewGenreCreated)
            {
                TempData["GenreErrorMessage"] = string.Format(NotificationMessages
                    .ExistingRecordErrorMessage, "genre",
                        createGenreBindingModel.Name);

                return View(createGenreBindingModel);
            }

            bool isNewGenreSavedToDatabase = _unitOfWork.CommitSaveChanges();

            if (!isNewGenreSavedToDatabase)
            {
                TempData["GenreErrorMessage"] = string.Format(
                    NotificationMessages.NewRecordFailedSaveErrorMessage,
                    "genre");

                return View(createGenreBindingModel);
            }

            TempData["GenreSuccessMessage"] = string.Format(
                NotificationMessages.RecordCreationSuccessMessage,
                "Genre", $"{createGenreBindingModel.Name}");

            return RedirectToIndexActionInCurrentController();
        }

        [Authorize(Roles = "Administrator, Editor")]
        public IActionResult Edit(string id)
        {
            EditGenreBindingModel editGenreBindingModel = _genreService.GetGenreEditingDetails(id);

            if (editGenreBindingModel == null)
            {
                return NotFound();
            }

            return View(editGenreBindingModel);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator, Editor")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditGenreBindingModel editGenreBindingModel)
        {
            if (!ModelState.IsValid)
            {
                return View(editGenreBindingModel);
            }

            bool isCurrentGenreEdited = _genreService.EditGenre(
                editGenreBindingModel, User.FindFirstValue(ClaimTypes.Name)
            );

            if (!isCurrentGenreEdited)
            {
                TempData["GenreErrorMessage"] = string.Format(NotificationMessages
                    .ExistingRecordErrorMessage, "genre",
                        editGenreBindingModel.Name);

                return View(editGenreBindingModel);
            }

            bool isCurrentGenreUpdateSavedToDatabase = _unitOfWork.CommitSaveChanges();

            if (!isCurrentGenreUpdateSavedToDatabase)
            {
                TempData["GenreErrorMessage"] = string.Format(
                    NotificationMessages.RecordFailedUpdateSaveErrorMessage,
                "genre");

                return View(editGenreBindingModel);
            }

            TempData["GenreSuccessMessage"] = string.Format(NotificationMessages
                .RecordUpdateSuccessMessage, "Genre",
                $"{editGenreBindingModel.Name}");

            return RedirectToIndexActionInCurrentController();
        }

        [Authorize(Roles = IdentityConstants.AdministratorRoleName)]
        public IActionResult Delete(string id)
        {
            DeleteGenreViewModel deleteGenreViewModel = _genreService.GetGenreDeletionDetails(id);

            if (deleteGenreViewModel == null)
            {
                return NotFound();
            }

            return View(deleteGenreViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = IdentityConstants.AdministratorRoleName)]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmDeletion(string id)
        {
            Genre genreToConfirmDeletion = _genreService.FindGenre(id);

            _genreService.DeleteGenre(genreToConfirmDeletion);

            bool isGenreDeleted = _unitOfWork.CommitSaveChanges();

            if (!isGenreDeleted)
            {
                TempData["GenreErrorMessage"] =
                    string.Format(NotificationMessages
                    .RecordFailedDeletionErrorMessage, "genre") +
                    $" {genreToConfirmDeletion.Name}";

                return RedirectToAction(nameof(Delete));
            }

            TempData["GenreSuccessMessage"] = string.Format(
                NotificationMessages.RecordDeletionSuccessMessage,
                "Genre", $"{genreToConfirmDeletion.Name}");

            return RedirectToIndexActionInCurrentController();
        }
    }
}
