using System;
using System.Collections.Generic;
using System.Text;
using MvvmCross.ViewModels;
using MvvmCross.Navigation;
using MvvmCross.Commands;
using System.Threading.Tasks;
using TestProject.Services;
using TestProject.Configurations.Interfaces;
using TestProject.Entities;
using TestProject.Configurations;

namespace TestProject.Core.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        public MenuViewModel(IMvxNavigationService navigationService)
            : base(navigationService)
        {
            ShowLoginViewModelCommand = new MvxAsyncCommand(Logout);
            ShowUserInfoViewModelCommand = new MvxAsyncCommand(() => _navigationService.Navigate<UserInfoViewModel>());
            ShowListTodoItemsViewModelCommand = new MvxAsyncCommand(() => _navigationService.Navigate<TodoListItemViewModel>());
        }

        public IMvxAsyncCommand ShowLoginViewModelCommand { get; private set; }

        public IMvxAsyncCommand ShowUserInfoViewModelCommand { get; private set; }

        public IMvxAsyncCommand ShowListTodoItemsViewModelCommand { get; private set; }

        private async Task Logout()
        {
            DeleteUser();

            var result = await _navigationService.Navigate<LoginViewModel>();
        }

        private void DeleteUser()
        {
            ILocalStorage<User> storage = new LocalStorage<User>();
            storage.Clear();
        }
    }
}
