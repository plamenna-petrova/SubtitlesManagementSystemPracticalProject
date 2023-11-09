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
    public class CountryRepository : BaseRepository<Country>, ICountryRepository
    {
        public CountryRepository(ApplicationDbContext applicationDbContext)
            : base(applicationDbContext)
        {

        }

        public override bool Exists(IQueryable<Country> countries, Country countryToFind)
        {
            Expression<Func<Country, bool>> countryExistsExpression = c =>
                c.Name.Trim().ToLower() == countryToFind.Name.ToLower();

            bool countryExists = countries.Any(countryExistsExpression);

            return countryExists;
        }
    }
}
