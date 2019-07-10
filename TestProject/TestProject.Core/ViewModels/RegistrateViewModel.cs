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
        private readonly IGenericRepository<User> _userGenericRepository;

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
            _userGenericRepository = new GenericRepository<User>();

            RegistrateUserCommand = new MvxAsyncCommand(UserRegistrated);
        }

        public override async Task Initialize()
        {
            await base.Initialize();

            _user = new User();           
        }

        public IMvxAsyncCommand RegistrateUserCommand { get; private set; }

        private async Task UserRegistrated()
        {
            bool isSuccess = await _userGenericRepository.Insert(User);
            if (!isSuccess)
            {
                return;
            }

            await _navigationService.Navigate<TodoListItemViewModel>();
        }
    }
}
