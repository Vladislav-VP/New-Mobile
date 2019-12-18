using Microsoft.AspNetCore.Identity;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using Configurations;
using Entities;
using Services.Interfaces;
using ViewModels.Api;
using ViewModels.Api.User;

namespace Services.Api
{
    public class UsersApiService : BaseApiService<User>, IUsersApiService
    {
        private readonly IImageService _imageService;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ISecurityService _securityService;
        private readonly IMailService _mailService;

        public UsersApiService(IImageService imageService, UserManager<User> userManager,
            SignInManager<User> signInManager, ISecurityService securityService, IMailService mailService) : base()
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _imageService = imageService;
            _securityService = securityService;
            _mailService = mailService;
        }

        public async Task<ResponseRegisterUserApiView> Register(RequestRegisterUserApiView newUser)
        {
            var responseRegister = new ResponseRegisterUserApiView();
            var user = new User
            {
                UserName = newUser.UserName,
                Email = newUser.Email
            };
            var result = new IdentityResult();
            using (_userManager)
            {
                result = await _userManager.CreateAsync(user, newUser.Password);
                user.ConfirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                string url = $"{ConfigSettings.Scheme}/{ConfigSettings.Domain}/api/userapi/ConfirmEmail?userId={user.Id}";
                string body = $"To confirm your email, follow this <a href='{url}'>link</a>";
                await _mailService.SendEmailAsync(newUser.Email, "Confirmation", body);
                await _userManager.UpdateAsync(user);
            }            
            responseRegister.IsSuccess = result.Succeeded;
            if (!result.Succeeded)
            {
                responseRegister.Message = result.Errors.FirstOrDefault()?.Description;
                return responseRegister;
            }
            responseRegister.Message = "Confirmation link was sent on your email";
            return responseRegister;
        }

        public async Task<ResponseLoginUserApiView> Login(RequestLoginUserApiView userRequest, ClaimsPrincipal principal)
        {
            var response = new ResponseLoginUserApiView();
            SignInResult result = await _signInManager.PasswordSignInAsync(userRequest.UserName, userRequest.Password, true, false);            
            if (!result.Succeeded)
            {
                response.Message = "Incorrect user name or password";
                return response;
            }
            User user = null;
            using (_userManager)
            {
                user = _userManager.Users.SingleOrDefault(u => u.UserName == userRequest.UserName);
            }
            if (user != null && !user.EmailConfirmed)
            {
                response.Message = "Email is not confirmed";
                return response;
            }
            response.IsSuccess = result.Succeeded;
            TokenData tokenData = _securityService.GenerateTokens(user);
            response.AccessToken = tokenData.AccessToken;
            response.RefreshToken = tokenData.RefreshToken;
            response.TokenExpirationDate = tokenData.AccessTokenExpirationDate;
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
                UserName = user.UserName
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

        public async Task<ResponseEditUserInfoUserApiView> EditUserInfo(RequestEditUserInfoUserApiView user, ClaimsPrincipal principal)
        {
            var response = new ResponseEditUserInfoUserApiView();
            var result = new IdentityResult();
            using (_userManager)
            {
                User retrievedUser = await _userManager.GetUserAsync(principal);                
                retrievedUser.UserName = user.UserName;
                retrievedUser.Email = user.Email;
                result = await _userManager.UpdateAsync(retrievedUser);
            }
            response.IsSuccess = result.Succeeded;
            if (!result.Succeeded)
            {
                response.Message = result.Errors.FirstOrDefault()?.Description;
            }
            return response;
        }

        public async Task<GetUserInfoUserApiView> GetUserInfo(ClaimsPrincipal principal)
        {
            var user = new User();
            using (_userManager)
            {
                user = await _userManager.GetUserAsync(principal);
            }
            var userInfo = new GetUserInfoUserApiView
            {
                UserName = user.UserName,
                Email = user.Email
            };
            return userInfo;
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

        public async Task<ResponseRefreshAccessTokenUserApiView> RefreshToken(RequestRefreshAccessTokenUserApiView tokens)
        {
            var response = new ResponseRefreshAccessTokenUserApiView();

            response = await _securityService.RefreshToken(tokens);

            return response;
        }

        public async Task<ConfirmEmailUserApiView> ConfirmEmail(string userId)
        {
            var confirmation = new ConfirmEmailUserApiView();
            var result = new IdentityResult();
            using (_userManager)
            {
                User user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return confirmation;
                }
                result = await _userManager.ConfirmEmailAsync(user, user.ConfirmationToken);
            }
            confirmation.IsSuccess = result.Succeeded;
            return confirmation;
        }

        public async Task<ResponseChangeEmailUserApiView> ChangeEmail(RequestChangeEmailUserApiView requestChangeEmail, ClaimsPrincipal principal)
        {
            var response = new ResponseChangeEmailUserApiView();
            using (_userManager)
            {
                User user = await _userManager.GetUserAsync(principal);
                string token = await _userManager.GenerateChangeEmailTokenAsync(user, requestChangeEmail.Email);
                response.Token = token;
                string url = $"{ConfigSettings.Scheme}/{ConfigSettings.Domain}/api/userapi/ConfirmChangeEmail?userId={user.Id}&email={requestChangeEmail.Email}";
                string body = $"To confirm your email, follow this <a href='{url}'>link</a>";
                await _mailService.SendEmailAsync(requestChangeEmail.Email, "Confirmation", body);
                user.ConfirmationToken = token;
                await _userManager.UpdateAsync(user);
            }
            response.IsSuccess = true;
            response.Message = "Confirmation link was sent on new email";
            return response;
        }

        public async Task<ConfirmChangeEmailUserApiView> ConfirmChangeEmail(string userId, string email)
        {
            var confirmation = new ConfirmChangeEmailUserApiView();
            var result = new IdentityResult();
            using (_userManager)
            {
                User user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return confirmation;
                }
                result = await _userManager.ChangeEmailAsync(user, email, user.ConfirmationToken);
            }
            confirmation.IsSuccess = result.Succeeded;
            return confirmation;
        }
    }
}
