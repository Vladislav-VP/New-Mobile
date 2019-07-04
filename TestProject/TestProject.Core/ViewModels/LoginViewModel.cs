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

            RegistrateCommand = new MvxAsyncCommand(RegistrateUser);
            LoginCommand = new MvxAsyncCommand(Login);
        }

        public IMvxAsyncCommand LoginCommand { get; private set; }   // Initialaze
        
        public IMvxAsyncCommand RegistrateCommand { get; private set; }   // Initialaze

        private async Task RegistrateUser()
        {
            var result = await _navigationService.Navigate<RegistrateViewModel>();
        }

        private async Task Login()
        {
            var result = await _navigationService.Navigate<TodoListItemViewModel>();
        }
    }
}
