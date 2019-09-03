using System;
using System.Threading.Tasks;

using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.Navigation.EventArguments;
using MvvmCross.ViewModels;

using TestProject.Services.Helpers.Interfaces;

namespace TestProject.Core.ViewModels
{
    public abstract class BaseViewModel : MvxViewModel
    {
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

        public override async Task Initialize()
        {
            await base.Initialize();

            _navigationService.AfterNavigate += (object sender, IMvxNavigateEventArgs e) =>
            {
                Type viewModelType = e.ViewModel.GetType();
                if (viewModelType == typeof(RegistrationViewModel))
                {
                    _storage.Clear();
                }
            };
        }

        protected virtual async Task GoBack()
        {
            await _navigationService.Close(this);
        }       
    }
}
