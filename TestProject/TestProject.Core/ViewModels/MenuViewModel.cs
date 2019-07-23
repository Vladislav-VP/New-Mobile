using System;
using System.Collections.Generic;
using System.Text;
using MvvmCross.ViewModels;
using MvvmCross.Navigation;
using MvvmCross.Commands;
using System.Threading.Tasks;
using TestProject.Services;
using TestProject.Entities;
using TestProject.Configurations;
using TestProject.Services.Storages.Interfaces;
using TestProject.Services.Storages;

namespace TestProject.Core.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        private string _userName;

        private readonly ILocalStorage<User> _storage;

        public MenuViewModel(IMvxNavigationService navigationService)
            : base(navigationService)
        {
            _storage = new LocalStorage<User>();

            ShowLoginViewModelCommand = new MvxAsyncCommand(Logout);
            ShowUserInfoViewModelCommand = new MvxAsyncCommand(() => _navigationService.Navigate<UserInfoViewModel>());
            ShowListTodoItemsViewModelCommand = new MvxAsyncCommand(() => _navigationService.Navigate<TodoListItemViewModel>());
        }

        public string UserName
        {
            get => _userName;
        }

        public IMvxAsyncCommand ShowLoginViewModelCommand { get; private set; }

        public IMvxAsyncCommand ShowUserInfoViewModelCommand { get; private set; }

        public IMvxAsyncCommand ShowListTodoItemsViewModelCommand { get; private set; }

        public async override Task Initialize()
        {
            await base.Initialize();

            User currentUser = _storage.Get();
            _userName = currentUser.Name;
        }

        private async Task Logout()
        {
            new LocalStorage<User>().Clear();

            var result = await _navigationService.Navigate<LoginViewModel>();
        }
    }
}
