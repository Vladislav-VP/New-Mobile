using AutoMapper;
using DataAccess.Context;
using Entities;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Api;

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

        public GetByNameUserApiView Find(string username)
        {
            var response = new GetByNameUserApiView();
            User user = _userRepository.Find(username);
            if (user == null)
            {
                response.Message = "User with this name already exists";
                return null;
            }
            response.IsSuccess = true;
            response.Message = "User found";
            return response;
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
            GetByNameUserApiView retrievedUser = Find(userToRegister.Name);
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
            response.IsSuccess = true;
            response.Message = "User succesfully logged in";
            return response;
        }
    }
}
