using System.Threading.Tasks;

using Xamarin.Essentials;

using TestProject.Services.Helpers.Interfaces;

namespace TestProject.Services.Helpers
{
    public class StorageHelper : IStorageHelper
    {
        private static readonly string _credentialsKey = "current_user";

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
