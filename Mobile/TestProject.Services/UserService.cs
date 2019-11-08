using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
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
    public class UserService : BaseApiService<User>, IUserService
    {
        private readonly IValidationHelper _validationHelper;

        private readonly IUserRepository _userRepository;

        private readonly IUserStorageHelper _storage;

        private readonly IDialogsHelper _dialogsHelper;

        private readonly IPhotoEditHelper _photoEditHelper;

        public UserService(IValidationHelper validationHelper, IDialogsHelper dialogsHelper,
            IUserRepository userRepository, IUserStorageHelper storage, IPhotoEditHelper photoEditHelper)
        {
            _url = "http://10.10.3.215:3000/api/usersapi";
            _validationHelper = validationHelper;
            _userRepository = userRepository;
            _storage = storage;
            _dialogsHelper = dialogsHelper;
            _photoEditHelper = photoEditHelper;
        }

        public async Task<DataHandleResult<EditPasswordHelper>> ChangePassword(int userId,
            string oldPassword, string newPassword, string newPasswordConfirmation)
        {
            User user = await Get(userId);
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
                await Update(user);
                //await _userRepository.Update(user);
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

            //User retrievedUser = await _userRepository.GetUser(newUserName);
            User retrievedUser = await Get(result.Data.Name);
            if (retrievedUser != null && retrievedUser.Id != result.Data.Id)
            {
                _dialogsHelper.DisplayAlertMessage(Strings.UserAlreadyExistsMessage);
                return result;
            }

            //result.Data.Name = newUserName;
            //await _userRepository.Update(result.Data);
            retrievedUser.Name = newUserName;
            await Update(retrievedUser);
            result.IsSucceded = true;

            return result;
        }

        public async Task<User> Get(string username)
        {
            HttpClient client = GetClient();
            string requestUri = $"{_url}/username={username}";
            string result = await client.GetStringAsync(requestUri);
            User user = JsonConvert.DeserializeObject<User>(result);
            return user;
        }

        public async Task<DataHandleResult<User>> Login(User user)
        {
            var result = new DataHandleResult<User> { Data = user };

            //User currentUser = await _userRepository.GetUser(user.Name, user.Password);
            User currentUser = await Post(user, $"{_url}/username={user.Name}/password={user.Password}");
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
            await Post(result.Data, _url);
            
            result.IsSucceded = true;
            return result;
        }

        public new async Task<User> Delete(int id)
        {
            _storage.Clear();
            return await base.Delete(id);
        }

        public async Task EditProfilePhoto(User user)
        {
            string[] buttons =
                {
                    Strings.ChoosePicture,
                    Strings.TakePicture
                };

            Dictionary<string, Func<Task<byte[]>>> optionResultPairs =
                new Dictionary<string, Func<Task<byte[]>>>();
            optionResultPairs.Add(Strings.CancelText, null);
            optionResultPairs.Add(Strings.ChoosePicture, _photoEditHelper.PickPhoto);
            optionResultPairs.Add(Strings.TakePicture, _photoEditHelper.TakePhoto);
            optionResultPairs.Add(Strings.DeletePicture, _photoEditHelper.DeletePhoto);

            string option = await _dialogsHelper.ChooseOption(Strings.ProfilePhotoTitle,
                Strings.CancelText, Strings.DeletePicture, buttons: buttons);

            if (option == Strings.CancelText)
            {
                return;
            }
            user.ImageBytes = await optionResultPairs[option]();
            if (user.ImageBytes == null)
            {
                user.ImageUrl = null;
            }
            await Post(user, $"{_url}/EditProfileImage");
        }

        public async Task<User> GetUserWithImage()
        {
            int id = await _storage.Get();
            User user = await Get(id, $"{_url}/GetProfileImage/{id}");
            return user;
        }
    }
}
