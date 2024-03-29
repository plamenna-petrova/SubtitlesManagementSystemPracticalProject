﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using SubtitlesManagementSystem.Common.GlobalConstants;
using SubtitlesManagementSystem.Web.Models.FilmProductions.ViewModels;

namespace SubtitlesManagementSystem.Web.Models.Countries.ViewModels
{
    public class CountryDetailsViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        [DisplayName(DisplayConstants.CreatedOnDisplayName)]
        public DateTime CreatedOn { get; set; }

        [DisplayName(DisplayConstants.CreatedByDisplayName)]
        public string CreatedBy { get; set; }

        [DisplayName(DisplayConstants.ModifiedOnDisplayName)]
        [DisplayFormat(NullDisplayText = DisplayConstants.NullModifiedOnEntryDisplayName)]
        public DateTime? ModifiedOn { get; set; }

        [DisplayName(DisplayConstants.ModifiedByDisplayName)]
        [DisplayFormat(NullDisplayText = DisplayConstants.NullModifiedByEntryDisplayName)]
        public string ModifiedBy { get; set; }

        public IEnumerable<FilmProductionDetailedInformationViewModel> RelatedFilmProductions { get; set; }
    }
}
