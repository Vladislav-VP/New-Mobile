using System;
using System.Collections.Generic;
using System.Text;
using MvvmCross.ViewModels;
using MvvmCross.Navigation;
using MvvmCross.Commands;
using System.Threading.Tasks;
using TestProject.Services;

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
            StaticObjects.CurrentUser = null;
            StaticObjects.CurrentTodoItems = null;

            var result = await _navigationService.Navigate<LoginViewModel>();
        }
    }
}
