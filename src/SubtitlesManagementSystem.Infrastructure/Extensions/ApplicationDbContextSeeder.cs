﻿using Data.DataAccess.Seeding;
using Data.DataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace SubtitlesManagementSystem.Infrastructure.Extensions
{
    public static class ApplicationDbContextSeeder
    {
        public static void ApplyDatabaseSeeding<T>(this IApplicationBuilder applicationBuilder, ILogger<T> logger)
        {
            try
            {
                using (var serviceScope = applicationBuilder
                    .ApplicationServices.CreateScope())
                {
                    using (var applicationDbContext = serviceScope
                        .ServiceProvider.GetRequiredService<ApplicationDbContext>())
                    {
                        var seeders = new List<ISeeder>
                        {
                            new RolesSeeder(),
                            new UsersSeeder(),
                            new EntitiesSeeder(),
                        };

                        foreach (var seeder in seeders)
                        {
                            bool isCurrentSeederCompleted = seeder
                                .SeedDatabase(applicationDbContext, serviceScope.ServiceProvider)
                                    .Result.Equals(true);

                            if (isCurrentSeederCompleted)
                            {
                                logger.LogInformation($"Seeder {seeder.GetType().Name} done.");
                            }
                            else
                            {
                                logger.LogInformation($"Nothing new to seed");
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                logger.LogError(exception.Message);
            }
        }
    }
}
