using Data.DataModels.Enums;

namespace SubtitlesManagementSystem.Web.Models.Users
{
    public class AllUsersViewModel
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public PromotionStatus PromotionStatus { get; set; }

        public string PromotionLevel { get; set; }

        public List<string> Roles { get; set; }
    }
}
