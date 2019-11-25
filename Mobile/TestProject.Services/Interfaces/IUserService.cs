using System.Threading.Tasks;
using TestProject.ApiModels.User;
using TestProject.Entities;
using TestProject.Services.DataHandleResults;
using TestProject.Services.Helpers;

namespace TestProject.Services.Interfaces
{
    public interface IUserService : IBaseApiService
    {
        Task<ResponseLoginUserApiModel> Login(RequestLoginUserApiModel user);

        Task<ResponseRegisterUserApiModel> RegisterUser(RequestRegisterUserApiModel user);

        Task<ResponseEditNameUserApiModel> EditUsername(RequestEditNameUserApiModel user);

        Task<ResponseChangePasswordUserApiModel> ChangePassword(RequestChangePasswordUserApiModel user);

        Task<ResponseEditProfileImageUserApiModel> EditProfilePhoto(RequestEditProfileImageUserApiModel user);

        Task<TodoItem> Get(string name);

        Task<GetProfileImageUserApiModel> GetUserWithImage(int id);

        Task<string> GetUserName(int id);
    }
}
