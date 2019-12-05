using System.Security.Claims;
using System.Threading.Tasks;

using ViewModels.UI.Home;
using ViewModels.UI.User;

namespace Services.Interfaces
{
    public interface IUsersService
    {
        Task<ResponseLoginUserView> Login(RequestLoginUserView user);

        HomeInfoUserView GetUserHomeInfo(ClaimsPrincipal claims);

        Task<ResponseCreateUserView> Register(RequestCreateUserView user);

        SettingsUserView GetUserSettings(string id);

        ResponseChangeNameUserView ChangeUsername(RequestChangeNameUserView user);

        ResponseChangePasswordUserView ChangePassword(RequestChangePasswordUserView user);

        ResponseChangeProfilePhotoUserView ChangeProfilePhoto(RequestChangeProfilePhotoUserView user);

        void DeleteAccount(string id);

        void RemoveProfilePhoto(string id);
    }
}
