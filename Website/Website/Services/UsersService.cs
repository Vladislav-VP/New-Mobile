using DataAccess.Context;
using Entities;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class UsersService : BaseService<User>
    {
        public UserRepository _userRepository;

        public UsersService(TodoListContext context) : base(context)
        {
            _userRepository = new UserRepository(_context);
        }

        public void ChangeUsername(int id, string username)
        {

        }

        public void ChangePassword(int id, string password)
        {

        }

        public User Find(string username, string password)
        {
            User user = _userRepository.Find(username, password);
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
    }
}
