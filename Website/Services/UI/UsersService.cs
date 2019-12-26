using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using Configurations;
using Constants;
using DataAccess.Repositories.Interfaces;
using Entities;
using Services.Interfaces;
using ViewModels;
using ViewModels.UI.User;

namespace Services.UI
{
    public class UsersService : IUsersService
    {
        private readonly IUserRepository _userRepository;
        private readonly IValidationService _validationService;
        private readonly IImageService _imageService;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMailService _mailService;

        public UsersService(IUserRepository userRepository, IImageService imageService, IValidationService validationService,
            UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration, IMailService mailService)
        {
            _userRepository = userRepository;
            _imageService = imageService;
            _validationService = validationService;
            _userManager = userManager;
            _signInManager = signInManager;
            _mailService = mailService;
        }
        
        public async Task<ResponseLoginUserView> Login(RequestLoginUserView user, ClaimsPrincipal principal)
        {
            var responseLogin = new ResponseLoginUserView();
            ResponseValidation responseValidation = _validationService.IsValid(user);
            if (!responseValidation.IsSuccess)
            {
                responseLogin.Message = responseValidation.Message;
                return responseLogin;
            }

            SignInResult result = await _signInManager.PasswordSignInAsync(user.Name, user.Password, true, false);
            responseLogin.IsSuccess = result.Succeeded;
            if (!result.Succeeded)
            {
                return responseLogin;
            }
            User retrievedUser = _userRepository.FindByName(user.Name);
            using (_userManager)
            {
                bool isConfirmed = await _userManager.IsEmailConfirmedAsync(retrievedUser);
                if (!isConfirmed)
                {
                    responseLogin.IsSuccess = false;
                }
            }
            return responseLogin;
        }

