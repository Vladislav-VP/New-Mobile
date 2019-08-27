using System.Threading.Tasks;

using MvvmCross;
using Xamarin.Essentials;

using TestProject.Configurations;
using TestProject.Entities;
using TestProject.Services.Helpers.Interfaces;
using TestProject.Services.Repositories.Interfaces;

namespace TestProject.Services.Helpers
{
    public class UserStorageHelper : IUserStorageHelper
    {
        public async Task Save(int id)
        {
            await SecureStorage.SetAsync(Constants.CredentialsKey, id.ToString());
        }

        public async Task<User> Get()
        {
            string key = await SecureStorage.GetAsync(Constants.CredentialsKey);
            if (key == null)
            {
                return null;
            }

            int id = int.Parse(key);
            IUserRepository userRepository = Mvx.IoCProvider.Resolve<IUserRepository>();
            return await userRepository.Find(id);
        }

        public void Clear()
        {
            SecureStorage.RemoveAll();
        }
    }
}
