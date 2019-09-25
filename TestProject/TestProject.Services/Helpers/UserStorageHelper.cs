using System.Threading.Tasks;

using MvvmCross;
using Xamarin.Essentials;

using TestProject.Entities;
using TestProject.Services.Helpers.Interfaces;
using TestProject.Services.Repositories.Interfaces;

namespace TestProject.Services.Helpers
{
    public class UserStorageHelper : IUserStorageHelper
    {
        private static readonly string _credentialsKey = "current_user";

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
            IUserRepository userRepository = Mvx.IoCProvider.Resolve<IUserRepository>();
            User user = await userRepository.Find(id);
            return user;
        }

        public void Clear()
        {
            SecureStorage.RemoveAll();
        }
    }
}
