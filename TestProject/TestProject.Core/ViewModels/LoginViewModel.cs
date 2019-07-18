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

namespace TestProject.Core.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly IUserRepository _userRepository;
        private readonly ITodoItemRepository _todoItemRepository;

        private string _userName;
        private string _password;

        public LoginViewModel(IMvxNavigationService navigationService)
            : base(navigationService)
        {
            _userRepository = new UserRepository();
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
            User user = new User { Name = this.UserName, Password = this.Password };
            bool isSuccess = await _userRepository
                .UserExists(user.Name);
            if (!isSuccess)
            {
                return;
            }

            StaticObjects.CurrentUser = await _userRepository.FindUser(UserName);
            StaticObjects.CurrentTodoItems = await _todoItemRepository.GetTodoItems(StaticObjects.CurrentUser.Id);
            var result = await _navigationService.Navigate<TodoListItemViewModel>();
        }
    }
}
