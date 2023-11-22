using Data.DataAccess.Repositories.Implementation;
using Data.DataAccess.Repositories.Interfaces;
using Data.DataModels.Entities;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }
    }
}
