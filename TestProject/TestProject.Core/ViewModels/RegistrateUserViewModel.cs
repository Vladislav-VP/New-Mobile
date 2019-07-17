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

namespace TestProject.Core.ViewModels
{
    public class RegistrateUserViewModel : BaseViewModel
    {
        private readonly IUserRepository _userRepository;

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

        public RegistrateUserViewModel(IMvxNavigationService navigationService)
            : base(navigationService)
        {
            _userRepository = new UserRepository();

            RegistrateUserCommand = new MvxAsyncCommand(UserRegistrated);
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

            StaticObjects.CurrentUser = await _userRepository.FindUser(UserName);
            StaticObjects.CurrentTodoItems = new MvxObservableCollection<TodoItem>();
        }
    }
}
