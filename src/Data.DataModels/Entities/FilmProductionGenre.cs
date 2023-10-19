using System;
using System.Collections.Generic;
using System.Text;

namespace Data.DataModels.Entities
{
    public class FilmProductionGenres
    {
        public string FilmProductionId { get; set; }

        public virtual FilmProduction FilmProduction { get; set; }

        public string GenreId { get; set; }

        public virtual Genre Genre { get; set; }
    }
}
