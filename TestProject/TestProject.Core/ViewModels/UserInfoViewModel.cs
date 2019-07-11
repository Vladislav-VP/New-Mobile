using MvvmCross.ViewModels;
using MvvmCross.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using TestProject.Entity;

namespace TestProject.Core.ViewModels
{
    public class UserInfoViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;

        private User _user;
        public User User
        {
            get => _user;
            set
            {
                _user = value;
                RaisePropertyChanged(() => User);
            }
        }

        public UserInfoViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }


    }
}
