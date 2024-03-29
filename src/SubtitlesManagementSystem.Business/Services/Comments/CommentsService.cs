﻿using Data.DataAccess.Repositories.Interfaces;
using Data.DataModels.Entities;
using SubtitlesManagementSystem.Web.Models.Comments.BindingModels;
using SubtitlesManagementSystem.Web.Models.SubtitlesCatalogue;

namespace SubtitlesManagementSystem.Business.Services.Comments
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;

        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public bool CreateComment(CreateCommentBindingModel createCommentBindingModel, string subtitlesId, string userId)
        {
            Comment commentToCreate = new Comment()
            {
                Content = createCommentBindingModel.Content,
                SubtitlesId = subtitlesId,
                ApplicationUserId = userId
            };

            var allComments = _commentRepository.GetAllAsNoTracking();

            if (_commentRepository.Exists(allComments, commentToCreate))
            {
                return false;
            }

            _commentRepository.Add(commentToCreate);

            return true;
        }

        public IEnumerable<AllCommentsForSubtitlesCatalogueItemViewModel> GetAllCommentsForSubtitlesCatalogueItem(string subtitlesId)
        {
            var allCommentsForSubtitlesCatalogueItem = _commentRepository
                .GetAllAsNoTracking()
                    .Where(c => c.SubtitlesId == subtitlesId)
                    .Select(c => new AllCommentsForSubtitlesCatalogueItemViewModel
                    {
                        Username = c.ApplicationUser.UserName,
                        CreatedOn = c.CreatedOn,
                        Content = c.Content,
                    })
                    .ToList();

            return allCommentsForSubtitlesCatalogueItem;
        }
    }
}
