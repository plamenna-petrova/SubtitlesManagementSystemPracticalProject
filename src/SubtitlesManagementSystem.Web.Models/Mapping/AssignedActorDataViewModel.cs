using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubtitlesManagementSystem.Web.Models.Mapping
{
    public class AssignedActorDataViewModel
    {
        public string ActorId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool IsAssigned { get; set; }
    }
}
