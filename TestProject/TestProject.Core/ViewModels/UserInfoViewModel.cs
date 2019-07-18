using MvvmCross.ViewModels;
using MvvmCross.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using TestProject.Entities;

namespace TestProject.Core.ViewModels
{
    public class UserInfoViewModel : BaseViewModel
    {
        private string _userName;
        private string _password;

        public UserInfoViewModel(IMvxNavigationService navigationService)
            : base(navigationService) { }

        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                RaisePropertyChanged(() => UserName);
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                RaisePropertyChanged(() => Password);
            }
        }
    }
}
