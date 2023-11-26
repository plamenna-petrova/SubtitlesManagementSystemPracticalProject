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
using SubtitlesManagementSystem.Business.Services.FilmProductions;
using SubtitlesManagementSystem.Business.Services.Subtitles;
using SubtitlesManagementSystem.Business.Transactions.Interfaces;
using SubtitlesManagementSystem.Common.GlobalConstants;
using SubtitlesManagementSystem.Web.Models.Subtitles.BindingModels;
using SubtitlesManagementSystem.Web.Models.Subtitles.ViewModels;
using System.Data;
using System.Security.Claims;

namespace SubtitlesManagementSystem.Web.Controllers
{
    public class SubtitlesController : BaseController
    {
        private readonly ISubtitlesService _subtitlesService;

        private readonly IFilmProductionService _filmProductionService;

        private readonly IUnitOfWork _unitOfWork;

        public SubtitlesController(
            ISubtitlesService subtitlesService,
            IFilmProductionService filmProductionService,
            IUnitOfWork unitOfWork
        )
        {
            _subtitlesService = subtitlesService;
            _filmProductionService = filmProductionService;
            _unitOfWork = unitOfWork;
        }

        [Authorize(Roles = "Administrator, Editor, Uploader")]
        public IActionResult Index()
        {
            IEnumerable<AllSubtitlesViewModel> allSubtitlesViewModel = _subtitlesService.GetAllSubtitles();

            return View(allSubtitlesViewModel);
        }


        [Authorize(Roles = "Administrator, Editor, Uploader")]
        public IActionResult Details(string id)
        {
            SubtitlesDetailsViewModel subtitlesDetailsViewModel = _subtitlesService
                .GetSubtitlesDetails(id);

            if (subtitlesDetailsViewModel == null)
            {
                return NotFound();
            }

            return View(subtitlesDetailsViewModel);
        }

        [Authorize(Roles = "Administrator, Editor, Uploader")]
        public IActionResult Create()
        {
            var allFilmProductions = _filmProductionService.GetAllFilmProductions();

            var allFilmProductionsIdsBySubtitles = _subtitlesService.GetAllToList()
                .Select(s => s.FilmProduction.Id)
                    .ToList();

            List<FilmProduction> allFilmProductionsForSelectList = new List<FilmProduction>();

            foreach (var filmProduction in allFilmProductions)
            {
                if (!allFilmProductionsIdsBySubtitles.Contains(filmProduction.Id))
                {
                    allFilmProductionsForSelectList.Add(filmProduction);
                }
            }

            if (allFilmProductionsForSelectList.Count > 0)
            {
                ViewData["FilmProductionByTitle"] = new SelectList(
                    allFilmProductionsForSelectList, "Id", "Title"
                );
            }
            else
            {
                TempData["SubtitlesErrorMessage"] = "No free film productions to create new subtitles";

                return RedirectToIndexActionInCurrentController();
            }

            return View(new CreateSubtitlesBindingModel());
        }

