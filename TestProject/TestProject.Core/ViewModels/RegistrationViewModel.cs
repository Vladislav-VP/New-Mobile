using System;
using System.Collections.Generic;
using System.Text;
using MvvmCross.ViewModels;
using MvvmCross.Navigation;
using MvvmCross.Commands;
using System.Threading.Tasks;
using TestProject.Entities;
using TestProject.Services.Repositories.Interfaces;
using TestProject.Services.Repositories;
using TestProject.Services;
using TestProject.Configurations.Interfaces;
using TestProject.Configurations;

namespace TestProject.Core.ViewModels
{
    public class RegistrationViewModel : BaseViewModel
    {
        private readonly IUserRepository _userRepository;

        private string _userName;
        private string _password;

        public RegistrationViewModel(IMvxNavigationService navigationService)
            : base(navigationService)
        {
            _userRepository = new UserRepository();

            RegistrateUserCommand = new MvxAsyncCommand(UserRegistrated);
        }

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

        public IMvxAsyncCommand RegistrateUserCommand { get; private set; }

        private async Task UserRegistrated()
        {
            await AddUser();
            await _navigationService.Navigate<TodoListItemViewModel>();
        }

        private async Task AddUser()
        {
            bool isSuccess = await _userRepository.Insert(new User { Name = this.UserName, Password = this.Password });
            if (!isSuccess)
            {
                return;
            }

            await SaveUserIntoStorage();
        }

        private async Task SaveUserIntoStorage()
        {
            User user = await _userRepository.FindUser(UserName);
            ILocalStorage<User> storage = new LocalStorage<User>();
            storage.Store(user);
        }
    }
}
