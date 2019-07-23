using MvvmCross.ViewModels;
using MvvmCross.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using MvvmCross.Commands;
using System.Threading.Tasks;

namespace TestProject.Core.ViewModels
{
    public abstract class BaseViewModel : MvxViewModel
    {
        protected readonly IMvxNavigationService _navigationService;

        public BaseViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;

            GoBackCommand = new MvxAsyncCommand(GoBack);
        }

        public IMvxAsyncCommand GoBackCommand { get; private set; }

        protected virtual async Task GoBack()
        {
            await _navigationService.Close(this);
        }
    }
}
