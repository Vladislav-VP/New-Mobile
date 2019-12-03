using ViewModels.UI.Home;
using ViewModels.UI.User;

namespace Services.Interfaces
{
    public interface IUsersService
    {
        ResponseLoginHomeView Login(RequestLoginHomeView user);

        HomeInfoUserView GetUserHomeInfo(string id);

        ResponseCreateUserView Register(RequestCreateUserView user);

        SettingsUserView GetUserSettings(string id);

        ResponseChangeNameUserView ChangeUsername(RequestChangeNameUserView user);

        ResponseChangePasswordUserView ChangePassword(RequestChangePasswordUserView user);

        ResponseChangeProfilePhotoUserView ChangeProfilePhoto(RequestChangeProfilePhotoUserView user);

        void DeleteAccount(string id);

        void RemoveProfilePhoto(string id);
    }
}
