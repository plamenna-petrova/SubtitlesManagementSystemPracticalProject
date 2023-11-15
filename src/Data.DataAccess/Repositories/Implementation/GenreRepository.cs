using Data.DataAccess.Repositories.Interfaces;
using Data.DataModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataAccess.Repositories.Implementation
{
    public class GenreRepository : BaseRepository<Genre>, IGenreRepository
    {
        public GenreRepository(ApplicationDbContext applicationDbContext)
            : base(applicationDbContext)
        {

        }

        public override bool Exists(IQueryable<Genre> genres, Genre genreToFind)
        {
            Expression<Func<Genre, bool>> genreExistsPredicate = g =>
                g.Name.Trim().ToLower() == genreToFind.Name.ToLower();

            bool countryExists = genres.Any(genreExistsPredicate);

            return countryExists;
        }
    }
}
