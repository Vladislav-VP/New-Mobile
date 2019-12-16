using System.Threading.Tasks;

using TestProject.ApiModels.User;

namespace TestProject.Services.Interfaces
{
    public interface IUserService : IBaseApiService
    {
        Task<ResponseLoginUserApiModel> Login(RequestLoginUserApiModel user);

        Task<ResponseRegisterUserApiModel> RegisterUser(RequestRegisterUserApiModel user);

        Task<ResponseEditUserInfoUserApiModel> EditUserInfo(RequestEditUserInfoUserApiModel user);

        Task<ResponseChangePasswordUserApiModel> ChangePassword(RequestChangePasswordUserApiModel user);

        Task<ResponseEditProfileImageUserApiModel> EditProfilePhoto(RequestEditProfileImageUserApiModel user);

        Task<GetProfileImageUserApiModel> GetUserWithImage();

        Task<GetUserInfoUserApiModel> GetUserInfo();

        Task Logout();

        Task<DeleteUserApiModel> Delete();
    }
}
