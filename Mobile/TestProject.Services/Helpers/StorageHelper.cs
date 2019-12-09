using System.Threading.Tasks;

using Xamarin.Essentials;

using TestProject.Services.Helpers.Interfaces;

namespace TestProject.Services.Helpers
{
    public class StorageHelper : IStorageHelper
    {
        private static readonly string _credentialsKey = "current_user";

        public async Task Save(string token)
        {
            await SecureStorage.SetAsync(_credentialsKey, token);
        }

        public async Task<string> Get()
        {
            string token = await SecureStorage.GetAsync(_credentialsKey);
            return token;
        }

        public void Clear()
        {
            SecureStorage.RemoveAll();
        }
    }
}
