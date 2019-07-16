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
            ShowLoginViewModelCommand = new MvxAsyncCommand(() => _navigationService.Navigate<LoginViewModel>());
            ShowUserInfoViewModelCommand = new MvxAsyncCommand(() => _navigationService.Navigate<UserInfoViewModel>());
            ShowListTodoItemsViewModelCommand = new MvxAsyncCommand(() => _navigationService.Navigate<TodoListItemViewModel>());
        }

        public IMvxAsyncCommand ShowLoginViewModelCommand { get; private set; }

        public IMvxAsyncCommand ShowUserInfoViewModelCommand { get; private set; }

        public IMvxAsyncCommand ShowListTodoItemsViewModelCommand { get; private set; }

    }
}
