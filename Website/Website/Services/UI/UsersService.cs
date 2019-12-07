using Microsoft.AspNetCore.Identity;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using Constants;
using DataAccess.Repositories.Interfaces;
using Entities;
using Services.Interfaces;
using ViewModels;
using ViewModels.UI.User;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Services.UI
{
    public class UsersService : IUsersService
    {
        private readonly IUserRepository _userRepository;
        private readonly IValidationService _validationService;
        private readonly IImageService _imageService;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;

        public UsersService(IUserRepository userRepository, IImageService imageService, IValidationService validationService,
            UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _imageService = imageService;
            _validationService = validationService;
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
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
            string id = _userManager.GetUserId(principal);
            User retrievedUser = _userRepository.FindByName(user.Name);
            object token = await GenerateJwtToken(retrievedUser.UserName, retrievedUser);
            user.Id = retrievedUser.Id;
            responseLogin.IsSuccess = result.Succeeded;
            return responseLogin;
        }

        public HomeInfoUserView GetUserHomeInfo(ClaimsPrincipal principal)
        {
            string id;
            bool signedIn = _signInManager.IsSignedIn(principal);
            using (_userManager)
            {
                id = _userManager.GetUserId(principal);
            }            
            User retrievedUser = _userRepository.FindById(id);
            if (retrievedUser == null)
            {
                return null;
            }
            var user = new HomeInfoUserView()
            {
                Id = retrievedUser.Id,
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
                UserName = user.Name
            };
            var result = new IdentityResult();
            using (_userManager)
            {
                result = await _userManager.CreateAsync(newUser, user.Password);
            }
            if (!result.Succeeded)
            {
                responseRegister.Message = result.Errors.FirstOrDefault()?.Description;
            }
            object token = await GenerateJwtToken(newUser.UserName, newUser);
            responseRegister.IsSuccess = result.Succeeded;
            return responseRegister;
        }

        public SettingsUserView GetUserSettings(ClaimsPrincipal principal)
        {
            string id;
            using (_userManager)
            {
                id = _userManager.GetUserId(principal);
            }            
            User retrievedUser = _userRepository.FindById(id);
            if (retrievedUser == null)
            {
                return null;
            }
            var user = new SettingsUserView
            {
                Id = retrievedUser.Id,
                Name = retrievedUser.UserName,
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
                string id = _userManager.GetUserId(principal);
                User retrievedUser = _userRepository.FindById(id);
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
                string id = _userManager.GetUserId(principal);
                User retrievedUser = _userRepository.FindById(id);
                result = await _userManager.ChangePasswordAsync(retrievedUser, user.OldPassword, user.NewPassword);
            }            
            responseChange.IsSuccess = result.Succeeded;
            return responseChange;
        }

        public ResponseChangeProfilePhotoUserView ChangeProfilePhoto(RequestChangeProfilePhotoUserView user, ClaimsPrincipal principal)
        {
            var response = new ResponseChangeProfilePhotoUserView();
            _imageService.UploadImage(user.ImageUrl, user.ImageBytes);
            string id;
            using (_userManager)
            {
                id = _userManager.GetUserId(principal);
            }            
            User retrievedUser = _userRepository.FindById(id);
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

        public void RemoveProfilePhoto(ClaimsPrincipal principal)
        {
            string id;
            using (_userManager)
            {
                id = _userManager.GetUserId(principal);
            }            
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

        public async Task Logout(ClaimsPrincipal principal)
        {
            await _signInManager.SignOutAsync();
        }

        private string RewriteImageUrl(string oldUrl)
        {
            int startIndex = oldUrl.LastIndexOf('\\');
            string imageName = oldUrl.Substring(startIndex).Replace('\\', '/');
            return $"~/ProfileImages{imageName}";
        }

        private async Task<object> GenerateJwtToken(string email, IdentityUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JwtExpireDays"]));

            var token = new JwtSecurityToken(
                _configuration["JwtIssuer"],
                _configuration["JwtIssuer"],
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }
}
