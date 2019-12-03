using System.IO;

using Constants;
using DataAccess.Repositories;
using Entities;
using ViewModels;
using ViewModels.UI.Home;
using ViewModels.UI.User;

namespace Services.UI
{
    public class UsersService
    {
        private readonly UserRepository _userRepository;
        private readonly ValidationService _validationService;
        private readonly ImageService _imageService;

        public UsersService()
        {
            _userRepository = new UserRepository();
            _validationService = new ValidationService();
            _imageService = new ImageService();
        }

        public ResponseLoginHomeView Login(RequestLoginHomeView user)
        {
            var responseLogin = new ResponseLoginHomeView();
            ResponseValidation responseValidation = _validationService.IsValid(user);
            if (!responseValidation.IsSuccess)
            {
                responseLogin.Message = responseValidation.Message;
                return responseLogin;
            }
            User retrievedUser = _userRepository.Find(user.Name, user.Password);
            if (retrievedUser == null)
            {
                responseLogin.Message = "Incorrect username or password";
                return responseLogin;
            }
            user.Id = retrievedUser.Id;
            responseLogin.IsSuccess = true;
            return responseLogin;
        }

        public HomeInfoUserView GetUserHomeInfo(string id)
        {
            User retrievedUser = _userRepository.FindById(id);
            if (retrievedUser == null)
            {
                return null;
            }
            var user = new HomeInfoUserView()
            {
                Id = id,
                Name = retrievedUser.Name
            };
            if (string.IsNullOrEmpty(retrievedUser.ImageUrl))
            {
                user.ImageUrl = UserConstants.ProfilePlaceholderUrl;
            }
            if (!string.IsNullOrEmpty(retrievedUser.ImageUrl))
            {
                user.ImageUrl = RewriteImageUrl(retrievedUser.ImageUrl);
            }
            return user;
        }

        public ResponseCreateUserView Register(RequestCreateUserView user)
        {
            var responseRegister = new ResponseCreateUserView();
            ResponseValidation responseValidation = _validationService.IsValid(user);
            if (!responseValidation.IsSuccess)
            {
                responseRegister.Message = responseValidation.Message;
                return responseRegister;
            }
            User retrievedUser = _userRepository.FindById(user.Name);
            if (retrievedUser != null)
            {
                responseRegister.Message = "User with this name already exists";
                return responseRegister;
            }
            var newUser = new User
            {
                Name = user.Name,
                Password = user.Password
            };
            _userRepository.Insert(newUser);
            responseRegister.IsSuccess = true;
            return responseRegister;
        }

        public SettingsUserView GetUserSettings(string id)
        {
            User retrievedUser = _userRepository.FindById(id);
            if (retrievedUser == null)
            {
                return null;
            }
            var user = new SettingsUserView
            {
                Id = retrievedUser.Id,
                Name = retrievedUser.Name,
            };
            if (string.IsNullOrEmpty(retrievedUser.ImageUrl))
            {
                user.ImageUrl = UserConstants.ProfilePlaceholderUrl;
            }
            if (!string.IsNullOrEmpty(retrievedUser.ImageUrl))
            {
                user.ImageUrl = RewriteImageUrl(retrievedUser.ImageUrl);
            }
            return user;
        }

        public ResponseChangeNameUserView ChangeUsername(RequestChangeNameUserView user)
        {
            var responseChange = new ResponseChangeNameUserView();
            ResponseValidation responseValidation = _validationService.IsValid(user);
            if (!responseValidation.IsSuccess)
            {
                responseChange.Message = responseValidation.Message;
                return responseChange;
            }
            User retrievedUser = _userRepository.FindById(user.Name);
            if (retrievedUser != null)
            {
                responseChange.Message = "User with this name already exists";
                return responseChange;
            }
            User userToModify = _userRepository.FindById(user.Id);
            userToModify.Name = user.Name;
            _userRepository.Update(userToModify);
            responseChange.IsSuccess = true;
            return responseChange;
        }

        public ResponseChangePasswordUserView ChangePassword(RequestChangePasswordUserView user)
        {
            var responseChange = new ResponseChangePasswordUserView();
            User retrievedUser = _userRepository.FindById(user.Id);
            user.OldPassword = retrievedUser.Password;
            ResponseValidation responseValidation = _validationService.IsValid(user);
            if (!responseValidation.IsSuccess)
            {
                responseChange.Message = responseValidation.Message;
                return responseChange;
            }
            retrievedUser.Password = user.NewPassword;
            _userRepository.Update(retrievedUser);
            responseChange.IsSuccess = true;
            return responseChange;
        }

        public ResponseChangeProfilePhotoUserView ChangeProfilePhoto(RequestChangeProfilePhotoUserView user)
        {
            var response = new ResponseChangeProfilePhotoUserView();
            _imageService.UploadImage(user.ImageUrl, user.ImageBytes);
            User retrievedUser = _userRepository.FindById(user.Id);
            if (File.Exists(retrievedUser.ImageUrl))
            {
                File.Delete(retrievedUser.ImageUrl);
            }
            retrievedUser.ImageUrl = user.ImageUrl;
            _userRepository.Update(retrievedUser);
            response.IsSuccess = true;
            return response;
        }

        public void DeleteAccount(string id)
        {
            User user = _userRepository.FindById(id);
            if (File.Exists(user.ImageUrl))
            {
                File.Delete(user.ImageUrl);
            }
            _userRepository.Delete(id);
        }

        public void RemoveProfilePhoto(string id)
        {
            User user = _userRepository.FindById(id);
            if (user.ImageUrl == null)
            {
                return;
            }
            if (File.Exists(user.ImageUrl))
            {
                File.Delete(user.ImageUrl);
            }
            user.ImageUrl = null;
            _userRepository.Update(user);
        }

        private string RewriteImageUrl(string oldUrl)
        {
            int startIndex = oldUrl.LastIndexOf('\\');
            string imageName = oldUrl.Substring(startIndex).Replace('\\', '/');
            return $"~/ProfileImages{imageName}";
        }
    }
}
