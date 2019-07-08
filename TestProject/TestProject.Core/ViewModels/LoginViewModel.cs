using System;
using System.Collections.Generic;
using System.Text;
using MvvmCross.ViewModels;
using TestProject.Core.Models;
using System.Threading.Tasks;
using MvvmCross.Navigation;
using MvvmCross.Commands;
using TestProject.Core.Services;
using TestProject.Core.Services.Interfaces;

namespace TestProject.Core.ViewModels
{
    public class LoginViewModel : MvxViewModel
    {
        private IMvxNavigationService _navigationService;
        private IDBService _dBService;

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

        public IMvxAsyncCommand LoginCommand { get; private set; }
        
        public IMvxAsyncCommand RegistrateCommand { get; private set; } 

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
