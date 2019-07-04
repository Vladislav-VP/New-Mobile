using System;
using System.Collections.Generic;
using System.Text;
using MvvmCross.ViewModels;
using TestProject.Core.Models;
using System.Threading.Tasks;
using MvvmCross.Navigation;
using MvvmCross.Commands;

namespace TestProject.Core.ViewModels
{
    public class LoginViewModel : MvxViewModel
    {
        private IMvxNavigationService _navigationService;

        private UserModel _user;

        public UserModel User
        {
            get => _user;
            set
            {
                _user = value;
                RaisePropertyChanged(() => User);
            }
        }


        public LoginViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public IMvxCommand LoginCommand { get; private set; }   // Initialaze
        
        public IMvxCommand RegisterCommand { get; private set; }   // Initialaze


    }
}
