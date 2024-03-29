﻿using Data.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using SubtitlesManagementSystem.Web.Models.Comments.ViewModels;
using SubtitlesManagementSystem.Web.Models.Favourites;
using SubtitlesManagementSystem.Web.Models.SubtitlesCatalogue;

namespace SubtitlesManagementSystem.Business.Services.SubtitlesCatalogue
{
    public class SubtitlesCatalogueService : ISubtitlesCatalogueService
    {
        private readonly ISubtitlesRepository _subtitlesRepository;

        private readonly ICommentRepository _commentRepository;

        private readonly IFavouritesRepository _favouritesRepository;

        public SubtitlesCatalogueService(
            ISubtitlesRepository subtitlesRepository,
            ICommentRepository commentRepository,
            IFavouritesRepository favouritesRepository
        )
        {
            _subtitlesRepository = subtitlesRepository;
            _commentRepository = commentRepository;
            _favouritesRepository = favouritesRepository;
        }

        public CatalogueItemsViewModel GetAllSubtitlesForCatalogue()
        {
            List<AllSubtitlesForCatalogueViewModel> allSubtitlesForCatalogue = _subtitlesRepository
                .GetAllAsNoTracking()
                    .Select(s => new AllSubtitlesForCatalogueViewModel
                    {
                        Id = s.Id,
                        Name = s.Name,
                        UploaderUserName = s.ApplicationUser.UserName,
                        UploadedOn = s.ModifiedOn,
                        RelatedFilmProduction = new FilmProductionForCatalogueViewModel
                        {
                            Title = s.FilmProduction.Title,
                            Duration = s.FilmProduction.Duration,
                            ReleaseDate = s.FilmProduction.ReleaseDate,
                            PlotSummary = s.FilmProduction.PlotSummary,
                            CountryName = s.FilmProduction.Country.Name,
                            LanguageName = s.FilmProduction.Language.Name,
                            ImageName = s.FilmProduction.ImageName
                        }
                    })
                    .OrderBy(s => s.Name)
                    .ToList();

            List<LatestCommentViewModel> latestComments = _commentRepository
                .GetAllAsNoTracking()
                    .OrderByDescending(c => c.CreatedOn)
                    .Take(5)
                    .Select(c => new LatestCommentViewModel
                    {
                        SubtitlesId = c.Subtitles.Id,
                        SubtitlesName = c.Subtitles.Name,
                        UserName = c.ApplicationUser.UserName
                    })
                    .ToList();

            var topSubtitlesIds = _favouritesRepository
               .GetAllAsNoTracking()
                   .GroupBy(f => f.SubtitlesId)
                   .OrderByDescending(fgr => fgr.Count())
                   .Take(5)
                   .Select(fgr => fgr.Key)
                   .ToList();

            List<TopSubtitlesViewModel> topSubtitles = new List<TopSubtitlesViewModel>();

            topSubtitlesIds.ForEach(topSubtitlesId =>
            {
                var relatedSubtitles = _subtitlesRepository.GetById(topSubtitlesId);

                topSubtitles.Add(new TopSubtitlesViewModel
                {
                    Id = relatedSubtitles.Id,
                    Name = relatedSubtitles.Name,
                    UploaderUserName = relatedSubtitles.ApplicationUser.UserName
                });
            });

            CatalogueItemsViewModel catalogueItemsViewModel = new CatalogueItemsViewModel
            {
                AllSubtitlesForCatalogue = allSubtitlesForCatalogue,
                LatestComments = latestComments,
                TopSubtitles = topSubtitles
            };

            return catalogueItemsViewModel;
        }

        public SubtitlesCatalogueItemDetailsViewModel GetSubtitlesCatalogueItemDetails(string subtitlesId)
        {
            var singleSubtitlesCatalogueItem = _subtitlesRepository
               .GetAllByCondition(s => s.Id == subtitlesId)
                   .Include(s => s.ApplicationUser)
                      .FirstOrDefault();

            if (singleSubtitlesCatalogueItem is null)
            {
                return null;
            }

            var singleSubtitlesDetails = new SubtitlesCatalogueItemDetailsViewModel
            {
                Id = singleSubtitlesCatalogueItem.Id,
                Name = singleSubtitlesCatalogueItem.Name,
                CreatedOn = singleSubtitlesCatalogueItem.CreatedOn,
                ModifiedOn = singleSubtitlesCatalogueItem.ModifiedOn,
                UploaderUserName = singleSubtitlesCatalogueItem.ApplicationUser.UserName,
                RelatedFilmProduction = new FilmProductionForCatalogueViewModel
                {
                    Title = singleSubtitlesCatalogueItem.FilmProduction.Title,
                    Duration = singleSubtitlesCatalogueItem.FilmProduction.Duration,
                    ReleaseDate = singleSubtitlesCatalogueItem.FilmProduction.ReleaseDate,
                    PlotSummary = singleSubtitlesCatalogueItem.FilmProduction.PlotSummary,
                    CountryName = singleSubtitlesCatalogueItem.FilmProduction.Country.Name,
                    LanguageName = singleSubtitlesCatalogueItem.FilmProduction.Language.Name,
                    ImageName = singleSubtitlesCatalogueItem.FilmProduction.ImageName
                }
            };

            return singleSubtitlesDetails;
        }
    }
}
