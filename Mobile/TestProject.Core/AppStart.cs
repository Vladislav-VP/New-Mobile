using System.Threading.Tasks;

using MvvmCross.Navigation;
using MvvmCross.ViewModels;

using TestProject.ApiModels.User;
using TestProject.Configurations;
using TestProject.Core.ViewModels;
using TestProject.Services.Helpers.Interfaces;
using TestProject.Services.Interfaces;

namespace TestProject.Core
{
    public class AppStart : MvxAppStart
    {
        private readonly IStorageHelper _storage;

        private readonly IUserService _userService;

        public AppStart(IMvxApplication application, IMvxNavigationService navigationService, IStorageHelper storage, IUserService userService)
            : base(application, navigationService)
        {
            _storage = storage;
            _userService = userService;
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
                string token = await _storage.Get(Constants.AccessTokenKey);
                if (string.IsNullOrEmpty(token))
                {
                    source.SetResult(false);
                    return;
                }
                ResponseRefreshAccessTokenUserApiModel response = await _userService.RefreshToken();
                source.SetResult(response.IsSuccess);
            });

            var isAuthenticated = source.Task.Result;

            return isAuthenticated;
        }
    }
}
