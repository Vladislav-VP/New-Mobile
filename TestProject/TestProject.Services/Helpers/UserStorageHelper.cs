using System.Threading.Tasks;

using Xamarin.Essentials;

using TestProject.Entities;
using TestProject.Services.Helpers.Interfaces;
using TestProject.Services.Repositories.Interfaces;

namespace TestProject.Services.Helpers
{
    public class UserStorageHelper : IUserStorageHelper
    {
        private static readonly string _credentialsKey = "current_user";

        private readonly IUserRepository _userRepository;

        public UserStorageHelper(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Save(int id)
        {
            await SecureStorage.SetAsync(_credentialsKey, id.ToString());
        }

        public async Task<User> Get()
        {
            string key = await SecureStorage.GetAsync(_credentialsKey);
            if (key == null)
            {
                return null;
            }

            int id = int.Parse(key);
            User user = await _userRepository.Find(id);
            return user;
        }

        public void Clear()
        {
            SecureStorage.RemoveAll();
        }
    }
}
