using Data.DataModels.Entities;
using SubtitlesManagementSystem.Web.Models.Languages.BindingModels;
using SubtitlesManagementSystem.Web.Models.Languages.ViewModels;

namespace SubtitlesManagementSystem.Business.Services.Languages
{
    public interface ILanguageService
    {
        List<Language> GetAllLanguages();

        IEnumerable<AllLanguagesViewModel> GetAllLanguagesWithRelatedData();

        LanguageDetailsViewModel GetLanguageDetails(string languageId);

        bool CreateLanguage(CreateLanguageBindingModel createLanguageBindingModel, string currentUserName);

        EditLanguageBindingModel GetLanguageEditingDetails(string languageId);

        bool EditLanguage(EditLanguageBindingModel editLanguageBindingModel, string currentUserName);

        DeleteLanguageViewModel GetLanguageDeletionDetails(string languageId);

        void DeleteLanguage(Language language);

        Language FindLanguage(string languageId);
    }
}
