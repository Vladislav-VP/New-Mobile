using System.Threading.Tasks;

using TestProject.Entities;
using TestProject.Resources;
using TestProject.Services.DataHandleResults;
using TestProject.Services.Helpers.Interfaces;
using TestProject.Services.Interfaces;
using TestProject.Services.Repositories.Interfaces;

namespace TestProject.Services
{
    public class LoginService : ILoginService
    {
        private readonly IUserRepository _userRepository;

        private readonly IUserStorageHelper _storage;

        private readonly IDialogsHelper _dialogsHelper;

        public LoginService(IUserRepository userRepository, IUserStorageHelper storage, IDialogsHelper dialogsHelper)
        {
            _userRepository = userRepository;
            _storage = storage;
            _dialogsHelper = dialogsHelper;
        }

        // TODO: Think about better names for users.
        public async Task Login(LoginResult result)
        {
            User enteredUser = result.Data;

            User currentUser = await _userRepository.GetUser(enteredUser.Name, enteredUser.Password);
            if(currentUser == null)
            {
                _dialogsHelper.DisplayAlertMessage(Strings.LoginErrorMessage);
                return;
            }

            await _storage.Save(currentUser.Id);
            result.IsSucceded = true;
        }
    }
}
