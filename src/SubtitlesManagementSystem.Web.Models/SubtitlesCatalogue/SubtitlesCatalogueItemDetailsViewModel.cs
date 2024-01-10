namespace SubtitlesManagementSystem.Web.Models.SubtitlesCatalogue
{
    public class SubtitlesCatalogueItemDetailsViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string UploaderUserName { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public FilmProductionForCatalogueViewModel RelatedFilmProduction { get; set; }

        public string CommentContent { get; set; }

        public IEnumerable<AllCommentsForSubtitlesCatalogueItemViewModel> Comments { get; set; }
    }
}
