using MvvmCross.ViewModels;
using MvvmCross.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using TestProject.Entities;
using System.Threading.Tasks;
using MvvmCross.Commands;
using TestProject.Services.Repositories.Interfaces;
using TestProject.Services.Repositories;
using TestProject.Services.Helpers;
using Acr.UserDialogs;
using TestProject.Services.Helpers.Interfaces;

namespace TestProject.Core.ViewModels
{
    public class UserSettingsViewModel : BaseViewModel
    {
        private readonly IUserRepository _userRepository;
        
        private readonly IUserDialogs _userDialogs;

        private string _userName;

        public UserSettingsViewModel(IMvxNavigationService navigationService, IUserDialogs userDialogs)
            : base(navigationService)
        {
            _userRepository = new UserRepository();

            _userDialogs = userDialogs;

            UserUpdatedCommand = new MvxAsyncCommand(UserUpdated);
            UserDeletedCommand = new MvxAsyncCommand(UserDeleted);
            ShowEditPasswordViewModelCommand = 
                new MvxAsyncCommand(async () => await _navigationService.Navigate<EditPasswordViewModel>());
        }

        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                RaisePropertyChanged(() => UserName);
            }
        }

        public IMvxAsyncCommand UserUpdatedCommand { get; private set; }

        public IMvxAsyncCommand UserDeletedCommand { get; private set; }

        public IMvxAsyncCommand ShowEditPasswordViewModelCommand { get; private set; }

        public async override Task Initialize()
        {
            await base.Initialize();

            User currentUser = await _storage.Load();

            _userName = currentUser.Name;
        }

        private async Task UserUpdated()
        {
            User currentUser = await _storage.Load();

            currentUser.Name = UserName;
            await _userRepository.Update(currentUser);

            await _navigationService.Navigate<TodoListItemViewModel>();
            await _navigationService.Navigate<MenuViewModel>();
        }

        private async Task UserDeleted()
        {
            var delete = await _userDialogs.ConfirmAsync(new UserDialogsHelper().ConfirmDelete());

            if (!delete)
            {
                return;
            }

            User currentUser = await _storage.Load();
            await _userRepository.Delete(currentUser);
            _storage.Clear();

            var result = await _navigationService.Navigate<LoginViewModel>();
        }
    }
}
