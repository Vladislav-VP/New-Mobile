using MvvmCross.Commands;
using MvvmCross.Navigation;

using TestProject.Services.Helpers.Interfaces;

namespace TestProject.Core.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public MainViewModel(IMvxNavigationService navigationService, IStorageHelper storage)
            : base(navigationService, storage)
        {
            GoToLoginCommand = new MvxAsyncCommand(async () =>
             await _navigationService.Navigate<LoginViewModel>());
            ShowTodoItemListCommand = new MvxAsyncCommand(async () =>
                  await _navigationService.Navigate<TodoItemListViewModel>());
            ShowMenuCommand = new MvxAsyncCommand(async () => 
                  await _navigationService.Navigate<MenuViewModel>());
        }
        
        public IMvxAsyncCommand ShowTodoItemListCommand { get; private set; }

        public IMvxAsyncCommand ShowMenuCommand { get; private set; }

        public IMvxAsyncCommand GoToLoginCommand { get; private set; }
    }
}
