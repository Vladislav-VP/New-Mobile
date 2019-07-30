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
using TestProject.Services;
using System.Collections.ObjectModel;
using TestProject.Configurations;
using System.IO;
using TestProject.Services.Helpers;
using TestProject.Resources;
using MvvmCross;

namespace TestProject.Core.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly IUserRepository _userRepository;
        private readonly ITodoItemRepository _todoItemRepository;

        private string _userName;
        private string _password;

        public LoginViewModel(IMvxNavigationService navigationService, IUserRepository userRepository)
            : base(navigationService)
        {
            _userRepository = userRepository;
            //_userRepository = new UserRepository();
            _todoItemRepository = new TodoItemRepository();

            LoginCommand = new MvxAsyncCommand(Login);
            ShowRegistrationScreenCommand = new MvxAsyncCommand(async ()
                => await _navigationService.Navigate<RegistrationViewModel>());
            ShowMenuCommand = new MvxAsyncCommand(async () => await _navigationService.Navigate<MenuViewModel>());
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

        public IMvxAsyncCommand LoginCommand { get; private set; }
        
        public IMvxAsyncCommand ShowRegistrationScreenCommand { get; private set; }

        public IMvxAsyncCommand ShowMenuCommand { get; private set; }

        private async Task Login()
        {
            var enteredUser = new User { Name = UserName, Password = Password };
            var userFromDatabase = await _userRepository.FindUser(enteredUser.Name);
            if (!enteredUser.Equals(userFromDatabase))
            {
                new UserDialogsHelper().ToastMessage(Strings.LoginErrorMessage);
                return;
            }

            await SaveUserIntoStorage();

            var result = await _navigationService.Navigate<TodoListItemViewModel>();
        }

        private async Task SaveUserIntoStorage()
        {
            User user = await _userRepository.FindUser(UserName);
            await _storage.Save(user);
        }
    }
}
