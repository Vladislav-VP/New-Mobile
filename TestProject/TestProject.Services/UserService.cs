using System.Threading.Tasks;

using TestProject.Entities;
using TestProject.Resources;
using TestProject.Services.DataHandleResults;
using TestProject.Services.Helpers.Interfaces;
using TestProject.Services.Interfaces;
using TestProject.Services.Repositories.Interfaces;

namespace TestProject.Services
{
    public class UserService : IUserService
    {
        private readonly IValidationHelper _validationHelper;

        private readonly IUserRepository _userRepository;

        private readonly IUserStorageHelper _storage;

        private readonly IDialogsHelper _dialogsHelper;

        public UserService(IValidationHelper validationHelper, IDialogsHelper dialogsHelper,
            IUserRepository userRepository, IUserStorageHelper storage)
        {
            _validationHelper = validationHelper;
            _userRepository = userRepository;
            _storage = storage;
            _dialogsHelper = dialogsHelper;
        }

        public async Task<EditPasswordResult> ChangePassword(User user,
            string oldPassword, string newPassword, string newPasswordConfirmation)
        {
            var result = new EditPasswordResult
            {
                Data = user,
                OldPasswordConfirmation = oldPassword,
                NewPassword = newPassword,
                NewPasswordConfirmation = newPasswordConfirmation
            };

            result.IsSucceded = _validationHelper.IsObjectValid(result, nameof(result.OldPasswordConfirmation))
                && _validationHelper.IsObjectValid(result, nameof(result.NewPassword))
                && _validationHelper.IsObjectValid(result, nameof(result.NewPasswordConfirmation));

            if (result.IsSucceded)
            {
                result.Data.Password = result.NewPassword;
                await _userRepository.Update(result.Data);
            }

            return result;
        }

        public async Task<EditUsernameResult> EditUsername(User user, string newUserName)
        {
            var result = new EditUsernameResult { Data = user };

            var userToCheck = new User
            {
                Name = newUserName,
                Password = result.Data.Password
            };

            bool isUserNameValid = _validationHelper.IsObjectValid(userToCheck);
            if (!isUserNameValid)
            {
                return result;
            }

            User retrievedUser = await _userRepository.GetUser(newUserName);
            if (retrievedUser != null && retrievedUser.Id != result.Data.Id)
            {
                _dialogsHelper.DisplayAlertMessage(Strings.UserAlreadyExistsMessage);
                return result;
            }

            result.Data.Name = newUserName;
            await _userRepository.Update(result.Data);
            result.IsSucceded = true;

            return result;
        }

        public async Task<LoginResult> Login(User user)
        {
            var result = new LoginResult { Data = user };
            User enteredUser = result.Data;

            User currentUser = await _userRepository.GetUser(enteredUser.Name, enteredUser.Password);
            if (currentUser == null)
            {
                _dialogsHelper.DisplayAlertMessage(Strings.LoginErrorMessage);
                return result;
            }

            await _storage.Save(currentUser.Id);
            result.IsSucceded = true;

            return result;
        }

        public async Task<RegistrationResult> RegisterUser(User user)
        {
            var result = new RegistrationResult { Data = user };

            bool isUserDataValid = _validationHelper.IsObjectValid(result.Data, nameof(result.Data.Name))
                && _validationHelper.IsObjectValid(result.Data, nameof(result.Data.Password));
            if (!isUserDataValid)
            {
                return result;
            }

            result.Data.Name = result.Data.Name.Trim();
            User retrievedUser = await _userRepository.GetUser(result.Data.Name);
            if (retrievedUser != null)
            {
                _dialogsHelper.DisplayAlertMessage(Strings.UserAlreadyExistsMessage);
                return result;
            }

            await AddUser(result.Data);
            result.IsSucceded = true;
            return result;
        }

        private async Task AddUser(User user)
        {
            await _userRepository.Insert(user);
            await _storage.Save(user.Id);
        }
    }
}
