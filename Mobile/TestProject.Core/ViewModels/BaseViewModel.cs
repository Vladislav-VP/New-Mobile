using System.Threading.Tasks;

using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

using TestProject.Services.Helpers.Interfaces;

namespace TestProject.Core.ViewModels
{
    public abstract class BaseViewModel : MvxViewModel
    {
        protected readonly IMvxNavigationService _navigationService;

        protected  readonly IStorageHelper _storage;

        public BaseViewModel(IMvxNavigationService navigationService, IStorageHelper storage)
        {
            _navigationService = navigationService;
            _storage = storage;

            GoBackCommand = new MvxAsyncCommand(GoBack);
        }

        public BaseViewModel(IMvxNavigationService navigationService)
            : this(navigationService, null) { }

        public IMvxAsyncCommand GoBackCommand { get; private set; }

        protected virtual async Task GoBack()
        {
            await _navigationService.Close(this);
        }       
    }
}
