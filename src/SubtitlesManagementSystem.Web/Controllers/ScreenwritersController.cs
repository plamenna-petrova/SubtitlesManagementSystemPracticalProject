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
using SubtitlesManagementSystem.Business.Services.Screenwriters;
using SubtitlesManagementSystem.Business.Transactions.Interfaces;
using SubtitlesManagementSystem.Common.GlobalConstants;
using SubtitlesManagementSystem.Web.Models.Screenwriters.BindingModels;
using SubtitlesManagementSystem.Web.Models.Screenwriters.ViewModels;
using System.Data;
using System.Security.Claims;

namespace SubtitlesManagementSystem.Web.Controllers
{
    public class ScreenwritersController : BaseController
    {
        private readonly IScreenwriterService _screenwriterService;

        private readonly IUnitOfWork _unitOfWork;

        public ScreenwritersController(
            IScreenwriterService screenwriterService,
            IUnitOfWork unitOfWork
        )
        {
            _unitOfWork = unitOfWork;
            _screenwriterService = screenwriterService;
        }

        [Authorize(Roles = "Administrator, Editor")]
        public IActionResult Index()
        {
            IEnumerable<AllScreenwritersViewModel> allScreenwritersViewModel = _screenwriterService.GetAllScreenwriters();

            return View(allScreenwritersViewModel);
        }

        [Authorize(Roles = "Administrator, Editor")]
        public IActionResult Details(string id)
        {
            ScreenwriterDetailsViewModel screenwriterDetailsViewModel = _screenwriterService
                .GetScreenwriterDetails(id);

            if (screenwriterDetailsViewModel == null)
            {
                return NotFound();
            }

            return View(screenwriterDetailsViewModel);
        }

        [Authorize(Roles = "Administrator, Editor")]
        public ViewResult Create()
        {

            return View(_screenwriterService.GetScreenwriterCreatingDetails());
        }

        [HttpPost]
        [Authorize(Roles = "Administrator, Editor")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(
            CreateScreenwriterBindingModel createScreenwriterBindingModel,
            string[] selectedFilmProductions
         )
        {
            if (!ModelState.IsValid)
            {
                return View(createScreenwriterBindingModel);
            }

            bool isNewScreenwriterCreated = _screenwriterService.CreateScreenwriter(
                createScreenwriterBindingModel, selectedFilmProductions, User.FindFirstValue(ClaimTypes.Name)
            );

            if (!isNewScreenwriterCreated)
            {
                TempData["ScreenwriterErrorMessage"] = string.Format(
                        NotificationMessages.ExistingRecordErrorMessage,
                        "screenwriter", $"{createScreenwriterBindingModel.FirstName} " +
                        $"{createScreenwriterBindingModel.LastName}"
                    );

                return View(createScreenwriterBindingModel);
            }

            bool isNewScreenwriterSavedToDatabase = _unitOfWork.CommitSaveChanges();

            if (!isNewScreenwriterSavedToDatabase)
            {
                TempData["ScreenwriterErrorMessage"] = string.Format(
                    NotificationMessages.NewRecordFailedSaveErrorMessage, "screenwriter"
                );

                return View(createScreenwriterBindingModel);
            }

            TempData["ScreenwriterSuccessMessage"] = string.Format(
                    NotificationMessages.RecordCreationSuccessMessage,
                    "Screenwriter", $"{createScreenwriterBindingModel.FirstName} " +
                    $"{createScreenwriterBindingModel.LastName}"
                );

            return RedirectToIndexActionInCurrentController();
        }

        [Authorize(Roles = "Administrator, Editor")]
        public IActionResult Edit(string id)
        {
            EditScreenwriterBindingModel editScreenwriterBindingModel = _screenwriterService
                .GetScreenwriterEditingDetails(id);

            if (editScreenwriterBindingModel == null)
            {
                return NotFound();
            }

            return View(editScreenwriterBindingModel);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator, Editor")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(
            EditScreenwriterBindingModel editScreenwriterBindingModel,
            string[] selectedFilmProductions
        )
        {
            if (!ModelState.IsValid)
            {
                return View(editScreenwriterBindingModel);
            }

            bool isCurrentScreenwriterEdited = _screenwriterService.EditScreenwriter(
                editScreenwriterBindingModel, selectedFilmProductions, User.FindFirstValue(ClaimTypes.Name)
            );

            if (!isCurrentScreenwriterEdited)
            {
                TempData["ScreenwriterErrorMessage"] = string.Format(
                        NotificationMessages.ExistingRecordErrorMessage,
                        "screenwriter", $"{editScreenwriterBindingModel.FirstName} " +
                        $"{editScreenwriterBindingModel.LastName}"
                    );

                return View(editScreenwriterBindingModel);
            }

            bool isCurrentScreenwriterUpdateSavedToDatabase = _unitOfWork.CommitSaveChanges();

            if (!isCurrentScreenwriterUpdateSavedToDatabase)
            {
                TempData["ScreenwriterErrorMessage"] = string.Format(
                    NotificationMessages.RecordFailedUpdateSaveErrorMessage,
                    "screenwriter"
                  );

                return View(editScreenwriterBindingModel);
            }

            TempData["ScreenwriterSuccessMessage"] = string.Format(
                   NotificationMessages.RecordUpdateSuccessMessage,
                   "Screenwriter", $"{editScreenwriterBindingModel.FirstName} " +
                   $"{editScreenwriterBindingModel.LastName}"
                );

            return RedirectToIndexActionInCurrentController();
        }

        [Authorize(Roles = IdentityConstants.AdministratorRoleName)]
        public IActionResult Delete(string id)
        {
            DeleteScreenwriterViewModel deleteScreenwriterViewModel = _screenwriterService
                .GetScreenwriterDeletionDetails(id);

            if (deleteScreenwriterViewModel == null)
            {
                return NotFound();
            }

            return View(deleteScreenwriterViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = IdentityConstants.AdministratorRoleName)]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmDeletion(string id)
        {
            Screenwriter screenwriterToConfirmDeletion = _screenwriterService.FindScreenwriter(id);

            _screenwriterService.DeleteScreenwriter(screenwriterToConfirmDeletion);

            bool isScreenwriterDeleted = _unitOfWork.CommitSaveChanges();

            if (!isScreenwriterDeleted)
            {
                TempData["ScreenwriterErrorMessage"] = string.Format(
                    NotificationMessages.RecordFailedDeletionErrorMessage,
                    "screenwriter"
                   ) + $" {screenwriterToConfirmDeletion.FirstName} " +
                   $"{screenwriterToConfirmDeletion.LastName}";

                return RedirectToAction(nameof(Delete));
            }

            TempData["ScreenwriterSuccessMessage"] = string.Format(
                    NotificationMessages.RecordDeletionSuccessMessage,
                    "Screenwriter", $"{screenwriterToConfirmDeletion.FirstName} " +
                    $"{screenwriterToConfirmDeletion.LastName}"
                  );

            return RedirectToIndexActionInCurrentController();
        }
    }
}
