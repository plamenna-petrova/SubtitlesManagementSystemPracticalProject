using Data.DataModels.Entities.Identity;
using SubtitlesManagementSystem.Web.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubtitlesManagementSystem.Business.Services.Users
{
    public interface IUserService
    {
        IEnumerable<AllUsersViewModel> GetAllUsers(string currentUserId);

        Task PromoteUser(string userId);

        ApplicationUser FindUser(string userId);

        void DeclinePromotion(string userId);

        Task DemoteUser(string userId);

        void EnrollForUploaderRole(string userId);

        void EnrollForEditorRole(string userId);

        DeleteUserViewModel GetUserDeletionDetails(string userId);

        Task<bool> DeleteUser(string userId);
    }
}
