using Acr.UserDialogs;
using Android.App;
using Android.Content.PM;
using Android.OS;
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
        private readonly IControlSigningHelper _controlInitializingHelper;

        private readonly IActivityStorageHelper _activityStorageHelper;

        public LoginActivity()
        {
            _controlInitializingHelper = Mvx.IoCProvider.Resolve<IControlSigningHelper>();
            _activityStorageHelper = Mvx.IoCProvider.Resolve<IActivityStorageHelper>();
        }
        
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            UserDialogs.Init(this);

            SetContentView(Resource.Layout.LoginActivity);

            _activityStorageHelper.ReplaceActivity(this);

            InitializeAllControls();
        }

        private void InitializeAllControls()
        {
            TextView tvUsername = FindViewById<TextView>(Resource.Id.tvUsername);
            TextView tvPassword = FindViewById<TextView>(Resource.Id.tvPassword);
            TextView tvWithoutAccount = FindViewById<TextView>(Resource.Id.tvWithoutAccount);
            Button btLogin = FindViewById<Button>(Resource.Id.btLogin);
            Button btGoToRegistration = FindViewById<Button>(Resource.Id.btGoToRegistration);

            _controlInitializingHelper.SignControl(tvUsername, Strings.UsernameTextViewLabel);
            _controlInitializingHelper.SignControl(tvPassword, Strings.PasswordTextViewLabel);
            _controlInitializingHelper.SignControl(tvWithoutAccount, Strings.WithoutAccountPrompt);
            _controlInitializingHelper.SignControl(btLogin, Strings.LoginButtonLabel);
            _controlInitializingHelper.SignControl(btGoToRegistration, Strings.RegistrationButtonLabel);
        }
    }
}