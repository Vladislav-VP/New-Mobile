using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using TestProject.Configurations;
using TestProject.Entities;
using TestProject.Services.Helpers.Interfaces;
using TestProject.Services.Repositories;
using Xamarin.Essentials;
using MvvmCross.IoC;
using MvvmCross;
using TestProject.Services.Repositories.Interfaces;

namespace TestProject.Services.Helpers
{
    public class StorageHelper : IStorageHelper<User>
    {
        public async Task Save(int id)
        {
            await SecureStorage.SetAsync(Constants.CredentialsKey, id.ToString());
        }

        public async Task<User> Retrieve()
        {
            var key = await SecureStorage.GetAsync(Constants.CredentialsKey);
            if (key == null)
            {
                return null;
            }

            int id = int.Parse(await SecureStorage.GetAsync(Constants.CredentialsKey));
            var userRepository = Mvx.IoCProvider.Resolve<IUserRepository>();
            return await userRepository.Find<User>(id);
        }

        public void Clear()
        {
            SecureStorage.RemoveAll();
        }
    }
}
