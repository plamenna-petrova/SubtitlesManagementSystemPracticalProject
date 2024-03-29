﻿using Data.DataAccess.Repositories.Interfaces;
using Data.DataModels.Entities.Identity;
using Data.DataModels.Enums;
using Microsoft.AspNetCore.Identity;
using SubtitlesManagementSystem.Web.Models.Users;
using System.Diagnostics;
using static SubtitlesManagementSystem.Common.GlobalConstants.IdentityConstants;

namespace SubtitlesManagementSystem.Business.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        private readonly ISubtitlesRepository _subtitlesRepository;

        private readonly IFavouritesRepository _favouritesRepository;

        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(
            IUserRepository userRepository,
            ISubtitlesRepository subtitlesRepository,
            IFavouritesRepository favouritesRepository,
            UserManager<ApplicationUser> userManager
        )
        {
            _userRepository = userRepository;
            _subtitlesRepository = subtitlesRepository;
            _favouritesRepository = favouritesRepository;
            _userManager = userManager;
        }

        public IEnumerable<AllUsersViewModel> GetAllUsers(string currentUserId)
        {
            var allUsers = _userRepository
                  .GetAllAsNoTracking()
                    .Where(u => u.Id != currentUserId)
                    .Select(au => new AllUsersViewModel
                    {
                        Id = au.Id,
                        Username = au.UserName,
                        PromotionStatus = au.PromotionStatus,
                        PromotionLevel = au.PromotionLevel,
                        Roles = au.ApplicationUserRoles.Select(aur => aur.Role.Name).ToList()
                    });

            return allUsers;
        }

        public async Task PromoteUser(string userId)
        {
            var user = FindUser(userId);

            if (user.PromotionStatus == PromotionStatus.Pending)
            {
                var roles = _userManager.GetRolesAsync(user).Result.ToArray();
                var mainUserRole = roles[0];

                switch (user.PromotionLevel)
                {
                    case UploaderRoleName:
                        if (mainUserRole == NormalUserRole)
                        {
                            await AssignRole(user, NormalUserRole, UploaderRoleName);
                            user.PromotionStatus = PromotionStatus.Accepted;
                            user.PromotionLevel = UploaderRoleName;
                        }
                        break;
                    case EditorRoleName:
                        if (mainUserRole == UploaderRoleName)
                        {
                            await AssignRole(user, UploaderRoleName, EditorRoleName);
                            user.PromotionStatus = PromotionStatus.Accepted;
                            user.PromotionLevel = EditorRoleName;
                        }
                        break;
                }

                _userRepository.Update(user);
            }
        }

        public async Task DemoteUser(string userId)
        {
            var user = FindUser(userId);
            var roles = _userManager.GetRolesAsync(user).Result.ToArray();

            var mainUserRole = roles[0];

            switch (mainUserRole)
            {
                case EditorRoleName:
                    await AssignRole(user, EditorRoleName, UploaderRoleName);
                    NeutralizePromotion(user.Id);
                    break;
                case UploaderRoleName:
                    await AssignRole(user, UploaderRoleName, NormalUserRole);
                    NeutralizePromotion(user.Id);
                    break;
            }
        }

        public void DeclinePromotion(string userId)
        {
            var user = FindUser(userId);

            if (user.PromotionStatus == PromotionStatus.Pending)
            {
                user.PromotionStatus = PromotionStatus.Declined;

                if (user.PromotionLevel != UploaderRoleName)
                {
                    user.PromotionLevel = null;
                }
            }

            _userRepository.Update(user);
        }

        public void NeutralizePromotion(string userId)
        {
            var user = FindUser(userId);

            if (user.PromotionStatus == PromotionStatus.Pending)
            {
                user.PromotionStatus = PromotionStatus.Neutral;

                if (user.PromotionLevel != UploaderRoleName)
                {
                    user.PromotionLevel = null;
                }
            }
        }

        public void EnrollForUploaderRole(string userId)
        {
            var user = FindUser(userId);

            user.PromotionStatus = PromotionStatus.Pending;
            user.PromotionLevel = UploaderRoleName;

            _userRepository.Update(user);
        }

        public void EnrollForEditorRole(string userId)
        {
            var user = FindUser(userId);

            user.PromotionStatus = PromotionStatus.Pending;
            user.PromotionLevel = EditorRoleName;

            _userRepository.Update(user);
        }

        public DeleteUserViewModel GetUserDeletionDetails(string userId)
        {
            var userToDelete = FindUser(userId);

            if (userToDelete is null)
            {
                return null;
            }

            var userToDeleteDetails = new DeleteUserViewModel
            {
                Id = userToDelete.Id,
                UserName = userToDelete.UserName,
                Role = userToDelete.ApplicationUserRoles.Select(aur => aur.Role).FirstOrDefault().Name
            };

            return userToDeleteDetails;
        }

        public async Task<bool> DeleteUser(string userId)
        {
            try
            {
                var managedUserToDelete = await _userManager.FindByIdAsync(userId);
                var userLogins = managedUserToDelete.Logins;
                var userRoles = await _userManager.GetRolesAsync(managedUserToDelete);

                foreach (var userLogin in userLogins.ToList())
                {
                    await _userManager.RemoveLoginAsync(managedUserToDelete, userLogin.LoginProvider, userLogin.ProviderKey);
                }

                if (userRoles.Count > 0)
                {
                    foreach (var userRole in userRoles.ToList())
                    {
                        await _userManager.RemoveFromRoleAsync(managedUserToDelete, userRole);
                    }
                }

                var subtitlesOfUser = _subtitlesRepository.GetAllAsNoTracking()
                    .Where(s => s.ApplicationUserId == managedUserToDelete.Id)
                    .ToArray();

                _subtitlesRepository.DeleteRange(subtitlesOfUser);

                var userFavourites = _favouritesRepository.GetAllAsNoTracking()
                    .Where(f => f.ApplicationUserId == managedUserToDelete.Id)
                    .ToArray();

                _favouritesRepository.DeleteRange(userFavourites);

                await _userManager.DeleteAsync(managedUserToDelete);

                return true;
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);

                return false;
            }
        }

        public ApplicationUser FindUser(string userId)
        {
            return _userRepository.GetById(userId);
        }

        private async Task AssignRole(ApplicationUser applicationUser, string oldRole, string newRole)
        {
            if (!string.IsNullOrEmpty(oldRole))
            {
                await _userManager.RemoveFromRoleAsync(applicationUser, oldRole);
            }

            await _userManager.AddToRoleAsync(applicationUser, newRole);

            _userRepository.Update(applicationUser);
        }
    }
}
