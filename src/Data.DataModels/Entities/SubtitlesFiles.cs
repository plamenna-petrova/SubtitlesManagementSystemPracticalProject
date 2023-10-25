using Data.DataModels.Abstraction;

namespace Data.DataModels.Entities
{
    public class SubtitlesFiles : BaseEntity
    {
        public string FileName { get; set; }

        public string SubtitlesId { get; set; }
    }
}