        [HttpPost]
        [Authorize(Roles = "Administrator, Editor, Uploader")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateSubtitlesBindingModel createSubtitlesBindingModel)
        {
            if (!ModelState.IsValid)
            {
                return View(createSubtitlesBindingModel);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            _subtitlesService.CreateSubtitles(createSubtitlesBindingModel, userId);

            bool areNewSubtitlesSavedToDatabase = _unitOfWork.CommitSaveChanges();

            if (!areNewSubtitlesSavedToDatabase)
            {
                var allFilmProductionsForSelectList = _filmProductionService.GetAllFilmProductions();

                ViewData["FilmProductionByTitle"] = new SelectList(
                    allFilmProductionsForSelectList,
                    "Id", "Title", createSubtitlesBindingModel.FilmProductionId
                );

                TempData["SubtitlesErrorMessage"] = string.Format(
                    NotificationMessages.NewRecordFailedSaveErrorMessage,
                "subtitles");

                return View(createSubtitlesBindingModel);
            }

            TempData["SubtitlesSuccessMessage"] = string.Format(
                NotificationMessages.RecordCreationSuccessMessage,
                "Subtitles", $"{createSubtitlesBindingModel.Name}");

            return RedirectToIndexActionInCurrentController();
        }

        [Authorize(Roles = "Administrator, Editor, Uploader")]
        public IActionResult Edit(string id)
        {
            EditSubtitlesBindingModel editSubtitlesBindingModel = _subtitlesService.GetSubtitlesEditingDetails(id);

            if (editSubtitlesBindingModel == null)
            {
                return NotFound();
            }

            var allFilmProductions = _filmProductionService.GetAllFilmProductions();

            var allFilmProductionsIdsBySubtitles = _subtitlesService.GetAllToList()
                .Select(s => s.FilmProduction.Id)
                    .ToList();

            List<FilmProduction> allFilmProductionsForSelectList = new List<FilmProduction>();

            foreach (var filmProduction in allFilmProductions)
            {
                if (!allFilmProductionsIdsBySubtitles.Contains(filmProduction.Id))
                {
                    allFilmProductionsForSelectList.Add(filmProduction);
                }
            }

            if (allFilmProductionsForSelectList.Count > 0)
            {
                ViewData["FilmProductionByTitle"] = new SelectList(
                    allFilmProductionsForSelectList, "Id", "Title"
                );
            }

            var subtitlesFilesBySubtitlesId = _subtitlesService.GetSubtitlesFilesBySubtitlesId(editSubtitlesBindingModel.Id);

            if (subtitlesFilesBySubtitlesId.Any())
            {
                ViewData["ExistingSubtitlesFilesNames"] = subtitlesFilesBySubtitlesId
                    .Select(sf => sf.FileName).ToList();
            }

            return View(editSubtitlesBindingModel);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator, Editor, Uploader")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditSubtitlesBindingModel editSubtitlesBindingModel)
        {
            var allFilmProductions = _filmProductionService.GetAllFilmProductions();

            var allFilmProductionsIdsBySubtitles = _subtitlesService.GetAllToList()
                .Select(s => s.FilmProduction.Id)
                    .ToList();

            List<FilmProduction> allFilmProductionsForSelectList = new List<FilmProduction>();

            foreach (var filmProduction in allFilmProductions)
            {
                if (!allFilmProductionsIdsBySubtitles.Contains(filmProduction.Id))
                {
                    allFilmProductionsForSelectList.Add(filmProduction);
                }
            }

            if (allFilmProductionsForSelectList.Count > 0)
            {
                ViewData["FilmProductionByTitle"] = new SelectList(
                    allFilmProductionsForSelectList, "Id", "Title"
                );
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            _subtitlesService.EditSubtitles(editSubtitlesBindingModel, userId);

            bool areCurrentSubtitlesSavedToDatabase = _unitOfWork.CommitSaveChanges();

            if (!areCurrentSubtitlesSavedToDatabase)
            {
                if (allFilmProductionsForSelectList.Count > 0)
                {
                    ViewData["FilmProductionByTitle"] = new SelectList(
                         allFilmProductionsForSelectList,
                         "Id", "Title", editSubtitlesBindingModel.FilmProductionId
                    );
                }

                TempData["SubtitlesErrorMessage"] = string.Format(
                    NotificationMessages.RecordFailedUpdateSaveErrorMessage,
                        "subtitles");

                return View(editSubtitlesBindingModel);
            }

            TempData["SubtitlesSuccessMessage"] = string.Format(NotificationMessages
                .RecordUpdateSuccessMessage, "Subtitles",
                $"");

            return RedirectToIndexActionInCurrentController();
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult Delete(string id)
        {
            DeleteSubtitlesViewModel deleteSubtitlesViewModel =
                _subtitlesService
                    .GetSubtitlesDeletionDetails(id);

            if (deleteSubtitlesViewModel == null)
            {
                return NotFound();
            }

            return View(deleteSubtitlesViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string id)
        {
            var subtitlesToConfirmDeletion = _subtitlesService
                .FindSubtitles(id);

            _subtitlesService.DeleteSubtitles(subtitlesToConfirmDeletion);

            bool areSubtitlesDeleted = _unitOfWork.CommitSaveChanges();

            if (!areSubtitlesDeleted)
            {
                string subtitlesFailedDeletionMessage = NotificationMessages
                    .RecordFailedDeletionErrorMessage;

                TempData["SubtitlesErrorMessage"] =
                    string.Format(
                        subtitlesFailedDeletionMessage,
                        $" subtitles {subtitlesToConfirmDeletion.Name}")
                    + "Check the subtitles relationship status!";

                return RedirectToAction(nameof(Delete));
            }

            TempData["SubtitlesSuccessMessage"] = string.Format(
                NotificationMessages.RecordDeletionSuccessMessage,
                "Subtitles", $"{subtitlesToConfirmDeletion.Name}");

            return RedirectToIndexActionInCurrentController();
        }
    }
}
