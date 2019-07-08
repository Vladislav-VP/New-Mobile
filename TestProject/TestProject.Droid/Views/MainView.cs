using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Acr.UserDialogs;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using MvvmCross.Platforms.Android.Views;
using TestProject.Core.ViewModels;
using Android.Content.PM;
using TestProject.Core;
using TestProject.Core.Services.Interfaces;
using TestProject.Core.Services;
using TestProject.Droid.Services;
using TestProject.Droid.Services.Interfaces;
using TestProject.Core.Models;
using System.Threading.Tasks;
using SQLite;

namespace TestProject.Droid.Views
{
    [MvxActivityPresentation]
    [Activity(Label = "Task list", LaunchMode = LaunchMode.SingleTop)]
    public class MainView : MvxAppCompatActivity<MainViewModel>
    {
        protected async override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            UserDialogs.Init(this);

            SetContentView(Resource.Layout.MainView);

            if (bundle == null)
            {
                ViewModel.LoadTodoItemListCommand.Execute(null);
            }
        }
    }
}