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

        Task<TResponse> EditUsername<TRequest, TResponse>(TRequest user, string newUserName);

        Task<DataHandleResult<EditPasswordHelper>> ChangePassword(int userId, string oldPassword,
            string newPassword, string newPasswordConfirmation);

        Task EditProfilePhoto(TodoItem user);

        Task<TodoItem> Get(string name);

        Task<TodoItem> GetUserWithImage();
    }
}
