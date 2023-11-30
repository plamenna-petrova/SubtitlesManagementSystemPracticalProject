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
    public class FavouritesRepository : BaseRepository<Favourites>, IFavouritesRepository
    {
        public FavouritesRepository(ApplicationDbContext applicationDbContext)
            : base(applicationDbContext)
        {

        }

        public override bool Exists(IQueryable<Favourites> favourites, Favourites favouritesToFind)
        {
            Expression<Func<Favourites, bool>> favouritesExistsPredicate = f =>
                f.ApplicationUserId == favouritesToFind.ApplicationUserId &&
                f.SubtitlesId == favouritesToFind.SubtitlesId;

            bool favouritesExists = favourites.Any(favouritesExistsPredicate);

            return favouritesExists;
        }
    }
}
