using SubtitlesManagementSystem.Web.Models.Favourites;

namespace SubtitlesManagementSystem.Business.Services.Favourites
{
    public interface IFavouritesService
    {
        IEnumerable<UserFavouritesViewModel> GetAllUserFavourites(string userId);

        bool AddToFavourites(string userId, string subtitlesId);

        void RemoveFromFavourites(Data.DataModels.Entities.Favourites favourites);

        Data.DataModels.Entities.Favourites FindFavourites(string userId, string subtiltesId);
    }
}
