using System;
using System.Collections.Generic;

using Acr.UserDialogs;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Views;
using Android.Views.InputMethods;
using MvvmCross;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Plugin.CurrentActivity;
using Plugin.Permissions;

using TestProject.Core.ViewModels;
using TestProject.Droid.Fragments;
using TestProject.Droid.Helpers.Interfaces;

namespace TestProject.Droid.Activities
{
    [MvxActivityPresentation]
    [Activity(Label = "Task list", 
        Theme = "@style/AppTheme", 
        LaunchMode = LaunchMode.SingleTop, 
        Name = "testProject.droid.activities.MainActivity")]
    public class MainActivity : MvxAppCompatActivity<MainViewModel>
    {
        private readonly IActivityReplaceHelper _activityReplaceHelper;

        public MainActivity()
        {
            _activityReplaceHelper = Mvx.IoCProvider.Resolve<IActivityReplaceHelper>();
        }

        public DrawerLayout DrawerLayout { get; set; }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Android.Resource.Id.Home)
            {
                DrawerLayout.OpenDrawer(GravityCompat.RelativeHorizontalGravityMask);
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        public override void OnBackPressed()
        {
            bool isDrawerOpen = (bool)DrawerLayout?.IsDrawerOpen(GravityCompat.RelativeHorizontalGravityMask);
            if (isDrawerOpen)
            {
                DrawerLayout.CloseDrawers();
                return;
            }

            CloseCurrentFragment(typeof(TodoItemListFragment));
        }
        
        public void HideSoftKeyboard()
        {
            if (CurrentFocus == null)
                return;

            InputMethodManager inputMethodManager = (InputMethodManager)GetSystemService(InputMethodService);
            inputMethodManager.HideSoftInputFromWindow(CurrentFocus.WindowToken, 0);

            CurrentFocus.ClearFocus();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            _activityReplaceHelper.ReplaceActivity(this);


            UserDialogs.Init(this);
                        
            SetContentView(Resource.Layout.MainActivity);
            Window.AddFlags(WindowManagerFlags.Fullscreen);

            ViewModel.ShowMenuCommand?.Execute(null);
            ViewModel.ShowTodoItemListCommand?.Execute(null);

            CrossCurrentActivity.Current.Init(this, bundle);

            DrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
        }

        private void CloseCurrentFragment(Type fragmentType)
        {
            IList<Android.Support.V4.App.Fragment> fragments = SupportFragmentManager.Fragments;
            if (fragments.Count == 0)
            {
                return;
            }

            MvxFragment currentFragment = (MvxFragment)fragments[fragments.Count - 1];
            Type currentFragmentType = currentFragment.GetType();
            if (currentFragmentType == fragmentType)
            {
                Finish();
                return;
            }

            BaseViewModel viewModel = (BaseViewModel)currentFragment.ViewModel;
            viewModel.GoBackCommand?.Execute(null);
        }
    }
}