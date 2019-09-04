using System.Threading.Tasks;

using MvvmCross;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

using TestProject.Core.ViewModels;
using TestProject.Entities;
using TestProject.Services.Helpers.Interfaces;

namespace TestProject.Core
{
    public class AppStart : MvxAppStart
    {
        public AppStart(IMvxApplication application, IMvxNavigationService navigationService) 
            : base(application, navigationService)
        {
        }

        protected async override Task NavigateToFirstViewModel(object hint = null)
        {

            IUserStorageHelper storage = Mvx.IoCProvider.Resolve<IUserStorageHelper>();
            User user = await storage.Get();
            if (user == null)
            {
                await NavigationService.Navigate<LoginViewModel>();
            }
            if (user != null)
            {
                await NavigationService.Navigate<TodoItemListViewModel>();
            }
        }
    }
}
