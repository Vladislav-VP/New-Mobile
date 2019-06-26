using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace TestProject.Droid
{
    class SignUpDialog : DialogFragment
    {
        private EditText _txtUserName;
        private EditText _txtEmail;
        private EditText _txtPassword;
        private Button _btnSaveSignUp;

        public event EventHandler<SignUpEventArgs> _onSignUpComplete;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.registerDialog, container, false);
            _txtUserName = view.FindViewById<EditText>(Resource.Id.txtUsername);
            _txtEmail = view.FindViewById<EditText>(Resource.Id.txtEmail);
            _txtPassword = view.FindViewById<EditText>(Resource.Id.txtPassword);
            _btnSaveSignUp = view.FindViewById<Button>(Resource.Id.btnSave);
            _btnSaveSignUp.Click += BtnSaveSignUp_Click;
            return view;
        }

        private void BtnSaveSignUp_Click(object sender, EventArgs e)
        {
            _onSignUpComplete(this, 
                new SignUpEventArgs(_txtUserName.Text, _txtEmail.Text, _txtPassword.Text));
            Dismiss();
        }
    }
}