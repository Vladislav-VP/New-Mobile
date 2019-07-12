using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using TestProject.Entities;
using TestProject.Services.Repositories.Interfaces;

namespace TestProject.Services.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IBaseRepository<User> _repository;

        public UserRepository()
        {
            _repository = new BaseRepository<User>();
        }

        public async Task Delete<T>(object pk)
        {
            await _repository.Delete<T>(pk);
        }

        public async Task<T> Find<T>(object pk) where T : class, new()
        {
            return await _repository.Find<T>(pk);
        }
        
        public async Task<IEnumerable<T>> GetAllObjects<T>() where T : class, new()
        {
            return await _repository.GetAllObjects<T>();
        }

        public async Task<bool> Insert(User user)
        {
            return await _repository.Insert(user);
        }

        public async Task Update(User user)
        {
            await _repository.Update(user);
        }

        public async Task<bool> UserExists(User user)
        {
            var users = await _repository.GetAllObjects<User>();
            return users.Contains(user);
        }

        public async Task<bool> UserExists(string userName)
        {
            var users = await _repository.GetAllObjects<User>();
            return (await FindUser(userName)) != null;
        }

        public async Task<User> FindUser(string userName)
        {
            var users = await _repository.GetAllObjects<User>();
            return users.Where(user => user.UserName == userName).FirstOrDefault();
        }
    }
}
