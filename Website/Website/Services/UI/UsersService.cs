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
            ValidationResponse validationResponse = _validationService.IsValid(user);
            if (!validationResponse.IsSuccess)
            {
                responseLogin.Message = validationResponse.Message;
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
            ValidationResponse validationResponse = _validationService.IsValid(user);
            if (!validationResponse.IsSuccess)
            {
                responseRegister.Message = validationResponse.Message;
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
            ValidationResponse validationResponse = _validationService.IsValid(user);
            if (!validationResponse.IsSuccess)
            {
                responseChange.Message = validationResponse.Message;
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

        private string RewriteImageUrl(string oldUrl)
        {
            int startIndex = oldUrl.LastIndexOf('\\');
            string imageName = oldUrl.Substring(startIndex).Replace('\\', '/');
            return $"~/ProfileImages{imageName}";
        }
    }
}
