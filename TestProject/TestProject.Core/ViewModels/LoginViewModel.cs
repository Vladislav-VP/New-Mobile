using System;
using System.Collections.Generic;
using System.Text;
using MvvmCross.ViewModels;
using TestProject.Entities;
using System.Threading.Tasks;
using MvvmCross.Navigation;
using MvvmCross.Commands;
using TestProject.Services.Repositories.Interfaces;
using TestProject.Services.Repositories;

namespace TestProject.Core.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private string _userName;
        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                RaisePropertyChanged(() => UserName);
            }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                RaisePropertyChanged(() => Password);
            }
        }

        public LoginViewModel(IMvxNavigationService navigationService)
            : base(navigationService)
        {
            LoginCommand = new MvxAsyncCommand(Login);
            ShowRegistrationScreenCommand = new MvxAsyncCommand(async ()
                => await _navigationService.Navigate<RegistrateUserViewModel>());            
            ShowMenuCommand = new MvxAsyncCommand(async () => await _navigationService.Navigate<MenuViewModel>());
        }

        public async override Task Initialize()
        {
            await base.Initialize();

            _userName = string.Empty;
            _password = string.Empty;
        }

        public IMvxAsyncCommand LoginCommand { get; private set; }
        
        public IMvxAsyncCommand ShowRegistrationScreenCommand { get; private set; }

        public IMvxAsyncCommand ShowMenuCommand { get; private set; }

        private async Task Login()
        {
            bool isSuccess = await new UserRepository()
                .UserExists(new User { UserName = this.UserName, Password = this.Password });
            if (!isSuccess)
            {
                return;
            }
            var result = await _navigationService.Navigate<TodoListItemViewModel>();
        }
    }
}
