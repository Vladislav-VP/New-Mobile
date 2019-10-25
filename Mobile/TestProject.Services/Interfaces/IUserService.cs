using System.Threading.Tasks;

using TestProject.Entities;
using TestProject.Services.DataHandleResults;
using TestProject.Services.Helpers;

namespace TestProject.Services.Interfaces
{
    public interface IUserService : IBaseApiService<User>
    {
        Task<DataHandleResult<User>> Login(User user);

        Task<DataHandleResult<User>> RegisterUser(User user);

        Task<DataHandleResult<User>> EditUsername(User user, string newUserName);

        Task<DataHandleResult<EditPasswordHelper>> ChangePassword(User user, string oldPassword,
            string newPassword, string newPasswordConfirmation);

        Task<User> FindByName(string name);
    }
}
