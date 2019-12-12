using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross;
using MvvmCross.Platforms.Android.Presenters.Attributes;

using TestProject.Configurations;
using TestProject.Core.ViewModels;
using TestProject.Droid.Activities;
using TestProject.Resources;
using TestProject.Services.Helpers.Interfaces;

namespace TestProject.Droid.Fragments
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.content_frame, AddToBackStack = true)]
    [Register("testProject.droid.fragments.UserSettingsFragment")]
    public class UserSettingsFragment : BaseFragment<UserSettingsViewModel>
    {
        protected override int FragmentId => Resource.Layout.UserSettingsFragment;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = base.OnCreateView(inflater, container, savedInstanceState);

            TextView tvUserNameSettings = view.FindViewById<TextView>(Resource.Id.tvUserNameSettings);
            TextView tvSettings = view.FindViewById<TextView>(Resource.Id.tvSettings);
            Button btSaveUserName = view.FindViewById<Button>(Resource.Id.btSaveUserName);
            Button btChangePassword = view.FindViewById<Button>(Resource.Id.btChangePassword);
            Button btDeleteAccount = view.FindViewById<Button>(Resource.Id.btDeleteAccount);

            tvUserNameSettings.Text = Strings.UsernameTextViewLabel;
            tvSettings.Text = Strings.SettingsLabel;
            btSaveUserName.Text = Strings.SaveChangesButtonLabel;
            btChangePassword.Text = Strings.ChangePasswordButtonLabel;
            btDeleteAccount.Text = Strings.DeleteAccountButtonLabel;

            return view;
        }

        public override async void OnPause()
        {
            base.OnPause();

            IStorageHelper storage = Mvx.IoCProvider.Resolve<IStorageHelper>();
            string userId = await storage.Get(Constants.AccessTokenKey);
            if (string.IsNullOrEmpty(userId))
            {
                return;                
            }

            ((MainActivity)Activity)?.ViewModel.ShowMenuCommand?.Execute(null);
            ((MainActivity)Activity)?.ViewModel.ShowTodoItemListCommand?.Execute(null);
        }
    }
}