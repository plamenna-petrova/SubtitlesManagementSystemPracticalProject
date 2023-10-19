using Data.DataModels.Abstraction;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data.DataModels.Entities
{
    public class FilmProduction : BaseEntity
    {
        public FilmProduction()
        {
            FilmProductionGenres = new HashSet<FilmProductionGenre>();
        }

        [Required]
        [MinLength(2)]
        [MaxLength(30)]
        public string Title { get; set; }

        public string CountryId { get; set; }   

        public virtual Country Country { get; set; }

        public string LanguageId { get; set; }  

        public virtual Language Language { get; set; }

        public virtual ICollection<FilmProductionGenre> FilmProductionGenres { get; set; }
    }
}
