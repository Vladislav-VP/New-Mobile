using System.Threading.Tasks;

using Xamarin.Essentials;

using TestProject.Services.Helpers.Interfaces;

namespace TestProject.Services.Helpers
{
    public class StorageHelper : IStorageHelper
    {
        public async Task Save(string key, string value)
        {
            await SecureStorage.SetAsync(key, value);
        }

        public async Task<string> Get(string key)
        {
            string value = await SecureStorage.GetAsync(key);
            return value;
        }

        public void Clear()
        {
            SecureStorage.RemoveAll();
        }
    }
}
