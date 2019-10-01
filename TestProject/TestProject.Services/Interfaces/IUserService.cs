using System.Threading.Tasks;

using TestProject.Entities;
using TestProject.Services.DataHandleResults;

namespace TestProject.Services.Interfaces
{
    public interface IUserService
    {
        Task<LoginResult> Login(User user);

        Task<RegistrationResult> RegisterUser(User user);

        Task<EditUsernameResult> EditUsername(User user, string newUserName);

        Task<EditPasswordResult> ChangePassword(User user, string oldPassword,
            string newPassword, string newPasswordConfirmation);
    }
}
