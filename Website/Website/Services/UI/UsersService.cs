using DataAccess.Repositories;
using Entities;
using ViewModels;
using ViewModels.UI;

namespace Services.UI
{
    public class UsersService
    {
        private readonly UserRepository _userRepository;
        private readonly ValidationService _validationService;
        public UsersService()
        {
            _userRepository = new UserRepository();
            _validationService = new ValidationService();
        }

        public ResponseLoginUserView Login(RequestLoginUserView user)
        {
            var responseLogin = new ResponseLoginUserView();
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

        public HomeInfoUserView GetUserHomeInfo(int id)
        {
            User retrievedUser = _userRepository.Find(id);
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
                user.ImageUrl = HomeInfoUserView.ProfilePlaceholderUrl;
            }
            if (!string.IsNullOrEmpty(retrievedUser.ImageUrl))
            {
                user.ImageUrl = RewriteImageUrl(retrievedUser.ImageUrl);
            }
            return user;
        }

        public ResponseRegisterUserView Register(RequestRegisterUserView user)
        {
            var responseRegister = new ResponseRegisterUserView();
            ResponseValidation responseValidation = _validationService.IsValid(user);
            if (!responseValidation.IsSuccess)
            {
                responseRegister.Message = responseValidation.Message;
                return responseRegister;
            }
            User retrievedUser = _userRepository.Find(user.Name);
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

        public SettingsUserView GetUserSettings(int id)
        {
            User retrievedUser = _userRepository.Find(id);
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
                user.ImageUrl = HomeInfoUserView.ProfilePlaceholderUrl;
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
            User retrievedUser = _userRepository.Find(user.Name);
            if (retrievedUser != null)
            {
                responseChange.Message = "User with this name already exists";
                return responseChange;
            }
            User userToModify = _userRepository.Find(user.Id);
            userToModify.Name = user.Name;
            _userRepository.Update(userToModify);
            responseChange.IsSuccess = true;
            return responseChange;
        }

        public ResponseChangePasswordUserView ChangePassword(RequestChangePasswordUserView user)
        {
            var responseChange = new ResponseChangePasswordUserView();
            User retrievedUser = _userRepository.Find(user.Id);
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

        private string RewriteImageUrl(string oldUrl)
        {
            int startIndex = oldUrl.LastIndexOf('\\');
            string imageName = oldUrl.Substring(startIndex).Replace('\\', '/');
            return $"~/ProfileImages{imageName}";
        }
    }
}
