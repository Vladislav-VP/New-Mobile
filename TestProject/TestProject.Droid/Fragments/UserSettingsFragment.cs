using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross.Platforms.Android.Presenters.Attributes;

using TestProject.Core.ViewModels;
using TestProject.Resources;

namespace TestProject.Droid.Fragments
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.content_frame)]
    [Register("testProject.droid.views.UserInfoView")]
    public class UserSettingsFragment : BaseFragment<UserSettingsViewModel>
    {
        protected override int FragmentId => Resource.Layout.UserSettingsFragment;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = base.OnCreateView(inflater, container, savedInstanceState);

            InitializeAllControls(view);

            return view;
        }

        protected override void InitializeAllControls(View view)
        {
            TextView tvUserNameSettings = view.FindViewById<TextView>(Resource.Id.tvUserNameSettings);
            TextView tvSettings = view.FindViewById<TextView>(Resource.Id.tvSettings);
            Button btSaveUserName = view.FindViewById<Button>(Resource.Id.btSaveUserName);
            Button btChangePassword = view.FindViewById<Button>(Resource.Id.btChangePassword);
            Button btDeleteAccount = view.FindViewById<Button>(Resource.Id.btDeleteAccount);

            _controlInitializingHelper.SignControl(tvUserNameSettings, ControlsLabels.UsernameTextViewLabel);
            _controlInitializingHelper.SignControl(tvSettings, ControlsLabels.UserSettingsTextViewLabel);
            _controlInitializingHelper.SignControl(btSaveUserName, ControlsLabels.SaveUserNameButtonLabel);
            _controlInitializingHelper.SignControl(btChangePassword, ControlsLabels.ChangePasswordButtonLabel);
            _controlInitializingHelper.SignControl(btDeleteAccount, ControlsLabels.DeleteAccountButtonLabel);
        }
    }
}