using System.Threading.Tasks;

using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

using TestProject.Services.Helpers.Interfaces;

namespace TestProject.Core.ViewModels
{
    public abstract class BaseViewModel : MvxViewModel
    {
        // TODO: Remove this _bufferViewModel field! Axillary
        protected static MvxViewModel _bufferViewModel;

        protected readonly IMvxNavigationService _navigationService;

        protected  readonly IUserStorageHelper _storage;

        public BaseViewModel(IMvxNavigationService navigationService, IUserStorageHelper storage)
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
