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
using System.Threading.Tasks;
using SQLite;
using TestProject.Entities;
using Android.Support.V4.Widget;
using Android.Support.V4.View;
using Android.Views.InputMethods;

namespace TestProject.Droid.Views
{
    [MvxActivityPresentation]
    [Activity(Label = "Task list", LaunchMode = LaunchMode.SingleTop)]
    public class MainView : MvxAppCompatActivity<MainViewModel>
    {
        public DrawerLayout DrawerLayout { get; set; }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            UserDialogs.Init(this);

            SetContentView(Resource.Layout.MainView);

            DrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            //DrawerLayout.Visibility = ViewStates.Invisible;

            if (bundle == null)
            {
                ViewModel.ShowLoginScreenCommand.Execute(null);
            }
        }

        public override void OnBackPressed()
        {
            if (DrawerLayout != null && DrawerLayout.IsDrawerOpen(GravityCompat.Start))
            {
                DrawerLayout.CloseDrawers();
            }
            else
            {
                base.OnBackPressed();
            }
        }

        public void HideSoftKeyboard()
        {
            if (CurrentFocus == null)
            {
                return;
            }

            InputMethodManager manager = (InputMethodManager)GetSystemService(InputMethodService);
            manager.HideSoftInputFromWindow(CurrentFocus.WindowToken, 0);

            CurrentFocus.ClearFocus();
        }
    }
}