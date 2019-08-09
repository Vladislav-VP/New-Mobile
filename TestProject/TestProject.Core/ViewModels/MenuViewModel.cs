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
using TestProject.Services.Helpers;
using TestProject.Services.Helpers.Interfaces;

namespace TestProject.Core.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        private string _userName;

        public MenuViewModel(IMvxNavigationService navigationService, IStorageHelper<User> storage)
            : base(navigationService, storage)
        {

            ShowLoginViewModelCommand = new MvxAsyncCommand(Logout);
            ShowUserInfoViewModelCommand = new MvxAsyncCommand(() => _navigationService.Navigate<UserSettingsViewModel>());
            ShowListTodoItemsViewModelCommand = 
                new MvxAsyncCommand(() => _navigationService.Navigate<TodoListItemViewModel>());
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

        public IMvxAsyncCommand ShowLoginViewModelCommand { get; private set; }

        public IMvxAsyncCommand ShowUserInfoViewModelCommand { get; private set; }

        public IMvxAsyncCommand ShowListTodoItemsViewModelCommand { get; private set; }

        public async override Task Initialize()
        {
            await base.Initialize();

            UserName = (await _storage.Retrieve()).Name;
        }

        private async Task Logout()
        {
            _storage.Clear();
            var result = await _navigationService.Navigate<LoginViewModel>();
        }
    }
}
