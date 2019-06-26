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
    class SignUpEventArgs : EventArgs
    {
        private string _userName;
        private string _email;
        private string _password;

        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                _userName = value;
            }
        }

        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
            }
        }

        public string PassWord
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
            }
        }

        public SignUpEventArgs(string username, string email, string password)
            :base()
        {
            _userName = username;
            _email = email;
            _password = password;
        }
    }
}