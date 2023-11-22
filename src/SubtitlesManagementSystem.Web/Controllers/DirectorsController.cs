using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Data.DataAccess;
using Data.DataModels.Entities;
using Microsoft.AspNetCore.Authorization;
using SubtitlesManagementSystem.Business.Services.Directors;
using SubtitlesManagementSystem.Business.Transactions.Interfaces;
using SubtitlesManagementSystem.Common.GlobalConstants;
using SubtitlesManagementSystem.Web.Models.Directors.BindingModels;
using SubtitlesManagementSystem.Web.Models.Directors.ViewModels;
using System.Data;
using System.Security.Claims;

namespace SubtitlesManagementSystem.Web.Controllers
{
    public class DirectorsController : BaseController
    {
        private readonly IDirectorService _directorService;

        private readonly IUnitOfWork _unitOfWork;

        public DirectorsController(IDirectorService directorService, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _directorService = directorService;
        }

        [Authorize(Roles = "Administrator, Editor")]
        public IActionResult Index()
        {
            IEnumerable<AllDirectorsViewModel> allDirectorsViewModel = _directorService.GetAllDirectors();

            return View(allDirectorsViewModel);
        }

        [Authorize(Roles = "Administrator, Editor")]
        public IActionResult Details(string id)
        {
            DirectorDetailsViewModel directorDetailsViewModel = _directorService
                .GetDirectorDetails(id);

            if (directorDetailsViewModel == null)
            {
                return NotFound();
            }

            return View(directorDetailsViewModel);
        }

        [Authorize(Roles = "Administrator, Editor")]
        public ViewResult Create()
        {
            return View(_directorService.GetDirectorCreatingDetails());
        }

        [HttpPost]
        [Authorize(Roles = "Administrator, Editor")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(
            CreateDirectorBindingModel createDirectorBindingModel,
            string[] selectedFilmProductions
         )
        {
            if (!ModelState.IsValid)
            {
                return View(createDirectorBindingModel);
            }

            bool isNewDirectorCreated = _directorService.CreateDirector(
                createDirectorBindingModel, selectedFilmProductions, User.FindFirstValue(ClaimTypes.Name)
            );

            if (!isNewDirectorCreated)
            {
                TempData["DirectorErrorMessage"] = string.Format(
                        NotificationMessages.ExistingRecordErrorMessage,
                        "director", $"{createDirectorBindingModel.FirstName} " +
                        $"{createDirectorBindingModel.LastName}"
                    );

                return View(createDirectorBindingModel);
            }

            bool isNewDirectorSavedToDatabase = _unitOfWork.CommitSaveChanges();

            if (!isNewDirectorSavedToDatabase)
            {
                TempData["DirectorErrorMessage"] = string.Format(
                    NotificationMessages.NewRecordFailedSaveErrorMessage, "director"
                );

                return View(createDirectorBindingModel);
            }

            TempData["DirectorSuccessMessage"] = string.Format(
                    NotificationMessages.RecordCreationSuccessMessage,
                    "Director", $"{createDirectorBindingModel.FirstName} " +
                    $"{createDirectorBindingModel.LastName}"
                );

            return RedirectToIndexActionInCurrentController();
        }

        [Authorize(Roles = "Administrator, Editor")]
        public IActionResult Edit(string id)
        {
            EditDirectorBindingModel editDirectorBindingModel = _directorService
                .GetDirectorEditingDetails(id);

            if (editDirectorBindingModel == null)
            {
                return NotFound();
            }

            return View(editDirectorBindingModel);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator, Editor")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(
            EditDirectorBindingModel editDirectorBindingModel,
            string[] selectedFilmProductions
        )
        {
            if (!ModelState.IsValid)
            {
                return View(editDirectorBindingModel);
            }

            bool isCurrentDirectorEdited = _directorService.EditDirector(
                editDirectorBindingModel, selectedFilmProductions, User.FindFirstValue(ClaimTypes.Name)
            );

            if (!isCurrentDirectorEdited)
            {
                TempData["DirectorErrorMessage"] = string.Format(
                        NotificationMessages.ExistingRecordErrorMessage,
                        "director", $"{editDirectorBindingModel.FirstName} " +
                        $"{editDirectorBindingModel.LastName}"
                    );

                return View(editDirectorBindingModel);
            }

            bool isCurrentDirectorUpdateSavedToDatabase = _unitOfWork.CommitSaveChanges();

            if (!isCurrentDirectorUpdateSavedToDatabase)
            {
                TempData["DirectorErrorMessage"] = string.Format(
                    NotificationMessages.RecordFailedUpdateSaveErrorMessage,
                    "director"
                  );

                return View(editDirectorBindingModel);
            }

            TempData["DirectorSuccessMessage"] = string.Format(
                   NotificationMessages.RecordUpdateSuccessMessage,
                   "Director", $"{editDirectorBindingModel.FirstName} " +
                   $"{editDirectorBindingModel.LastName}"
                );

            return RedirectToIndexActionInCurrentController();
        }

        [Authorize(Roles = IdentityConstants.AdministratorRoleName)]
        public IActionResult Delete(string id)
        {
            DeleteDirectorViewModel deleteDirectorViewModel = _directorService
                .GetDirectorDeletionDetails(id);

            if (deleteDirectorViewModel == null)
            {
                return NotFound();
            }

            return View(deleteDirectorViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = IdentityConstants.AdministratorRoleName)]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmDeletion(string id)
        {
            Director directorToConfirmDeletion = _directorService.FindDirector(id);

            _directorService.DeleteDirector(directorToConfirmDeletion);

            bool isDirectorDeleted = _unitOfWork.CommitSaveChanges();

            if (!isDirectorDeleted)
            {
                TempData["DirectorErrorMessage"] = string.Format(
                    NotificationMessages.RecordFailedDeletionErrorMessage,
                    "director"
                   ) + $" {directorToConfirmDeletion.FirstName} " +
                   $"{directorToConfirmDeletion.LastName}";

                return RedirectToAction(nameof(Delete));
            }

            TempData["DirectorSuccessMessage"] = string.Format(
                    NotificationMessages.RecordDeletionSuccessMessage,
                    "Director", $"{directorToConfirmDeletion.FirstName} " +
                    $"{directorToConfirmDeletion.LastName}"
                  );

            return RedirectToIndexActionInCurrentController();
        }
    }
}
