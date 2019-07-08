using System;
using System.Collections.Generic;
using System.Text;
using MvvmCross.ViewModels;
using MvvmCross.Navigation;
using MvvmCross.Commands;
using TestProject.Core.Models;
using System.Threading.Tasks;
using TestProject.Core.Services.Interfaces;
using TestProject.Core.Services;

namespace TestProject.Core.ViewModels
{
    public class RegistrateViewModel : MvxViewModel
    {
        private IMvxNavigationService _navigationService;
        private IDBService _dBService;

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
            _dBService = new DBService();

            RegistrateUserCommand = new MvxAsyncCommand(UserRegistrated);
        }

        public IMvxAsyncCommand RegistrateUserCommand { get; private set; }

        private async Task UserRegistrated()
        {
            await _dBService.AddUser(User);
            await _navigationService.Navigate<TodoListItemViewModel>();
        }
    }
}
