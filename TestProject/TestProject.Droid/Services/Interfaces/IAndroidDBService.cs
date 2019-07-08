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
using TestProject.Core.Models;

namespace TestProject.Droid.Services.Interfaces
{
    public interface IAndroidDBService
    {
        Task CreateDatabase();

        Task<UserModel> GetUser(UserModel user);

        Task AddUser(UserModel user);
    }
}