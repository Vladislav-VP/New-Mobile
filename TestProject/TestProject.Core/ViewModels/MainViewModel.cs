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
            ShowTodoItemListCommand = new MvxAsyncCommand(async () =>
                  await _navigationService.Navigate<TodoItemListViewModel>());
        }
        
        public IMvxAsyncCommand ShowTodoItemListCommand { get; private set; }
    }
}
