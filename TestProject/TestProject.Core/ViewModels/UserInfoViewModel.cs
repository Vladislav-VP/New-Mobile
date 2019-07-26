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
using TestProject.Services.Helpers.Interrfaces;

namespace TestProject.Core.ViewModels
{
    public class UserInfoViewModel : BaseViewModel
    {
        private readonly IUserRepository _userRepository;

        private readonly IStorageHelper<User> _storage;

        private readonly IUserDialogs _userDialogs;

        private string _userName;
        private string _password;

        public UserInfoViewModel(IMvxNavigationService navigationService, IUserDialogs userDialogs)
            : base(navigationService)
        {
            _userRepository = new UserRepository();

            _storage = new CredentialsStorageHelper();

            _userDialogs = userDialogs;

            UserUpdatedCommand = new MvxAsyncCommand(UserUpdated);
            UserDeletedCommand = new MvxAsyncCommand(UserDeleted);
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

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                RaisePropertyChanged(() => Password);
            }
        }

        public IMvxAsyncCommand UserUpdatedCommand { get; private set; }

        public IMvxAsyncCommand UserDeletedCommand { get; private set; }

        public async override Task Initialize()
        {
            await base.Initialize();

            User currentUser = await _storage.Load();

            _userName = currentUser.Name;
            _password = currentUser.Password;
        }

        private async Task UserUpdated()
        {
            User currentUser = await _storage.Load();

            await _userRepository.Update(currentUser);
            await ReplaceUserInStorage(currentUser);

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

            var result = await _navigationService.Navigate<LoginViewModel>();
        }

        private async Task ReplaceUserInStorage(User user)
        {
            _storage.Clear();
            await _storage.Save(user);
        }
    }
}
