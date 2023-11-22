﻿using Data.DataModels.Entities;
using SubtitlesManagementSystem.Web.Models.Screenwriters.BindingModels;
using SubtitlesManagementSystem.Web.Models.Screenwriters.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubtitlesManagementSystem.Business.Services.Screenwriters
{
    public interface IScreenwriterService
    {
        IEnumerable<AllScreenwritersViewModel> GetAllScreenwriters();

        CreateScreenwriterBindingModel GetScreenwriterCreatingDetails();

        ScreenwriterDetailsViewModel GetScreenwriterDetails(string directorId);

        bool CreateScreenwriter(CreateScreenwriterBindingModel createScreenwriterBindingModel, string[] selectedFilmProductions, string currentUserName);

        EditScreenwriterBindingModel GetScreenwriterEditingDetails(string directorId);

        bool EditScreenwriter(EditScreenwriterBindingModel editScreenwriterBindingModel, string[] selectedFilmProductions, string currentUserName);

        DeleteScreenwriterViewModel GetScreenwriterDeletionDetails(string directorId);

        void DeleteScreenwriter(Screenwriter director);

        Screenwriter FindScreenwriter(string directorId);
    }
}
