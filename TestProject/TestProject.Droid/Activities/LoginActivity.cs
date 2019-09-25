using Acr.UserDialogs;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Android.Widget;
using MvvmCross;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Platforms.Android.Presenters.Attributes;

using TestProject.Core.ViewModels;
using TestProject.Droid.Helpers.Interfaces;
using TestProject.Resources;

namespace TestProject.Droid.Activities
{
    [MvxActivityPresentation]
    [Activity(Label = "Log in",
        Theme = "@style/AppTheme",
        LaunchMode = LaunchMode.SingleTop,
        Name = "testProject.droid.activities.LoginActivity")]
    public class LoginActivity : MvxAppCompatActivity<LoginViewModel>
    {
        private readonly IActivityReplaceHelper _activityStorageHelper;

        public LoginActivity()
        {
            _activityStorageHelper = Mvx.IoCProvider.Resolve<IActivityReplaceHelper>();
        }
        
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            UserDialogs.Init(this);

            SetContentView(Resource.Layout.LoginActivity);
            Window.AddFlags(WindowManagerFlags.Fullscreen);

            _activityStorageHelper.ReplaceActivity(this);

            TextView tvUsername = FindViewById<TextView>(Resource.Id.tvUsername);
            TextView tvPassword = FindViewById<TextView>(Resource.Id.tvPassword);
            TextView tvWithoutAccount = FindViewById<TextView>(Resource.Id.tvWithoutAccount);
            Button btLogin = FindViewById<Button>(Resource.Id.btLogin);
            Button btGoToRegistration = FindViewById<Button>(Resource.Id.btGoToRegistration);

            tvUsername.Text = Strings.UsernameTextViewLabel;
            tvPassword.Text = Strings.PasswordTextViewLabel;
            tvWithoutAccount.Text = Strings.WithoutAccountPrompt;
            btLogin.Text = Strings.LoginButtonLabel;
            btGoToRegistration.Text = Strings.RegistrationButtonLabel;
        }
    }
}