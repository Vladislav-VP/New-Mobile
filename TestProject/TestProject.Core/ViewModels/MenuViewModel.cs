using System;
using System.Collections.Generic;
using System.Text;
using MvvmCross.ViewModels;
using MvvmCross.Navigation;
using MvvmCross.Commands;

namespace TestProject.Core.ViewModels
{
    public class MenuViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;

        public MenuViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;

            LogoutCommand = new MvxAsyncCommand(() => _navigationService.Navigate<LoginViewModel>());
            GotoSettingsCommand = new MvxAsyncCommand(() => _navigationService.Navigate<UserInfoViewModel>());
        }

        public IMvxAsyncCommand LogoutCommand { get; private set; }

        public IMvxAsyncCommand GotoSettingsCommand { get; private set; }

    }
}
