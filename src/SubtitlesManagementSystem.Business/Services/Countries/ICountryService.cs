using Data.DataModels.Entities;
using SubtitlesManagementSystem.Web.Models.Countries.BindingModels;
using SubtitlesManagementSystem.Web.Models.Countries.ViewModels;

namespace SubtitlesManagementSystem.Business.Services.Countries
{
    public interface ICountryService
    {
        List<Country> GetAllCountries();

        IEnumerable<AllCountriesViewModel> GetAllCountriesWithRelatedData();

        CountryDetailsViewModel GetCountryDetails(string countryId);

        bool CreateCountry(CreateCountryBindingModel createCountryBindingModel, string currentUserName);

        EditCountryBindingModel GetCountryEditingDetails(string countryId);

        bool EditCountry(EditCountryBindingModel editCountryBindingModel, string currentUserName);

        DeleteCountryViewModel GetCountryDeletionDetails(string countryId);

        void DeleteCountry(Country country);

        Country FindCountry(string countryId);
    }
}
