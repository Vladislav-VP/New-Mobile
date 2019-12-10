using Microsoft.AspNetCore.Identity;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using DataAccess.Repositories.Interfaces;
using Entities;
using Services.Interfaces;
using ViewModels.Api.User;

namespace Services.Api
{
    public class UsersApiService : BaseApiService<User>, IUsersApiService
    {
        private readonly IUserRepository _userRepository;
        private readonly IImageService _imageService;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ISecurityService _securityService;

        public UsersApiService(IUserRepository userRepository, IImageService imageService,
            UserManager<User> userManager, SignInManager<User> signInManager, ISecurityService securityService) : base()
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _signInManager = signInManager;
            _imageService = imageService;
            _securityService = securityService;
        }

        public async Task<ResponseRegisterUserApiView> Register(RequestRegisterUserApiView userToRegister)
        {
            var response = new ResponseRegisterUserApiView();
            var user = new User
            {
                UserName = userToRegister.Name
            };
            var result = new IdentityResult();
            using (_userManager)
            {
                result = await _userManager.CreateAsync(user, userToRegister.Password);
            }            
            response.IsSuccess = result.Succeeded;
            response.Message = "User was succesfully registered";
            return response;
        }

        public async Task<ResponseLoginUserApiView> Login(RequestLoginUserApiView userRequest, ClaimsPrincipal principal)
        {
            var response = new ResponseLoginUserApiView();
            SignInResult result = await _signInManager.PasswordSignInAsync(userRequest.Name, userRequest.Password, true, false);
            response.IsSuccess = result.Succeeded;
            if (!result.Succeeded)
            {
                response.Message = result.ToString();
                return response;
            }
            User user = null;
            using (_userManager)
            {
                user = _userManager.Users.SingleOrDefault(u => u.UserName == userRequest.Name);
            }
            if (user == null)
            {
                response.Message = "User was not found";
                response.IsSuccess = false;
                return response;
            }
            var token = await _securityService.GenerateJwtToken(userRequest.Name, user);
            response.Token = token.ToString();
            return response;
        }

        public GetProfileImageUserApiView GetUserWithPhoto(ClaimsPrincipal principal)
        {
            
            string id;
            using (_userManager)
            {
                id = _userManager.GetUserId(principal);
            }
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

        public async Task<ResponseEditProfileImageUserApiView> ReplaceProfilePhoto(RequestEditProfileImageUserApiView user,
            string imageUrl, ClaimsPrincipal principal)
        {
            if (user.ImageBytes != null)
            {
                _imageService.UploadImage(imageUrl, user.ImageBytes);
                user.ImageUrl = imageUrl;
            }
            using (_userManager)
            {
                string id = _userManager.GetUserId(principal);
                User userToModify = _userRepository.FindById(id);
                if (File.Exists(userToModify.ImageUrl))
                {
                    File.Delete(userToModify.ImageUrl);
                }
                userToModify.ImageUrl = user.ImageUrl;
                await _userManager.UpdateAsync(userToModify);
            }            
            var response = new ResponseEditProfileImageUserApiView
            {
                ImageBytes = user.ImageBytes
            };
            return response;
        }

        public async Task<ResponseEditNameUserApiView> EditUserName(RequestEditNameUserApiView user, ClaimsPrincipal principal)
        {
            var response = new ResponseEditNameUserApiView();
            var result = new IdentityResult();
            using (_userManager)
            {
                string id = _userManager.GetUserId(principal);
                User retrievedUser = _userRepository.FindById(id);
                retrievedUser.UserName = user.Name;
                result = await _userManager.UpdateAsync(retrievedUser);
            }
            response.IsSuccess = result.Succeeded;
            if (!result.Succeeded)
            {
                response.Message = result.Errors.FirstOrDefault()?.Description;
            }
            return response;
        }

        public string GetUserName(ClaimsPrincipal principal)
        {
            string id;
            using (_userManager)
            {
                id = _userManager.GetUserId(principal);
            }
            User user = _userRepository.FindById(id);
            return user.UserName;
        }

        public ResponseChangePasswordUserApiView ChangePassword(RequestChangePasswordUserApiView user)
        {            
            User userToModify = _userRepository.FindById(user.Id);
            //user.OldPassword = userToModify.Password;
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
            //userToModify.Password = user.NewPassword;
            Update(userToModify);
            response.IsSuccess = true;
            response.Message = "Password successfully changed";
            return response;
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<DeleteUserApiView> Delete(ClaimsPrincipal principal)
        {
            var response = new DeleteUserApiView();
            var result = new IdentityResult();
            using (_userManager)
            {

                User user = await _userManager.GetUserAsync(principal);
                if (File.Exists(user.ImageUrl))
                {
                    File.Delete(user.ImageUrl);
                }
                result = await _userManager.DeleteAsync(user);
            }
            response.IsSuccess = result.Succeeded;
            response.Message = "Account deleted";
            return response;
        }
    }
}
