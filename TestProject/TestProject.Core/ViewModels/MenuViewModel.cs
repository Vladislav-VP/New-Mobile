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

namespace TestProject.Core.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        private string _userName;

        public MenuViewModel(IMvxNavigationService navigationService)
            : base(navigationService)
        {

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

            _userName = (await new CredentialsStorageHelper().Load()).Name;
        }

        private async Task Logout()
        {

            var result = await _navigationService.Navigate<LoginViewModel>();
        }
    }
}
