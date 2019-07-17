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
using TestProject.Core.ViewModels;

namespace TestProject.Droid.Views
{
    [MvxActivityPresentation]
    [Activity(Label = "Task list", 
        Theme = "@style/AppTheme", 
        LaunchMode = LaunchMode.SingleTop, 
        Name = "testProject.droid.views.MainView")]
    public class MainView : MvxAppCompatActivity<MainViewModel>
    {
        public DrawerLayout DrawerLayout { get; set; }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            UserDialogs.Init(this);

            SetContentView(Resource.Layout.MainView);

            DrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);

            if (bundle == null)
            {                
                ViewModel.ShowListTodoItemsViewModelCommand.Execute(null);
                ViewModel.ShowMenuViewModelCommand.Execute(null);
            }
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    DrawerLayout.OpenDrawer(GravityCompat.RelativeHorizontalGravityMask);
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }

        public override void OnBackPressed()
        {
            if (DrawerLayout != null && DrawerLayout.IsDrawerOpen(GravityCompat.RelativeHorizontalGravityMask))
                DrawerLayout.CloseDrawers();
            else
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
    }
}