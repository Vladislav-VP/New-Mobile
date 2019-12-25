using System.Security.Claims;
using System.Threading.Tasks;

using ViewModels.UI.User;

namespace Services.Interfaces
{
    public interface IUsersService
    {
        Task<ResponseLoginUserView> Login(RequestLoginUserView user, ClaimsPrincipal principal);

        Task<HomeInfoUserView> GetUserHomeInfo(ClaimsPrincipal claims);

        Task<ResponseCreateUserView> Register(RequestCreateUserView user);

        Task<SettingsUserView> GetUserSettings(ClaimsPrincipal principal);

        Task<ResponseChangeNameUserView> ChangeUsername(RequestChangeNameUserView user, ClaimsPrincipal principal);

        Task<ResponseChangePasswordUserView> ChangePassword(RequestChangePasswordUserView user, ClaimsPrincipal principal);

        Task<ResponseChangeProfilePhotoUserView> ChangeProfilePhoto(RequestChangeProfilePhotoUserView user, ClaimsPrincipal principal);

        Task DeleteAccount(ClaimsPrincipal principal);

        Task RemoveProfilePhoto(ClaimsPrincipal principal);

        Task Logout(ClaimsPrincipal principal);

        Task<ConfirmEmailUserView> ConfirmEmail(string userId);

        Task<ResponseResetPasswordUserView> ResetPassword(RequestResetPasswordUserView requestReset);

        Task<ResponseChangeEmailUserView> ChangeEmail(RequestChangeEmailUserView requestChangeEmail, ClaimsPrincipal principal);

        Task<ConfirmChangeEmailUserView> ConfirmChangeEmail(string userId, string email);
    }
}
