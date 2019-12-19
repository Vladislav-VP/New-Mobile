using System.Security.Claims;
using System.Threading.Tasks;

using ViewModels.UI.User;

namespace Services.Interfaces
{
    public interface IUsersService
    {
        Task<ResponseLoginUserView> Login(RequestLoginUserView user, ClaimsPrincipal principal);

        HomeInfoUserView GetUserHomeInfo(ClaimsPrincipal claims);

        Task<ResponseCreateUserView> Register(RequestCreateUserView user);

        SettingsUserView GetUserSettings(ClaimsPrincipal principal);

        Task<ResponseChangeNameUserView> ChangeUsername(RequestChangeNameUserView user, ClaimsPrincipal principal);

        Task<ResponseChangePasswordUserView> ChangePassword(RequestChangePasswordUserView user, ClaimsPrincipal principal);

        ResponseChangeProfilePhotoUserView ChangeProfilePhoto(RequestChangeProfilePhotoUserView user, ClaimsPrincipal principal);

        Task DeleteAccount(ClaimsPrincipal principal);

        void RemoveProfilePhoto(ClaimsPrincipal principal);

        Task Logout(ClaimsPrincipal principal);

        Task<ResponseResetPasswordUserView> ResetPassword(RequestResetPasswordUserView requestReset);
    }
}
