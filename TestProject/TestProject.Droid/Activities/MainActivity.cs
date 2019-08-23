using Acr.UserDialogs;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Views;
using Android.Views.InputMethods;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Plugin.CurrentActivity;
using Plugin.Permissions;

using TestProject.Core.ViewModels;

namespace TestProject.Droid.Activities
{
    [MvxActivityPresentation]
    [Activity(Label = "Task list", 
        Theme = "@style/AppTheme", 
        LaunchMode = LaunchMode.SingleTop, 
        Name = "testProject.droid.activities.MainActivity")]
    public class MainActivity : MvxAppCompatActivity<MainViewModel>
    {
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
            if (DrawerLayout != null && DrawerLayout.IsDrawerOpen(GravityCompat.RelativeHorizontalGravityMask))
            {
                DrawerLayout.CloseDrawers();
                return;
            }
                
            base.OnBackPressed();
        }

        public void HideSoftKeyboard()
        {
            if (CurrentFocus == null)
                return;

            InputMethodManager inputMethodManager = (InputMethodManager)GetSystemService(InputMethodService);
            inputMethodManager.HideSoftInputFromWindow(CurrentFocus.WindowToken, 0);

            CurrentFocus.ClearFocus();
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            UserDialogs.Init(this);
                        
            SetContentView(Resource.Layout.MainActivity);
            
            CrossCurrentActivity.Current.Init(this, bundle);

            DrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);

            if (bundle == null && ViewModel.ShowMenuViewModelCommand != null)
            {
                ViewModel.ShowMenuViewModelCommand.Execute(null);
            }

        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}