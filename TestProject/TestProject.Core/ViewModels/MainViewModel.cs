using MvvmCross.Commands;
using MvvmCross.Navigation;

using TestProject.Entities;
using TestProject.Services.Helpers.Interfaces;

namespace TestProject.Core.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public MainViewModel(IMvxNavigationService navigationService, IStorageHelper<User> storage)
            : base(navigationService, storage)
        {
            ShowListTodoItemsViewModelCommand = new MvxAsyncCommand(async () =>
                  await _navigationService.Navigate<TodoListItemViewModel>());
            ShowMenuViewModelCommand = new MvxAsyncCommand(async () =>
                  await _navigationService.Navigate<MenuViewModel>());
        }
        
        public IMvxAsyncCommand ShowListTodoItemsViewModelCommand { get; private set; }

        public IMvxAsyncCommand ShowMenuViewModelCommand { get; private set; }
    }
}
