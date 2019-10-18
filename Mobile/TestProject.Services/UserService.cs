using System.Threading.Tasks;

using TestProject.Entities;
using TestProject.Resources;
using TestProject.Services.DataHandleResults;
using TestProject.Services.Helpers;
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

        public async Task<DataHandleResult<EditPasswordHelper>> ChangePassword(User user,
            string oldPassword, string newPassword, string newPasswordConfirmation)
        {
            var editPasswordHelper = new EditPasswordHelper
            {
                OldPassword = user.Password,
                OldPasswordConfirmation = oldPassword,
                NewPassword = newPassword,
                NewPasswordConfirmation = newPasswordConfirmation
            };

            var result = new DataHandleResult<EditPasswordHelper>
            {
                Data = editPasswordHelper
            };

            result.IsSucceded = _validationHelper.IsObjectValid(result.Data, nameof(result.Data.OldPasswordConfirmation))
                && _validationHelper.IsObjectValid(result.Data, nameof(result.Data.NewPassword))
                && _validationHelper.IsObjectValid(result.Data, nameof(result.Data.NewPasswordConfirmation));

            if (result.IsSucceded)
            {
                user.Password = newPassword;
                await _userRepository.Update(user);
            }

            return result;
        }

        public async Task<DataHandleResult<User>> EditUsername(User user, string newUserName)
        {
            var result = new DataHandleResult<User> { Data = user };

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

        public async Task<DataHandleResult<User>> Login(User user)
        {
            var result = new DataHandleResult<User> { Data = user };

            User currentUser = await _userRepository.GetUser(user.Name, user.Password);
            if (currentUser == null)
            {
                _dialogsHelper.DisplayAlertMessage(Strings.LoginErrorMessage);
                return result;
            }

            await _storage.Save(currentUser.Id);
            result.IsSucceded = true;

            return result;
        }

        public async Task<DataHandleResult<User>> RegisterUser(User user)
        {
            var result = new DataHandleResult<User> { Data = user };

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
