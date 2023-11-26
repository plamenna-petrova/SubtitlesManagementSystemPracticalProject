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
using SubtitlesManagementSystem.Business.Services.Countries;
using SubtitlesManagementSystem.Business.Services.FilmProductions;
using SubtitlesManagementSystem.Business.Transactions.Interfaces;
using SubtitlesManagementSystem.Common.GlobalConstants;
using SubtitlesManagementSystem.Web.Models.FilmProductions.BindingModels;
using SubtitlesManagementSystem.Web.Models.FilmProductions.ViewModels;
using System.Data;
using System.Security.Claims;
using SubtitlesManagementSystem.Business.Services.Languages;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SubtitlesManagementSystem.Web.Controllers
{
    public class FilmProductionsController : BaseController
    {
        private readonly IFilmProductionService _filmProductionService;

        private readonly ICountryService _countryService;

        private readonly ILanguageService _languageService;

        private readonly IUnitOfWork _unitOfWork;

        private readonly ILogger _logger;

        public FilmProductionsController(
        IFilmProductionService filmProductionService,
            ICountryService countryService,
            ILanguageService languageService,
            IUnitOfWork unitOfWork,
            ILogger<FilmProductionsController> logger
        )
        {
            _filmProductionService = filmProductionService;
            _countryService = countryService;
            _languageService = languageService;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        [Authorize(Roles = "Administrator, Editor")]
        public IActionResult Index()
        {
            IEnumerable<AllFilmProductionsViewModel> allFilmProductionsViewModel =
                _filmProductionService.GetAllFilmProductionsWithRelatedData();

            return View(allFilmProductionsViewModel);
        }

        [Authorize(Roles = "Administrator, Editor")]
        public IActionResult Details(string id)
        {
            FilmProductionFullDetailsViewModel filmProductionFullDetailsViewModel =
                _filmProductionService
                    .GetFilmProductionDetails(id);

            if (filmProductionFullDetailsViewModel == null)
            {
                return NotFound();
            }

            return View(filmProductionFullDetailsViewModel);
        }

        [Authorize(Roles = "Administrator, Editor")]
        public IActionResult Create()
        {
            var allCountriesForSelectList = _countryService.GetAllCountries();
            var allLanguagesForSelectList = _languageService.GetAllLanguages();

            ViewData["CountryByName"] = new SelectList(
                allCountriesForSelectList, "Id", "Name"
            );
            ViewData["LanguageByName"] = new SelectList(
                allLanguagesForSelectList, "Id", "Name"
            );

            return View(_filmProductionService.GetFilmProductionCreatingDetails());
        }

        [HttpPost]
        [Authorize(Roles = "Administrator, Editor")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateFilmProductionBindingModel
            createFilmProductionBindingModel,
            string[] selectedGenres,
            string[] selectedActors,
            string[] selectedDirectors,
            string[] selectedScreenwriters
        )
        {
            if (!ModelState.IsValid)
            {
                return View(createFilmProductionBindingModel);
            }

            _filmProductionService.CreateFilmProduction(
                createFilmProductionBindingModel,
                selectedGenres,
                selectedActors,
                selectedDirectors,
                selectedScreenwriters,
                User.FindFirstValue(ClaimTypes.Name)
            );

            bool isNewFilmProductionSavedToDatabase = _unitOfWork.CommitSaveChanges();

            if (!isNewFilmProductionSavedToDatabase)
            {
                var allCountriesForSelectList = _countryService.GetAllCountries();
                var allLanguagesForSelectList = _languageService.GetAllLanguages();

                ViewData["CountryByName"] = new SelectList(
                            allCountriesForSelectList, "Id", "Name",
                            createFilmProductionBindingModel.CountryId
                        );
                ViewData["LanguageByName"] = new SelectList(
                            allLanguagesForSelectList, "Id", "Name",
                            createFilmProductionBindingModel.LanguageId
                        );

                TempData["FilmProductionErrorMessage"] = string.Format(
                    NotificationMessages.NewRecordFailedSaveErrorMessage,
                    "film production");

                return View(createFilmProductionBindingModel);
            }

            TempData["FilmProductionSuccessMessage"] = string.Format(
                NotificationMessages.RecordCreationSuccessMessage,
                "Film Production", $"{createFilmProductionBindingModel.Title}");

            return RedirectToIndexActionInCurrentController();
        }

        [Authorize(Roles = "Administrator, Editor")]
        public IActionResult Edit(string id)
        {
            EditFilmProductionBindingModel editFilmProductionBindingModel =
                _filmProductionService
                    .GetFilmProductionEditingDetails(id);

            if (editFilmProductionBindingModel == null)
            {
                return NotFound();
            }

            var allCountriesForSelectList = _countryService.GetAllCountries();
            var allLanguagesForSelectList = _languageService.GetAllLanguages();

            ViewData["CountryByName"] = new SelectList(
                    allCountriesForSelectList, "Id", "Name",
                    editFilmProductionBindingModel.CountryId
                );
            ViewData["LanguageByName"] = new SelectList(
                    allLanguagesForSelectList, "Id", "Name",
                    editFilmProductionBindingModel.LanguageId
                );

            return View(editFilmProductionBindingModel);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator, Editor")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(
            EditFilmProductionBindingModel editFilmProductionBindingModel,
            string[] selectedGenres,
            string[] selectedActors,
            string[] selectedDirectors,
            string[] selectedScreenwriters
        )
        {
            var allCountriesForSelectList = _countryService.GetAllCountries();
            var allLanguagesForSelectList = _languageService.GetAllLanguages();

            if (!ModelState.IsValid)
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);

                ViewData["CountryByName"] = new SelectList(
                        allCountriesForSelectList, "Id", "Name",
                        editFilmProductionBindingModel.CountryId
                    );
                ViewData["LanguageByName"] = new SelectList(
                        allLanguagesForSelectList, "Id", "Name",
                        editFilmProductionBindingModel.LanguageId
                    );

                return View(editFilmProductionBindingModel);
            }

            _filmProductionService.EditFilmProduction(
                editFilmProductionBindingModel,
                selectedGenres,
                selectedActors,
                selectedDirectors,
                selectedScreenwriters,
                User.FindFirstValue(ClaimTypes.Name)
            );

            bool isCurrentFilmProductionSavedToDatabase = _unitOfWork.CommitSaveChanges();

            if (!isCurrentFilmProductionSavedToDatabase)
            {
                ViewData["CountryByName"] = new SelectList(
                        allCountriesForSelectList, "Id", "Name",
                        editFilmProductionBindingModel.CountryId
                    );
                ViewData["LanguageByName"] = new SelectList(
                        allLanguagesForSelectList, "Id", "Name",
                        editFilmProductionBindingModel.LanguageId
                    );

                TempData["FilmProductionErrorMessage"] = string.Format(
                NotificationMessages.RecordFailedUpdateSaveErrorMessage,
                        "film production");

                return View(editFilmProductionBindingModel);
            }

            TempData["FilmProductionSuccessMessage"] = string.Format(NotificationMessages
                .RecordUpdateSuccessMessage, "Film Production",
                $"{editFilmProductionBindingModel.Title}");

            return RedirectToIndexActionInCurrentController();
        }

        [Authorize(Roles = IdentityConstants.AdministratorRoleName)]
        public IActionResult Delete(string id)
        {
            DeleteFilmProductionViewModel deleteFilmProductionViewModel =
                _filmProductionService
                    .GetFilmProductionDeletionDetails(id);

            if (deleteFilmProductionViewModel == null)
            {
                return NotFound();
            }

            return View(deleteFilmProductionViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = IdentityConstants.AdministratorRoleName)]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmDeletion(string id)
        {
            var filmProductionToConfirmDeletion = _filmProductionService
                .FindFilmProduction(id);

            _filmProductionService.DeleteFilmProduction(filmProductionToConfirmDeletion);

            bool isFilmProductionDeleted = _unitOfWork.CommitSaveChanges();

            if (!isFilmProductionDeleted)
            {
                string failedDeletionMessage = NotificationMessages
                    .RecordFailedDeletionErrorMessage;

                TempData["FilmProductionErrorMessage"] =
                    string.Format(
                        failedDeletionMessage,
                        $" film production {filmProductionToConfirmDeletion.Title}")
                    + "Check the film production relationship status!";

                return RedirectToAction(nameof(Delete));
            }
            else
            {
                _filmProductionService.DeleteFilmProductionImage(filmProductionToConfirmDeletion);
            }

            TempData["FilmProductionSuccessMessage"] = string.Format(
                NotificationMessages.RecordDeletionSuccessMessage,
                "Film Production", $"{filmProductionToConfirmDeletion.Title}");

            return RedirectToIndexActionInCurrentController();
        }
    }
}
