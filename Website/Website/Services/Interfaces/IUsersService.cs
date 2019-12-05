using System.Security.Claims;
using System.Threading.Tasks;

using ViewModels.UI.User;

namespace Services.Interfaces
{
    public interface IUsersService
    {
        Task<ResponseLoginUserView> Login(RequestLoginUserView user);

        HomeInfoUserView GetUserHomeInfo(ClaimsPrincipal claims);

        Task<ResponseCreateUserView> Register(RequestCreateUserView user);

        SettingsUserView GetUserSettings(ClaimsPrincipal principal);

        ResponseChangeNameUserView ChangeUsername(RequestChangeNameUserView user);

        Task<ResponseChangePasswordUserView> ChangePassword(RequestChangePasswordUserView user, ClaimsPrincipal principal);

        ResponseChangeProfilePhotoUserView ChangeProfilePhoto(RequestChangeProfilePhotoUserView user);

        void DeleteAccount(string id);

        void RemoveProfilePhoto(string id);

        Task Logout();
    }
}
