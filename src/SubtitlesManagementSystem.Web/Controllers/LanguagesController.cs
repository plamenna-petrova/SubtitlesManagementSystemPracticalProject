﻿using Microsoft.AspNetCore.Mvc;
using Data.DataModels.Entities;
using Microsoft.AspNetCore.Authorization;
using SubtitlesManagementSystem.Business.Transactions.Interfaces;
using SubtitlesManagementSystem.Common.GlobalConstants;
using SubtitlesManagementSystem.Web.Models.Languages.BindingModels;
using SubtitlesManagementSystem.Web.Models.Languages.ViewModels;
using System.Data;
using System.Security.Claims;
using SubtitlesManagementSystem.Business.Services.Languages;
using SubtitlesManagementSystem.Common.Helpers;

namespace SubtitlesManagementSystem.Web.Controllers
{
    public class LanguagesController : BaseController
    {
        private readonly ILanguageService _languageService;

        private readonly IUnitOfWork _unitOfWork;

        private readonly ILogger _logger;

        public LanguagesController(
            ILanguageService languageService,
            IUnitOfWork unitOfWork,
            ILogger<LanguagesController> logger
        )
        {
            _languageService = languageService;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        [Authorize(Roles = "Administrator, Editor")]
        public IActionResult Index(string sortOrder, string currentFilter, string searchTerm, int? pageSize, int? pageNumber)
        {
            IEnumerable<AllLanguagesViewModel> allLanguagesViewModel = _languageService.GetAllLanguagesWithRelatedData();

            ViewData["CurrentSort"] = sortOrder;
            ViewData["LanguageNameSort"] = string.IsNullOrEmpty(sortOrder)
                ? "language_name_descending"
                : "";

            if (searchTerm != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchTerm = currentFilter;
            }

            ViewData["LanguageSearchFilter"] = searchTerm;

            if (!string.IsNullOrEmpty(searchTerm))
            {
                allLanguagesViewModel = allLanguagesViewModel
                        .Where(alvm =>
                            alvm.Name.ToLower().Contains(searchTerm.ToLower())
                        );
            }

            allLanguagesViewModel = sortOrder switch
            {
                "language_name_descending" => allLanguagesViewModel
                        .OrderByDescending(alvm => alvm.Name),
                _ => allLanguagesViewModel.OrderBy(alvm => alvm.Name)
            };

            pageSize ??= 3;

            ViewData["CurrentPageSize"] = pageSize;

            var languagesPaginatedList = PaginatedList<AllLanguagesViewModel>
                .Create(allLanguagesViewModel, pageNumber ?? 1, (int)pageSize);

            return View(languagesPaginatedList);
        }

        [Authorize(Roles = "Administrator, Editor")]
        public IActionResult Details(string id)
        {
            LanguageDetailsViewModel languageDetailsViewModel = _languageService.GetLanguageDetails(id);

            if (languageDetailsViewModel == null)
            {
                return NotFound();
            }

            return View(languageDetailsViewModel);
        }

        [Authorize(Roles = "Administrator, Editor")]
        public ViewResult Create()
        {
            return View(new CreateLanguageBindingModel());
        }

        [HttpPost]
        [Authorize(Roles = "Administrator, Editor")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateLanguageBindingModel createLanguageBindingModel)
        {
            if (!ModelState.IsValid)
            {
                return View(createLanguageBindingModel);
            }

            bool isNewLanguageCreated = _languageService.CreateLanguage(
                createLanguageBindingModel, User.FindFirstValue(ClaimTypes.Name)
            );

            if (!isNewLanguageCreated)
            {
                TempData["LanguageErrorMessage"] = string.Format(NotificationMessages
                    .ExistingRecordErrorMessage, "language",
                        createLanguageBindingModel.Name);

                return View(createLanguageBindingModel);
            }

            bool isNewLanguageSavedToDatabase = _unitOfWork.CommitSaveChanges();

            if (!isNewLanguageSavedToDatabase)
            {
                TempData["LanguageErrorMessage"] = string.Format(
                    NotificationMessages.NewRecordFailedSaveErrorMessage,
                    "language");

                return View(createLanguageBindingModel);
            }

            TempData["LanguageSuccessMessage"] = string.Format(
                NotificationMessages.RecordCreationSuccessMessage,
                "Language", $"{createLanguageBindingModel.Name}");

            return RedirectToIndexActionInCurrentController();
        }

        [Authorize(Roles = "Administrator, Editor")]
        public IActionResult Edit(string id)
        {
            EditLanguageBindingModel editLanguageBindingModel = _languageService.GetLanguageEditingDetails(id);

            if (editLanguageBindingModel == null)
            {
                return NotFound();
            }

            return View(editLanguageBindingModel);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator, Editor")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditLanguageBindingModel editLanguageBindingModel)
        {
            if (!ModelState.IsValid)
            {
                return View(editLanguageBindingModel);
            }

            bool isCurrentLanguageEdited = _languageService.EditLanguage(
                editLanguageBindingModel, User.FindFirstValue(ClaimTypes.Name)
            );

            if (!isCurrentLanguageEdited)
            {
                TempData["LanguageErrorMessage"] = string.Format(NotificationMessages
                    .ExistingRecordErrorMessage, "language",
                        editLanguageBindingModel.Name);

                return View(editLanguageBindingModel);
            }

            bool isCurrentLanguageUpdateSavedToDatabase = _unitOfWork.CommitSaveChanges();

            if (!isCurrentLanguageUpdateSavedToDatabase)
            {
                TempData["LanguageErrorMessage"] = string.Format(
                    NotificationMessages.RecordFailedUpdateSaveErrorMessage,
                        "language");

                return View(editLanguageBindingModel);
            }

            TempData["LanguageSuccessMessage"] = string.Format(NotificationMessages
                .RecordUpdateSuccessMessage, "Language",
                $"{editLanguageBindingModel.Name}");

            return RedirectToIndexActionInCurrentController();
        }

        [Authorize(Roles = IdentityConstants.AdministratorRoleName)]
        public IActionResult Delete(string id)
        {
            DeleteLanguageViewModel deleteLanguageViewModel = _languageService.GetLanguageDeletionDetails(id);

            if (deleteLanguageViewModel == null)
            {
                return NotFound();
            }

            return View(deleteLanguageViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = IdentityConstants.AdministratorRoleName)]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmDeletion(string id)
        {
            Language languageToConfirmDeletion = _languageService.FindLanguage(id);

            _languageService.DeleteLanguage(languageToConfirmDeletion);

            bool isLanguageDeleted = _unitOfWork.CommitSaveChanges();

            if (!isLanguageDeleted)
            {
                string failedDeletionMessage = NotificationMessages
                    .RecordFailedDeletionErrorMessage;

                TempData["LanguageErrorMessage"] =
                    string.Format(failedDeletionMessage, "language") +
                    $" {languageToConfirmDeletion.Name}"
                    + "Check the language relationship status!";

                return RedirectToAction(nameof(Delete));
            }

            TempData["LanguageSuccessMessage"] = string.Format(
                NotificationMessages.RecordDeletionSuccessMessage,
                "Language", $"{languageToConfirmDeletion.Name}");

            return RedirectToIndexActionInCurrentController();
        }
    }
}
