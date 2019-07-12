using System;
using System.Collections.Generic;
using System.Text;
using MvvmCross.ViewModels;
using MvvmCross.Navigation;
using MvvmCross.Commands;

namespace TestProject.Core.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {

        public MenuViewModel(IMvxNavigationService navigationService)
            : base(navigationService)
        {
            LogoutCommand = new MvxAsyncCommand(() => _navigationService.Navigate<LoginViewModel>());
            ShowSettingsCommand = new MvxAsyncCommand(() => _navigationService.Navigate<UserInfoViewModel>());
            ShowTodoItemsCommand = new MvxAsyncCommand(() => _navigationService.Navigate<TodoListItemViewModel>());
        }

        public IMvxAsyncCommand LogoutCommand { get; private set; }

        public IMvxAsyncCommand ShowSettingsCommand { get; private set; }

        public IMvxAsyncCommand ShowTodoItemsCommand { get; private set; }

    }
}
