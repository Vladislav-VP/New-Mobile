using System;
using System.Collections.Generic;
using System.Text;
using MvvmCross.ViewModels;
using MvvmCross.Navigation;
using MvvmCross.Commands;
using TestProject.Core.Models;
using System.Threading.Tasks;

namespace TestProject.Core.ViewModels
{
    public class RegistrateViewModel : MvxViewModel
    {
        private IMvxNavigationService _navigationService;

        private UserModel _user;

        public UserModel User
        {
            get => _user;
            set
            {
                _user = value;
                RaisePropertyChanged(() => User);
            }
        }

        public RegistrateViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public IMvxAsyncCommand<UserModel> RegistrateUserCommand { get; private set; }

        private async Task UserRegistrated()
        {
            var result = await _navigationService.Navigate<TodoListItemViewModel>();
            // Write logic
        }
    }
}
