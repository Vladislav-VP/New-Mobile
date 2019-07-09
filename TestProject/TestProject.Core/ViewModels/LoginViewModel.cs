using System;
using System.Collections.Generic;
using System.Text;
using MvvmCross.ViewModels;
using TestProject.Entity;
using System.Threading.Tasks;
using MvvmCross.Navigation;
using MvvmCross.Commands;
using TestProject.Repositories;
using TestProject.Repositories.Interfaces;
namespace TestProject.Core.ViewModels
{
    public class LoginViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly IDBRepository _dBService;

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
