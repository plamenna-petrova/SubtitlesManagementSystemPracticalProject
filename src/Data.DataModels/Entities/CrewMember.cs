using Data.DataModels.Abstraction;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data.DataModels.Entities
{
    public abstract class CrewMember : BaseEntity
    {
        [Required]
        [MinLength(2)]
        [MaxLength(25)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(25)]
        public string LastName { get; set; }

        public string CreatedBy { get; set; }

        public string ModifiedBy { get; set; }
    }
}
