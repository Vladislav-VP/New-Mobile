using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

namespace TestProject.Droid.Views
{
    [MvxActivityPresentation]
    [Activity(Label = "Task list", LaunchMode = LaunchMode.SingleTop)]
    public class MainView : MvxAppCompatActivity<MainViewModel>
    {
        protected override void OnCreate(Bundle bundle)
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