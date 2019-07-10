using System;
using System.Collections.Generic;
using System.Text;
using MvvmCross.ViewModels;
using TestProject.Entity;
using System.Threading.Tasks;
using MvvmCross.Navigation;
using MvvmCross.Commands;
using TestProject.Repositories;
using TestProject.Repositories.Interfaces;

namespace TestProject.Core.ViewModels
{
    public class LoginViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly IGenericRepository<User> _genericUserRepository;

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

        public LoginViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
            _genericUserRepository = new GenericRepository<User>();

            RegistrateCommand = new MvxAsyncCommand(RegistrateUser);
            LoginCommand = new MvxAsyncCommand(Login);
        }

        public async override Task Initialize()
        {
            await base.Initialize();

            _user = new User();
        }

        public IMvxAsyncCommand LoginCommand { get; private set; }
        
        public IMvxAsyncCommand RegistrateCommand { get; private set; } 

        private async Task RegistrateUser()
        {
            var result = await _navigationService.Navigate<RegistrateViewModel>();
        }

        private async Task Login()
        {
            User searchedUser = await new UserRepository().FindUserByName(User.UserName);
            if (searchedUser == null || User.UserName != searchedUser.UserName)
            {
                return;
            }
            var result = await _navigationService.Navigate<TodoListItemViewModel>();
        }
    }
}
