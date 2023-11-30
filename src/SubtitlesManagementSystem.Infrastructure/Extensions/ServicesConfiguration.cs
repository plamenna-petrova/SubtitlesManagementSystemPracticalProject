using Data.DataAccess.Repositories.Implementation;
using Data.DataAccess.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using SubtitlesManagementSystem.Business.Services.Actors;
using SubtitlesManagementSystem.Business.Services.Comments;
using SubtitlesManagementSystem.Business.Services.Countries;
using SubtitlesManagementSystem.Business.Services.Directors;
using SubtitlesManagementSystem.Business.Services.Favourites;
using SubtitlesManagementSystem.Business.Services.FilmProductions;
using SubtitlesManagementSystem.Business.Services.Genres;
using SubtitlesManagementSystem.Business.Services.Languages;
using SubtitlesManagementSystem.Business.Services.Screenwriters;
using SubtitlesManagementSystem.Business.Services.Subtitles;
using SubtitlesManagementSystem.Business.Services.SubtitlesCatalogue;
using SubtitlesManagementSystem.Business.Transactions.Implementation;
using SubtitlesManagementSystem.Business.Transactions.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubtitlesManagementSystem.Infrastructure.Extensions
{
    public static class ServicesConfiguration
    {
        public static void RegisterServiceLayer(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();

            serviceCollection.AddTransient<ICountryService, CountryService>();
            serviceCollection.AddTransient<IGenreService, GenreService>();
            serviceCollection.AddTransient<ILanguageService, LanguageService>();
            serviceCollection.AddTransient<IActorService, ActorService>();
            serviceCollection.AddTransient<IFilmProductionService, FilmProductionService>();
            serviceCollection.AddTransient<IDirectorService, DirectorService>();
            serviceCollection.AddTransient<IScreenwriterService, ScreenwriterService>();
            serviceCollection.AddTransient<ISubtitlesService, SubtitlesService>();
            serviceCollection.AddTransient<ICommentService, CommentService>();
            serviceCollection.AddTransient<IFavouritesService, FavouritesService>();
            serviceCollection.AddTransient<ISubtitlesCatalogueService, SubtitlesCatalogueService>();
        }
    }
}
