using System.Threading.Tasks;

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
            GoToLoginCommand = new MvxAsyncCommand(async () =>
             await _navigationService.Navigate<LoginViewModel>());
            ShowTodoItemListCommand = new MvxAsyncCommand(async () =>
                  await _navigationService.Navigate<TodoItemListViewModel>());
            ShowMenuCommand = new MvxAsyncCommand(async () => 
                  await _navigationService.Navigate<MenuViewModel>());
        }
        
        public IMvxAsyncCommand ShowTodoItemListCommand { get; private set; }

        public IMvxAsyncCommand ShowMenuCommand { get; private set; }

        public async Task StartTabViewModels()
        {
            await _navigationService.Navigate<MenuViewModel>();
            await _navigationService.Navigate<TodoItemListViewModel>();
        }

        public IMvxAsyncCommand GoToLoginCommand { get; private set; }
    }
}
