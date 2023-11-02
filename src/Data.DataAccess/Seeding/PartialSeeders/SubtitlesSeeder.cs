﻿using Data.DataModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataAccess.Seeding.PartialSeeders
{
    internal static class SubtitlesSeeder
    {
        internal static Subtitles[] SubtitlesSeedingArray { get; private set; } = SeedSubtitles();

        private static Subtitles[] SeedSubtitles()
        {
            var subtitlesToSeed = new Subtitles[]
            {
                new Subtitles()
                {
                    Name = $"{FilmProductionSeeder.FilmProductionSeedingArray[0].Title} " +
                        $"{FilmProductionSeeder.FilmProductionSeedingArray[0].ReleaseDate.Year}",
                    CreatedOn = DateTime.UtcNow,
                    FilmProductionId = FilmProductionSeeder.FilmProductionSeedingArray[0].Id,
                },
                new Subtitles()
                {
                    Name = $"{FilmProductionSeeder.FilmProductionSeedingArray[1].Title} " +
                        $"{FilmProductionSeeder.FilmProductionSeedingArray[1].ReleaseDate.Year}",
                    CreatedOn = DateTime.UtcNow,
                    FilmProductionId = FilmProductionSeeder.FilmProductionSeedingArray[1].Id
                },
                new Subtitles()
                {
                    Name = $"{FilmProductionSeeder.FilmProductionSeedingArray[2].Title} " +
                        $"{FilmProductionSeeder.FilmProductionSeedingArray[2].ReleaseDate.Year}",
                    CreatedOn = DateTime.UtcNow,
                    FilmProductionId = FilmProductionSeeder.FilmProductionSeedingArray[2].Id
                },
                new Subtitles()
                {
                    Name = $"{FilmProductionSeeder.FilmProductionSeedingArray[3].Title} " +
                        $"{FilmProductionSeeder.FilmProductionSeedingArray[3].ReleaseDate.Year}",
                    CreatedOn = DateTime.UtcNow,
                    FilmProductionId = FilmProductionSeeder.FilmProductionSeedingArray[3].Id
                },
                new Subtitles()
                {
                    Name = $"{FilmProductionSeeder.FilmProductionSeedingArray[4].Title} " +
                        $"{FilmProductionSeeder.FilmProductionSeedingArray[4].ReleaseDate.Year}",
                    CreatedOn = DateTime.UtcNow,
                    FilmProductionId = FilmProductionSeeder.FilmProductionSeedingArray[4].Id
                }
            };

            return subtitlesToSeed;
        }
    }
}
