using System.Threading.Tasks;

using MvvmCross;
using MvvmCross.Navigation;

using TestProject.Core.ViewModels;

namespace TestProject.iOS.Extensions
{
    public static class MainViewModelExtensions
    {
        public static async Task StartTabViewModels(this MainViewModel mainViewModel)
        {
            IMvxNavigationService navigationService = Mvx.IoCProvider.Resolve<IMvxNavigationService>();

            await navigationService.Navigate<MenuViewModel>();
            await navigationService.Navigate<TodoItemListViewModel>();
        }
    }
}