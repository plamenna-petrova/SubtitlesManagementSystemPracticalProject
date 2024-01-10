using Data.DataAccess.Repositories.Interfaces;
using Data.DataModels.Entities;

namespace Data.DataAccess.Repositories.Implementation
{
    public class SubtitlesFilesRepository : BaseRepository<SubtitlesFiles>, ISubtitlesFilesRepository
    {
        public SubtitlesFilesRepository(ApplicationDbContext applicationDbContext)
            : base(applicationDbContext)
        {

        }
    }
}
