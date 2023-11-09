using Microsoft.Extensions.DependencyInjection;
using SubtitlesManagementSystem.Business.Services.Countries;
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
        }
    }
}
