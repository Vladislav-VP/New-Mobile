using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TestProject.ApiModels.User;
using TestProject.Entities;
using TestProject.Resources;
using TestProject.Services.DataHandleResults;
using TestProject.Services.Helpers;
using TestProject.Services.Helpers.Interfaces;
using TestProject.Services.Interfaces;
using TestProject.Services.Repositories.Interfaces;

namespace TestProject.Services
{
    public class UserService : BaseApiService, IUserService
    {
        private readonly IValidationHelper _validationHelper;

        private readonly IUserRepository _userRepository;

        private readonly IUserStorageHelper _storage;

        private readonly IDialogsHelper _dialogsHelper;

        private readonly IPhotoEditHelper _photoEditHelper;

        public UserService(IValidationHelper validationHelper, IDialogsHelper dialogsHelper,
            IUserRepository userRepository, IUserStorageHelper storage, IPhotoEditHelper photoEditHelper)
        {
            _url = "http://10.10.3.215:3000/api/userapi";
            _validationHelper = validationHelper;
            _userRepository = userRepository;
            _storage = storage;
            _dialogsHelper = dialogsHelper;
            _photoEditHelper = photoEditHelper;
        }

        public Task<DataHandleResult<EditPasswordHelper>> ChangePassword
            (int userId, string oldPassword, string newPassword, string newPasswordConfirmation)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseEditProfileImageUserApiModel> EditProfilePhoto(RequestEditProfileImageUserApiModel user)
        {
            var response = new ResponseEditProfileImageUserApiModel()
            {
                ImageBytes = user.ImageBytes
            };            
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
                return response;
            }
            user.ImageBytes = await optionResultPairs[option]();
            response = await Post<RequestEditProfileImageUserApiModel, ResponseEditProfileImageUserApiModel>
                (user, $"{_url}/EditProfileImage");

            return response;
        }

        public async Task<ResponseEditNameUserApiModel> EditUsername(RequestEditNameUserApiModel user)
        {
            var response = new ResponseEditNameUserApiModel();
            bool isUserNameValid = _validationHelper.IsObjectValid(user);
            if (!isUserNameValid)
            {
                return response;
            }
            response = await Post<RequestEditNameUserApiModel, ResponseEditNameUserApiModel>(user, $"{_url}/EditName");
            if (!response.IsSuccess)
            {
                _dialogsHelper.DisplayAlertMessage(response.Message);
                return response;
            }
            response.IsSuccess = true;
            return response;
        }

        public new Task<TodoItem> Get(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetUserName(int id)
        {
            string name = await Get<string>(id, $"{_url}/GetUserName/{id}");
            return name;
        }

        public async Task<GetProfileImageUserApiModel> GetUserWithImage(int id)
        {
            GetProfileImageUserApiModel user = await Get<GetProfileImageUserApiModel>(id, $"{_url}/GetProfileImage/{id}");
            return user;
        }

        public async Task<ResponseLoginUserApiModel> Login(RequestLoginUserApiModel user)
        {
            ResponseLoginUserApiModel response = await Post<RequestLoginUserApiModel, ResponseLoginUserApiModel>
                (user, $"{_url}/username={user.Name}/password={user.Password}");
            if (!response.IsSuccess)
            {
                _dialogsHelper.DisplayAlertMessage(response.Message);
                return response;
            }

            await _storage.Save(response.Id);
            response.IsSuccess = true;

            return response;
        }

        public async Task<ResponseRegisterUserApiModel> RegisterUser(RequestRegisterUserApiModel user)
        {
            var response = new ResponseRegisterUserApiModel();
            bool isUserNameValid = _validationHelper.IsObjectValid(user, nameof(user.Name));
            bool isPasswordValid = _validationHelper.IsObjectValid(user, nameof(user.Password));
            if (!isUserNameValid || !isPasswordValid)
            {
                response.Message = "Invalid format of credentials";
                return response;
            }
            response = await Post<RequestRegisterUserApiModel, ResponseRegisterUserApiModel>(user, $"{_url}/Register");
            if (!response.IsSuccess)
            {
                _dialogsHelper.DisplayAlertMessage(response.Message);
            }
            return response;
        }

        //public async Task<DataHandleResult<EditPasswordHelper>> ChangePassword(int userId,
        //    string oldPassword, string newPassword, string newPasswordConfirmation)
        //{
        //    T user = await Get(userId);
        //    var editPasswordHelper = new EditPasswordHelper
        //    {
        //        OldPassword = user.Password,
        //        OldPasswordConfirmation = oldPassword,
        //        NewPassword = newPassword,
        //        NewPasswordConfirmation = newPasswordConfirmation
        //    };

        //    var result = new DataHandleResult<EditPasswordHelper>
        //    {
        //        Data = editPasswordHelper
        //    };

        //    result.IsSucceded = _validationHelper.IsObjectValid(result.Data, nameof(result.Data.OldPasswordConfirmation))
        //        && _validationHelper.IsObjectValid(result.Data, nameof(result.Data.NewPassword))
        //        && _validationHelper.IsObjectValid(result.Data, nameof(result.Data.NewPasswordConfirmation));

        //    if (result.IsSucceded)
        //    {                
        //        user.Password = newPassword;
        //        await Update(user);
        //        //await _userRepository.Update(user);
        //    }

        //    return result;
        //}

        //public async Task<DataHandleResult<T>> EditUsername(T user, string newUserName)
        //{
        //    var result = new DataHandleResult<T> { Data = user };

        //    var userToCheck = new T
        //    {
        //        Name = newUserName,
        //        Password = result.Data.Password
        //    };

        //    bool isUserNameValid = _validationHelper.IsObjectValid(userToCheck);
        //    if (!isUserNameValid)
        //    {
        //        return result;
        //    }

        //    //User retrievedUser = await _userRepository.GetUser(newUserName);
        //    T retrievedUser = await Get(user.Name);
        //    if (retrievedUser != null && retrievedUser.Id != user.Id)
        //    {
        //        _dialogsHelper.DisplayAlertMessage(Strings.UserAlreadyExistsMessage);
        //        return result;
        //    }

        //    //result.Data.Name = newUserName;
        //    //await _userRepository.Update(result.Data);
        //    retrievedUser.Name = newUserName;
        //    await Update(retrievedUser);
        //    result.IsSucceded = true;

        //    return result;
        //}

        //public async Task<T> Get(string username)
        //{
        //    HttpClient client = GetClient();
        //    string requestUri = $"{_url}/username={username}";
        //    string result = await client.GetStringAsync(requestUri);
        //    T user = JsonConvert.DeserializeObject<T>(result);
        //    return user;
        //}        

        //public new async Task<T> Delete(int id)
        //{
        //    _storage.Clear();
        //    return await base.Delete(id);
        //}       
    }
}
