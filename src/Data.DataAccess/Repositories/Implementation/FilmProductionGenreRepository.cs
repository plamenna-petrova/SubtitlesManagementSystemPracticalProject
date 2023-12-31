﻿using Data.DataAccess.Repositories.Interfaces;
using Data.DataModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataAccess.Repositories.Implementation
{
    public class FilmProductionGenreRepository : BaseRepository<FilmProductionGenre>,
        IFilmProductionGenreRepository
    {
        public FilmProductionGenreRepository(ApplicationDbContext applicationDbContext)
            : base(applicationDbContext)
        {

        }
    }
}
