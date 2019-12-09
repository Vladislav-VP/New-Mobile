using System.Threading.Tasks;

using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using TestProject.ApiModels.User;
using TestProject.Core.ViewModels;
using TestProject.Services.Helpers.Interfaces;
using TestProject.Services.Interfaces;

namespace TestProject.Core
{
    public class AppStart : MvxAppStart
    {
        private readonly IStorageHelper _storage;
        private readonly IUserService _userService;

        public AppStart(IMvxApplication application, IMvxNavigationService navigationService,
            IStorageHelper storage, IUserService userService)
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
                string userId = await _storage.Get();
                if (string.IsNullOrEmpty(userId))
                {
                    source.SetResult(false);
                    return;
                }
                GetProfileImageUserApiModel user = await _userService.GetUserWithImage();

                source.SetResult(user != null);
            });

            var isAuthenticated = source.Task.Result;

            return isAuthenticated;
        }
    }
}
