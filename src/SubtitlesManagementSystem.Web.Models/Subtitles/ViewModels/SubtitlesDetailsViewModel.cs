﻿using SubtitlesManagementSystem.Common.GlobalConstants;
using SubtitlesManagementSystem.Web.Models.FilmProductions.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SubtitlesManagementSystem.Web.Models.Subtitles.ViewModels
{
    public class SubtitlesDetailsViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        [DisplayName(DisplayConstants.CreatedOnDisplayName)]
        public DateTime CreatedOn { get; set; }

        [DisplayName(DisplayConstants.ModifiedOnDisplayName)]
        [DisplayFormat(NullDisplayText = DisplayConstants.NullModifiedOnEntryDisplayName)]
        public DateTime? ModifiedOn { get; set; }

        public FilmProductionDetailedInformationViewModel RelatedFilmProduction { get; set; }
    }
}
