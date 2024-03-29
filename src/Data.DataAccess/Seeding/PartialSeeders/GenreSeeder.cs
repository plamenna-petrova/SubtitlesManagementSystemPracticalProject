﻿using Data.DataModels.Entities;

namespace Data.DataAccess.Seeding.PartialSeeders
{
    internal static class GenreSeeder
    {
        internal static Genre[] GenreSeedingArray { get; private set; } = SeedGenres();

        private static Genre[] SeedGenres()
        {
            var genresToSeed = new Genre[]
            {
                new Genre()
                {
                    Name = "Action",
                    CreatedOn = DateTime.UtcNow
                },
                new Genre()
                {
                    Name = "Adventure",
                    CreatedOn = DateTime.UtcNow
                },
                new Genre()
                {
                    Name = "Mystery",
                    CreatedOn = DateTime.UtcNow
                },
                new Genre()
                {
                    Name = "Comedy",
                    CreatedOn = DateTime.UtcNow
                },
                new Genre()
                {
                    Name = "Sci-Fi",
                    CreatedOn = DateTime.UtcNow
                },
                new Genre()
                {
                    Name = "Thriller",
                    CreatedOn = DateTime.UtcNow
                },
                new Genre()
                {
                    Name = "Drama",
                    CreatedOn = DateTime.UtcNow
                },
                new Genre()
                {
                    Name = "Crime",
                    CreatedOn = DateTime.UtcNow
                }
            };

            return genresToSeed;
        }
    }
}
