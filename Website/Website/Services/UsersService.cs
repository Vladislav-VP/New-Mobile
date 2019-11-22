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

        public User Find(string username)
        {
            User user = _userRepository.Find(username);
            return user;
        }

        public void Register(string username, string password)
        {
            var user = new User
            {
                Name = username,
                Password = password
            };
            // TODO: Add logic for validation.
            Insert(user);
        }

        public LoginUserApiView Login(string username, string password)
        {
            User user = _userRepository.Find(username, password);
            LoginUserApiView userForLogin = Mapper.Map<LoginUserApiView>(user);
            return userForLogin;
        }
    }
}
