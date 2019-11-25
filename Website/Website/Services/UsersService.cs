using AutoMapper;
using DataAccess.Context;
using Entities;
using Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Api;
using ViewModels.Api.User;

namespace Services
{
    public class UsersService : BaseService<User>
    {
        public UserRepository _userRepository;

        public UsersService(TodoListContext context) : base(context)
        {
            _userRepository = new UserRepository(_context);
        }

        public void EditUser(User user)
        {
            User userToModify = _userRepository.Find(user.Id);
            userToModify.Name = user.Name;
            userToModify.Password = user.Password;
            _userRepository.Update(userToModify);
        }

        public User Find(string username, string password)
        {
            User user = _userRepository.Find(username, password);
            return user;
        }

        public User Find(string username)
        {
            User user = _userRepository.Find(username);
            return user;
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
            User retrievedUser = Find(userToRegister.Name);
            if (retrievedUser != null)
            {
                response.Message = "User with this name already exists";
                return response;
            }
            var user = new User
            {
                Name = userToRegister.Name,
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

        public GetProfileImageUserApiView GetUserWithPhoto(int id)
        {
            User user = FindById(id);
            var userWithPhoto = new GetProfileImageUserApiView
            {
                Id = user.Id,
                Name = user.Name
            };
            if(string.IsNullOrEmpty(user.ImageUrl))
            {
                return userWithPhoto;
            }
            // TODO : Move using block to separate method in separate service
            using (var imageFileStream = new FileStream(user.ImageUrl, FileMode.Open, FileAccess.ReadWrite))
            {
                using (var imageMemoryStream = new MemoryStream())
                {
                    imageFileStream.CopyTo(imageMemoryStream);
                    user.ImageBytes = imageMemoryStream.ToArray();
                }
            }
            userWithPhoto.ImageBytes = user.ImageBytes;
            return userWithPhoto;
        }

        public ResponseEditProfileImageUserApiView ReplaceProfilePhoto(RequestEditProfileImageUserApiView user, string imageUrl)
        {
            if (user.ImageBytes != null)
            {
                UploadProfilePhoto(imageUrl, user);
                user.ImageUrl = imageUrl;
            }
            User userToModify = FindById(user.Id);
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
            User retrievedUser = Find(user.Name);
            if (retrievedUser != null && retrievedUser.Id != user.Id)
            {
                response.Message = "User with this name already exists";
                return response;
            }
            retrievedUser = FindById(user.Id);
            retrievedUser.Name = user.Name;
            Update(retrievedUser);
            response.IsSuccess = true;
            response.Message = "Username successfully changed";
            return response;
        }

        public string GetUserName(int id)
        {
            User user = FindById(id);
            return user.Name;
        }

        private void UploadProfilePhoto(string imageUrl, RequestEditProfileImageUserApiView user)
        {
            // TODO : Move this method to separate service
            using (var imageStream = new MemoryStream(user.ImageBytes))
            {
                using (var imageFileStream = new FileStream(imageUrl, FileMode.Create))
                {
                    imageStream.CopyTo(imageFileStream);
                }
            }
        }
    }
}
