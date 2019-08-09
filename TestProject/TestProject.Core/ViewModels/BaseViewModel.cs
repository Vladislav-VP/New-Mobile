using MvvmCross.ViewModels;
using MvvmCross.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using MvvmCross.Commands;
using System.Threading.Tasks;
using TestProject.Services.Helpers.Interfaces;
using TestProject.Entities;
using TestProject.Services.Helpers;

namespace TestProject.Core.ViewModels
{
    public abstract class BaseViewModel : MvxViewModel
    {
        protected readonly IMvxNavigationService _navigationService;

        protected  readonly IStorageHelper<User> _storage;

        public BaseViewModel(IMvxNavigationService navigationService, IStorageHelper<User> storage)
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
