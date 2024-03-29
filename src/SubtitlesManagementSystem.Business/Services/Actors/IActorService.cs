﻿using Data.DataModels.Entities;
using SubtitlesManagementSystem.Web.Models.Actors.BindingModels;
using SubtitlesManagementSystem.Web.Models.Actors.ViewModels;

namespace SubtitlesManagementSystem.Business.Services.Actors
{
    public interface IActorService
    {
        IEnumerable<AllActorsViewModel> GetAllActors();

        ActorDetailsViewModel GetActorDetails(string actorId);

        CreateActorBindingModel GetActorCreatingDetails();

        bool CreateActor(CreateActorBindingModel createActorBindingModel, string[] selectedFilmProductions, string currentUserName);

        EditActorBindingModel GetActorEditingDetails(string actorId);

        bool EditActor(EditActorBindingModel editActorBindingModel, string[] selectedFilmProductions, string currentUserName);

        DeleteActorViewModel GetActorDeletionDetails(string actorId);

        void DeleteActor(Actor actor);

        Actor FindActor(string actorId);
    }
}
