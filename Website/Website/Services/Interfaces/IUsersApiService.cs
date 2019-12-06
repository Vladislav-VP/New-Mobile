using Entities;
using ViewModels.Api.User;

namespace Services.Interfaces
{
    public interface IUsersApiService : IBaseApiService<User>
    {
        ResponseRegisterUserApiView Register(RequestRegisterUserApiView userToRegister);

        ResponseLoginUserApiView Login(RequestLoginUserApiView userRequest);

        GetProfileImageUserApiView GetUserWithPhoto(string id);

        ResponseEditProfileImageUserApiView ReplaceProfilePhoto(RequestEditProfileImageUserApiView user, string imageUrl);

        ResponseEditNameUserApiView EditUserName(RequestEditNameUserApiView user);

        string GetUserName(string id);

        ResponseChangePasswordUserApiView ChangePassword(RequestChangePasswordUserApiView user);

        DeleteUserApiView Delete(int id);
    }
}
