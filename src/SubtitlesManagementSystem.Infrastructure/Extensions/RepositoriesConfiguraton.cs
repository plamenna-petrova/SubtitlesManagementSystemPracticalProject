﻿using Data.DataAccess.Repositories.Implementation;
using Data.DataAccess.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace SubtitlesManagementSystem.Infrastructure.Extensions
{
    public static class RepositoriesConfiguration
    {
        public static void RegisterRepositories(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<ICountryRepository, CountryRepository>();
            serviceCollection.AddScoped<IGenreRepository, GenreRepository>();
            serviceCollection.AddScoped<ILanguageRepository, LanguageRepository>();
            serviceCollection.AddScoped<IActorRepository, ActorRepository>();
            serviceCollection.AddScoped<IFilmProductionRepository, FilmProductionRepository>();
            serviceCollection.AddScoped<IFilmProductionActorRepository, FilmProductionActorRepository>();
            serviceCollection.AddScoped<IDirectorRepository, DirectorRepository>();
            serviceCollection.AddScoped<IFilmProductionDirectorRepository, FilmProductionDirectorRepository>();
            serviceCollection.AddScoped<IScreenwriterRepository, ScreenwriterRepository>();
            serviceCollection.AddScoped<IFilmProductionScreenwriterRepository, FilmProductionScreenwriterRepository>();
            serviceCollection.AddScoped<IFilmProductionGenreRepository, FilmProductionGenreRepository>();
            serviceCollection.AddScoped<ISubtitlesRepository, SubtitlesRepository>();
            serviceCollection.AddScoped<ISubtitlesFilesRepository, SubtitlesFilesRepository>();
            serviceCollection.AddScoped<ICommentRepository, CommentRepository>();
            serviceCollection.AddScoped<IFavouritesRepository, FavouritesRepository>();
            serviceCollection.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
