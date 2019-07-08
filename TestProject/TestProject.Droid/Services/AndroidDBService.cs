using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using TestProject.Core;
using TestProject.Core.Models;
using TestProject.Core.Services;
using TestProject.Core.Services.Interfaces;
using TestProject.Droid.Services.Interfaces;

namespace TestProject.Droid.Services
{
    public class AndroidDBService : IAndroidDBService
    {
        private IDBService _dBService;

        private string _path = System.IO.Path.Combine(Application.Context.FilesDir.AbsolutePath,Constants.DatabaseFile);

        public AndroidDBService()
        {
            _dBService = new DBService();
        }

        public async Task AddUser(UserModel user)
        {
            _dBService.AddUser(user);
        }

        public async Task CreateDatabase()
        {
            await _dBService.CreateDatabase();
        }

        public Task<UserModel> GetUser(UserModel user)
        {
            return _dBService.FindUser(user);
        }
    }
}