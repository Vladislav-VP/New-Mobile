using MvvmCross.Commands;
using MvvmCross.Navigation;

using TestProject.Services.Helpers.Interfaces;

namespace TestProject.Core.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public MainViewModel(IMvxNavigationService navigationService, IUserStorageHelper storage)
            : base(navigationService, storage)
        {
            ShowListTodoItemsViewModelCommand = new MvxAsyncCommand(async () =>
                  await _navigationService.Navigate<TodoItemListViewModel>());
            ShowMenuViewModelCommand = new MvxAsyncCommand(async () =>
                  await _navigationService.Navigate<MenuViewModel>());
        }
        
        public IMvxAsyncCommand ShowListTodoItemsViewModelCommand { get; private set; }

        public IMvxAsyncCommand ShowMenuViewModelCommand { get; private set; }
    }
}
