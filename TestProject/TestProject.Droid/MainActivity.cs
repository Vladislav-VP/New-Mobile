using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;


namespace TestProject.Droid
{
    [Activity(Label = "options Menu", MainLauncher = true)]
    public class MainActivity : Activity
    {
        private Button _signUp;
        private Button _submitNewUser;
        private EditText _txtUserName;
        private EditText _txtEmail;
        private EditText _txtPassword;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
            _signUp = FindViewById<Button>(Resource.Id.btnSignUp);
            _submitNewUser = FindViewById<Button>(Resource.Id.btnSave);
            _txtUserName = FindViewById<EditText>(Resource.Id.txtUsername);
            _txtEmail = FindViewById<EditText>(Resource.Id.txtEmail);
            _txtPassword = FindViewById<EditText>(Resource.Id.txtPassword);

            _signUp.Click += (object sender, EventArgs e) =>
              {
                  FragmentTransaction transFrags = FragmentManager.BeginTransaction();
                  SignUpDialog signUpDialog = new SignUpDialog();
                  signUpDialog.Show(transFrags, "Fragment Dialog");
                  signUpDialog._onSignUpComplete += SignUpDialog_onSignUpComplete;
              };
        }

        private void SignUpDialog_onSignUpComplete(object sender, SignUpEventArgs e)
        {
            StartActivity(typeof(UserRegisteredActivity));
        }
    }
}