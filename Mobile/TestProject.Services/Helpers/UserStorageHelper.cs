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

        public async Task<int> Get()
        {
            string userIdValue = await SecureStorage.GetAsync(_credentialsKey);
            if (userIdValue == null)
            {
                return 0;
            }

            int id = int.Parse(userIdValue);
            return id;
        }

        public void Clear()
        {
            SecureStorage.RemoveAll();
        }
    }
}
