using System.IO;

using DataAccess.Repositories.Interfaces;
using Entities;
using Services.Interfaces;
using ViewModels.Api.User;

namespace Services.Api
{
    public class UsersApiService : BaseApiService<User>, IUsersApiService
    {
        public readonly IUserRepository _userRepository;
        public readonly ImageService _imageService;

        public UsersApiService(IUserRepository userRepository) : base()
        {
            _userRepository = userRepository;
            _imageService = new ImageService();
        }

        public ResponseRegisterUserApiView Register(RequestRegisterUserApiView userToRegister)
        {
            var response = new ResponseRegisterUserApiView();
            if (userToRegister == null)
            {
                response.Message = "Something went wrong";
                return response;
            }
            if (string.IsNullOrEmpty(userToRegister.Name))
            {
                response.Message = "Username can not be emnpty";
                return response;
            }
            if (string.IsNullOrEmpty(userToRegister.Password))
            {
                response.Message = "Password can not be empty";
                return response;
            }
            User retrievedUser = _userRepository.FindByName(userToRegister.Name);
            if (retrievedUser != null)
            {
                response.Message = "User with this name already exists";
                return response;
            }
            var user = new User
            {
                UserName = userToRegister.Name,
                Password = userToRegister.Password
            };
            Insert(user);
            response.IsSuccess = true;
            response.Message = "User was succesfully registered";
            return response;
        }

        public ResponseLoginUserApiView Login(RequestLoginUserApiView userRequest)
        {
            User retrievedUser = _userRepository.Find(userRequest.Name, userRequest.Password);
            var response = new ResponseLoginUserApiView();
            if (retrievedUser == null)
            {
                response.Message = "Incorrect username or password";
                return response;
            }
            response.Id = retrievedUser.Id;
            response.IsSuccess = true;
            response.Message = "User succesfully logged in";
            return response;
        }

        public GetProfileImageUserApiView GetUserWithPhoto(string id)
        {
            User user = _userRepository.FindById(id);
            if (user == null)
            {
                return null;
            }
            var userWithPhoto = new GetProfileImageUserApiView
            {
                Id = user.Id,
                Name = user.UserName
            };
            if(string.IsNullOrEmpty(user.ImageUrl))
            {
                return userWithPhoto;
            }
            userWithPhoto.ImageBytes = _imageService.GetImage(user.ImageUrl);
            return userWithPhoto;
        }

        public ResponseEditProfileImageUserApiView ReplaceProfilePhoto(RequestEditProfileImageUserApiView user, string imageUrl)
        {
            if (user.ImageBytes != null)
            {
                _imageService.UploadImage(imageUrl, user.ImageBytes);
                user.ImageUrl = imageUrl;
            }
            User userToModify = _userRepository.FindById(user.Id);
            if (File.Exists(userToModify.ImageUrl))
            {
                File.Delete(userToModify.ImageUrl);
            }
            userToModify.ImageUrl = user.ImageUrl;
            Update(userToModify);
            var response = new ResponseEditProfileImageUserApiView
            {
                ImageBytes = user.ImageBytes
            };
            return response;
        }

        public ResponseEditNameUserApiView EditUserName(RequestEditNameUserApiView user)
        {
            var response = new ResponseEditNameUserApiView();
            if (string.IsNullOrEmpty(user.Name))
            {
                response.Message = "Username can not be emnpty";
                return response;
            }
            User retrievedUser = _userRepository.FindByName(user.Name);
            if (retrievedUser != null && retrievedUser.Id != user.Id)
            {
                response.Message = "User with this name already exists";
                return response;
            }
            retrievedUser = _userRepository.FindById(user.Id);
            retrievedUser.UserName = user.Name;
            Update(retrievedUser);
            response.IsSuccess = true;
            response.Message = "Username successfully changed";
            return response;
        }

        public string GetUserName(string id)
        {
            User user = _userRepository.FindById(id);
            return user.UserName;
        }

        public ResponseChangePasswordUserApiView ChangePassword(RequestChangePasswordUserApiView user)
        {            
            User userToModify = _userRepository.FindById(user.Id);
            user.OldPassword = userToModify.Password;
            var response = new ResponseChangePasswordUserApiView();
            if (user.OldPassword != user.OldPasswordConfirmation)
            {
                response.Message = "Incorrect old passsword";
                return response;
            }
            if (string.IsNullOrEmpty(user.NewPassword) || string.IsNullOrEmpty(user.NewPasswordConfirmation))
            {
                response.Message = "Password can not be empty";
                return response;
            }
            if (user.NewPassword != user.NewPasswordConfirmation)
            {
                response.Message = "Passwords do not correspond";
                return response;
            }
            userToModify.Password = user.NewPassword;
            Update(userToModify);
            response.IsSuccess = true;
            response.Message = "Password successfully changed";
            return response;
        }

        public new DeleteUserApiView Delete(int id)
        {
            var response = new DeleteUserApiView();
            base.Delete(id);
            response.IsSuccess = true;
            response.Message = "Account deleted";
            return response;
        }
    }
}
