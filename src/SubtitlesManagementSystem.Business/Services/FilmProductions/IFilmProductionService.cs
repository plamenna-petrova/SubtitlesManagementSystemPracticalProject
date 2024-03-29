﻿using Data.DataModels.Entities;
using SubtitlesManagementSystem.Web.Models.FilmProductions.BindingModels;
using SubtitlesManagementSystem.Web.Models.FilmProductions.ViewModels;

namespace SubtitlesManagementSystem.Business.Services.FilmProductions
{
    public interface IFilmProductionService
    {
        List<FilmProduction> GetAllFilmProductions();

        IEnumerable<AllFilmProductionsViewModel> GetAllFilmProductionsWithRelatedData();

        FilmProductionFullDetailsViewModel GetFilmProductionDetails(string filmProductionId);

        CreateFilmProductionBindingModel GetFilmProductionCreatingDetails();

        void CreateFilmProduction(
            CreateFilmProductionBindingModel createFilmProductionBindingModel,
            string[] selectedGenres, string[] selectedActors,
            string[] selectedDirectors, string[] selectedScreenwriters, string currentUserName
        );

        EditFilmProductionBindingModel GetFilmProductionEditingDetails(string filmProductionId);

        void EditFilmProduction(
            EditFilmProductionBindingModel editFilmProductionBindingModel,
            string[] selectedGenres, string[] selectedActors,
            string[] selectedDirectors, string[] selectedScreenwriters, string currentUserName
        );

        DeleteFilmProductionViewModel GetFilmProductionDeletionDetails(string filmProductionId);

        void DeleteFilmProduction(FilmProduction filmProduction);

        FilmProduction FindFilmProduction(string filmProductionId);

        void DeleteFilmProductionImage(FilmProduction filmProduction);
    }
}
