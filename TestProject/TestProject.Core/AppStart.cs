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

        protected override async Task NavigateToFirstViewModel(object hint = null)
        {
            bool isAuthenticated = IsAuthenticated();

            if (!isAuthenticated)
            {
                await NavigationService.Navigate<LoginViewModel>();
            }
            if (isAuthenticated)
            {
                await NavigationService.Navigate<MainViewModel>();
            }
        }


        private bool IsAuthenticated()
        {
            TaskCompletionSource<bool> source = new TaskCompletionSource<bool>();

            Task.Run(async () =>
            {
                IUserStorageHelper storage = Mvx.IoCProvider.Resolve<IUserStorageHelper>();

                User user = await storage.Get();

                source.SetResult(user != null);
            });

            var isAuthenticated = source.Task.Result;

            return isAuthenticated;
        }
    }
}
