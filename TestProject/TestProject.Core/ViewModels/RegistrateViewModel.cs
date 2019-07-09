using System;
using System.Collections.Generic;
using System.Text;
using MvvmCross.ViewModels;
using MvvmCross.Navigation;
using MvvmCross.Commands;
using System.Threading.Tasks;
using TestProject.Entity;
using TestProject.Repositories.Interfaces;
using TestProject.Repositories;

namespace TestProject.Core.ViewModels
{
    public class RegistrateViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly IDBRepository _repository;

        private User _user;

        public User User
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
            _repository = new DBRepository();

            RegistrateUserCommand = new MvxAsyncCommand(UserRegistrated);
        }

        public IMvxAsyncCommand RegistrateUserCommand { get; private set; }

        private async Task UserRegistrated()
        {
            await _repository.AddUser(User);
            await _navigationService.Navigate<TodoListItemViewModel>();
        }
    }
}