        public async Task<HomeInfoUserView> GetUserHomeInfo(ClaimsPrincipal principal)
        {
            User retrievedUser = null;
            using (_userManager)
            {
                retrievedUser = await _userManager.GetUserAsync(principal);
            }            
            if (retrievedUser == null)
            {
                return null;
            }
            var user = new HomeInfoUserView()
            {
                Name = retrievedUser.UserName
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

        public async Task<ResponseCreateUserView> Register(RequestCreateUserView user)
        {
            var responseRegister = new ResponseCreateUserView();
            var newUser = new User
            {
                UserName = user.Name,
                Email = user.Email
            };
            var result = new IdentityResult();
            using (_userManager)
            {
                result = await _userManager.CreateAsync(newUser, user.Password);
                newUser.ConfirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
                string url = $"{ConfigSettings.Scheme}/{ConfigSettings.Domain}/user/ConfirmEmail?userId={newUser.Id}";
                string body = $"To confirm your email, follow this <a href='{url}'>link</a>";
                await _mailService.SendEmailAsync(newUser.Email, "Confirmation", body);
                await _userManager.UpdateAsync(newUser);
            }
            if (!result.Succeeded)
            {
                responseRegister.Message = result.Errors.FirstOrDefault()?.Description;
            }
            responseRegister.IsSuccess = result.Succeeded;
            return responseRegister;
        }

        public async Task<SettingsUserView> GetUserSettings(ClaimsPrincipal principal)
        {
            User retrievedUser = null;
            using (_userManager)
            {
                retrievedUser = await _userManager.GetUserAsync(principal);
            }
            if (retrievedUser == null)
            {
                return null;
            }
            var user = new SettingsUserView
            {
                Name = retrievedUser.UserName,
                Email = retrievedUser.Email
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

        public async Task<ResponseChangeNameUserView> ChangeUsername(RequestChangeNameUserView user, ClaimsPrincipal principal)
        {
            var responseChange = new ResponseChangeNameUserView();
            var result = new IdentityResult();
            using (_userManager)
            {
                User retrievedUser = await _userManager.GetUserAsync(principal);
                retrievedUser.UserName = user.Name;
                result = await _userManager.UpdateAsync(retrievedUser);
            }
            responseChange.IsSuccess = result.Succeeded;
            return responseChange;
        }

        public async Task<ResponseChangePasswordUserView> ChangePassword(RequestChangePasswordUserView user, ClaimsPrincipal principal)
        {
            var responseChange = new ResponseChangePasswordUserView();
            ResponseValidation responseValidation = _validationService.IsValid(user);
            if (!responseValidation.IsSuccess)
            {
                responseChange.Message = responseValidation.Message;
                return responseChange;
            }
            var result = new IdentityResult();
            using (_userManager)
            {
                User retrievedUser = await _userManager.GetUserAsync(principal);
                result = await _userManager.ChangePasswordAsync(retrievedUser, user.OldPassword, user.NewPassword);
            }            
            responseChange.IsSuccess = result.Succeeded;
            return responseChange;
        }

        public async Task<ResponseChangeProfilePhotoUserView> ChangeProfilePhoto(RequestChangeProfilePhotoUserView user, ClaimsPrincipal principal)
        {
            var response = new ResponseChangeProfilePhotoUserView();
            _imageService.UploadImage(user.ImageUrl, user.ImageBytes);
            User retrievedUser = null;
            using (_userManager)
            {
                retrievedUser = await _userManager.GetUserAsync(principal);
            }
            if (File.Exists(retrievedUser.ImageUrl))
            {
                File.Delete(retrievedUser.ImageUrl);
            }
            retrievedUser.ImageUrl = user.ImageUrl;
            _userRepository.Update(retrievedUser);
            response.IsSuccess = true;
            return response;
        }

        public async Task DeleteAccount(ClaimsPrincipal principal)
        {
            using (_userManager)
            {
                User user = await _userManager.GetUserAsync(principal);
                if (File.Exists(user.ImageUrl))
                {
                    File.Delete(user.ImageUrl);
                }
                await _userManager.DeleteAsync(user);
            }            
        }

        public async Task RemoveProfilePhoto(ClaimsPrincipal principal)
        {
            User user = null;
            using (_userManager)
            {
                user = await _userManager.GetUserAsync(principal);
            }
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

        public async Task Logout(ClaimsPrincipal principal)
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<ResponseResetPasswordUserView> ResetPassword(RequestResetPasswordUserView requestReset)
        {
            var responseReset = new ResponseResetPasswordUserView();
            ResponseValidation responseValidation = _validationService.IsValid(requestReset);
            if (!responseValidation.IsSuccess)
            {
                responseReset.Message = responseValidation.Message;
                return responseReset;
            }
            var result = new IdentityResult();
            using (_userManager)
            {
                User user = await _userManager.FindByEmailAsync(requestReset.Email);
                result = await _userManager.ResetPasswordAsync(user, user.ConfirmationToken, requestReset.NewPassword);
            }
            responseReset.IsSuccess = result.Succeeded;
            if (!result.Succeeded)
            {
                responseReset.Message = result.Errors.FirstOrDefault()?.Description;
            }
            return responseReset;
        }

        public async Task<ConfirmEmailUserView> ConfirmEmail(string userId)
        {
            var confirmation = new ConfirmEmailUserView();
            var result = new IdentityResult();
            using (_userManager)
            {
                User user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    confirmation.Message = "User not found";
                    return confirmation;
                }
                result = await _userManager.ConfirmEmailAsync(user, user.ConfirmationToken);
            }
            confirmation.IsSuccess = result.Succeeded;
            if (!result.Succeeded)
            {
                confirmation.Message = result.Errors.FirstOrDefault()?.Description;
            }
            return confirmation;
        }

        public async Task<ResponseChangeEmailUserView> ChangeEmail(RequestChangeEmailUserView requestChangeEmail, ClaimsPrincipal principal)
        {
            var response = new ResponseChangeEmailUserView();
            using (_userManager)
            {
                User user = await _userManager.GetUserAsync(principal);
                string token = await _userManager.GenerateChangeEmailTokenAsync(user, requestChangeEmail.Email);
                response.Token = token;
                string url = $"{ConfigSettings.Scheme}/{ConfigSettings.Domain}/user/ConfirmChangeEmail?userId={user.Id}&email={requestChangeEmail.Email}";
                string body = $"To confirm your email, follow this <a href='{url}'>link</a>";
                await _mailService.SendEmailAsync(requestChangeEmail.Email, "Confirmation", body);
                user.ConfirmationToken = token;
                await _userManager.UpdateAsync(user);
            }
            response.IsSuccess = true;
            return response;
        }

        public async Task<ConfirmChangeEmailUserView> ConfirmChangeEmail(string userId, string email)
        {
            var confirmation = new ConfirmChangeEmailUserView();
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

        public async Task<ResponseForgotPasswordUserView> ForgotPassword(RequestForgotPasswordUserView user)
        {
            var response = new ResponseForgotPasswordUserView
            {
                Message = $"Recovery link was to sent to '{user.Email}'"
            };
            using (_userManager)
            {
                User retrievedUser = await _userManager.FindByEmailAsync(user.Email);
                if (retrievedUser == null)
                {
                    return response;
                }
                bool confirmed = await _userManager.IsEmailConfirmedAsync(retrievedUser);
                if (!confirmed)
                {
                    return response;
                }
                string token = await _userManager.GeneratePasswordResetTokenAsync(retrievedUser);
                string url = $"{ConfigSettings.Scheme}/{ConfigSettings.Domain}/User/ResetPassword?email={user.Email}";
                string body = $"To reset you password, follow this <a href='{url}'>link</a>";
                await _mailService.SendEmailAsync(user.Email, "Confirmation", body);
                retrievedUser.ConfirmationToken = token;
                await _userManager.UpdateAsync(retrievedUser);
            }
            response.IsSuccess = true;
            return response;
        }

        private string RewriteImageUrl(string oldUrl)
        {
            int startIndex = oldUrl.LastIndexOf('\\');
            string imageName = oldUrl.Substring(startIndex).Replace('\\', '/');
            return $"~/ProfileImages{imageName}";
        }
    }
}
