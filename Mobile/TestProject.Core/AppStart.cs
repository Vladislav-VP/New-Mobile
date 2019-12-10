using System.Threading.Tasks;

using MvvmCross.Navigation;
using MvvmCross.ViewModels;

using TestProject.Core.ViewModels;
using TestProject.Services.Helpers.Interfaces;

namespace TestProject.Core
{
    public class AppStart : MvxAppStart
    {
        private readonly IStorageHelper _storage;

        public AppStart(IMvxApplication application, IMvxNavigationService navigationService, IStorageHelper storage)
            : base(application, navigationService)
        {
            _storage = storage;
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
                string token = await _storage.Get();
                if (string.IsNullOrEmpty(token))
                {
                    source.SetResult(false);
                    return;
                }
                source.SetResult(token != null);
            });

            var isAuthenticated = source.Task.Result;

            return isAuthenticated;
        }
    }
}
