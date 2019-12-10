using Microsoft.AspNetCore.Identity;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using Entities;
using Services.Interfaces;
using ViewModels.Api.User;

namespace Services.Api
{
    public class UsersApiService : BaseApiService<User>, IUsersApiService
    {
        private readonly IImageService _imageService;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ISecurityService _securityService;

        public UsersApiService(IImageService imageService, UserManager<User> userManager,
            SignInManager<User> signInManager, ISecurityService securityService) : base()
        {
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
            if (!result.Succeeded)
            {
                response.Message = result.Errors.FirstOrDefault()?.Description;
            }
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

        public async Task<GetProfileImageUserApiView> GetUserWithPhoto(ClaimsPrincipal principal)
        {            
            var user = new User();
            using (_userManager)
            {
                user = await _userManager.GetUserAsync(principal);
            }
            var userWithPhoto = new GetProfileImageUserApiView
            {
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
                User userToModify = await _userManager.GetUserAsync(principal);
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
                User retrievedUser = await _userManager.GetUserAsync(principal);                
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

        public async Task<string> GetUserName(ClaimsPrincipal principal)
        {
            var user = new User();
            using (_userManager)
            {
                user = await _userManager.GetUserAsync(principal);
            }
            return user.UserName;
        }

        public async Task<ResponseChangePasswordUserApiView> ChangePassword(RequestChangePasswordUserApiView user, ClaimsPrincipal principal)
        {
            var response = new ResponseChangePasswordUserApiView();
            var result = new IdentityResult();
            using (_userManager)
            {
                User retrievedUser = await _userManager.GetUserAsync(principal);
                result = await _userManager.ChangePasswordAsync(retrievedUser, user.OldPassword, user.NewPassword);
            }
            response.IsSuccess = result.Succeeded;
            if (!result.Succeeded)
            {
                response.Message = result.Errors.FirstOrDefault()?.Description;
            }
            return response;
        }

        public async Task Logout(ClaimsPrincipal principal)
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
