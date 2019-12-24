using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using TestProject.ApiModels.User;
using TestProject.Configurations;
using TestProject.Resources;
using TestProject.Services.Helpers.Interfaces;
using TestProject.Services.Interfaces;

namespace TestProject.Services
{
    public class UserService : BaseApiService, IUserService
    {
        private readonly IValidationHelper _validationHelper;

        private readonly IDialogsHelper _dialogsHelper;

        private readonly IPhotoEditHelper _photoEditHelper;

        public UserService(IValidationHelper validationHelper, IDialogsHelper dialogsHelper,
            IStorageHelper storage, IPhotoEditHelper photoEditHelper) : base(storage)
        {
            _url = "http://10.10.3.215:3000/api/userapi";
            _validationHelper = validationHelper;
            _storage = storage;
            _dialogsHelper = dialogsHelper;
            _photoEditHelper = photoEditHelper;
        }

        public async Task<ResponseChangePasswordUserApiModel> ChangePassword(RequestChangePasswordUserApiModel user)
        {
            var response = new ResponseChangePasswordUserApiModel();
            bool isValid = _validationHelper.IsObjectValid(user);
            if (!isValid)
            {
                return response;
            }
            response = await Post<RequestChangePasswordUserApiModel,
                ResponseChangePasswordUserApiModel>(user, $"{_url}/ChangePassword");
            if (!response.IsSuccess)
            {
                _dialogsHelper.DisplayAlertMessage(response.Message);
                return response;
            }
            return response;
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
            if (user.ImageBytes == null && option != Strings.DeletePicture)
            {
                return response;
            }
            response = await Post<RequestEditProfileImageUserApiModel, ResponseEditProfileImageUserApiModel>
                (user, $"{_url}/EditProfileImage");

            return response;
        }

        public async Task<ResponseEditUserInfoUserApiModel> EditUserName(RequestEditUserInfoUserApiModel user)
        {
            var response = new ResponseEditUserInfoUserApiModel();
            bool isValid = _validationHelper.IsObjectValid(user);
            if (!isValid)
            {
                return response;
            }
            response = await Post<RequestEditUserInfoUserApiModel, ResponseEditUserInfoUserApiModel>(user, $"{_url}/EditUserName");
            if (!response.IsSuccess)
            {
                _dialogsHelper.DisplayAlertMessage(response.Message);
                return response;
            }
            response.IsSuccess = true;
            return response;
        }

        public async Task<ResponseChangeEmailUserApiModel> ChangeEmail(RequestChangeEmailUserApiModel user)
        {
            var response = new ResponseChangeEmailUserApiModel();
            bool isValid = _validationHelper.IsObjectValid(user);
            if (!isValid)
            {
                return response;
            }
            response = await Post<RequestChangeEmailUserApiModel, ResponseChangeEmailUserApiModel>(user, $"{_url}/ChangeEmail");
            _dialogsHelper.DisplayAlertMessage(response.Message);
            return response;
        }

        public async Task<GetUserInfoUserApiModel> GetUserInfo()
        {
            GetUserInfoUserApiModel user = await Get<GetUserInfoUserApiModel>($"{_url}/GetUserInfo");
            return user;
        }

        public async Task<GetProfileImageUserApiModel> GetUserWithImage()
        {
            GetProfileImageUserApiModel user = await Get<GetProfileImageUserApiModel>($"{_url}/GetProfileImage");
            return user;
        }

        public async Task<ResponseLoginUserApiModel> Login(RequestLoginUserApiModel user)
        {
            var response = new ResponseLoginUserApiModel();
            bool isValid=_validationHelper.IsObjectValid(user, true);
            if (!isValid)
            {
                return response;
            }
            response = await Post<RequestLoginUserApiModel, ResponseLoginUserApiModel>
                (user, $"{_url}/Login", false);
            if (!response.IsSuccess)
            {
                _dialogsHelper.DisplayAlertMessage(response.Message);
                return response;
            }

            await _storage.Save(Constants.AccessTokenKey, response.AccessToken);
            await _storage.Save(Constants.RefreshTokenKey, response.RefreshToken);
            await _storage.Save(Constants.ExpirationDateKey, response.TokenExpirationDate.ToString());
            response.IsSuccess = true;

            return response;
        }

        public async Task<ResponseRegisterUserApiModel> RegisterUser(RequestRegisterUserApiModel user)
        {
            var response = new ResponseRegisterUserApiModel();
            bool isValid = _validationHelper.IsObjectValid(user);
            if (!isValid)
            {
                return response;
            }
            response = await Post<RequestRegisterUserApiModel, ResponseRegisterUserApiModel>
                (user, $"{_url}/Register", false);
            return response;
        }

        public async Task Logout()
        {
            HttpClient client = await GetClient(false);
            await client.PostAsync($"{_url}/Logout", null);
            _storage.Clear();
        }

        public async Task<DeleteUserApiModel> Delete()
        {
            DeleteUserApiModel response = await Delete<DeleteUserApiModel>();
            if (response.IsSuccess)
            {
                _storage.Clear();
            }
            return response;
        }

        public async Task<ResponseForgotPasswordUserApiModel> ForgotPassword(RequestForgotPasswordUserApiModel user)
        {
            var response = new ResponseForgotPasswordUserApiModel();
            bool isValid = _validationHelper.IsObjectValid(user);
            if (!isValid)
            {
                return response;
            }
            response = await Post<RequestForgotPasswordUserApiModel, ResponseForgotPasswordUserApiModel>
                (user, $"{_url}/ForgotPassword", false);
            response.IsSuccess = true;
            return response;
        }
    }
}
