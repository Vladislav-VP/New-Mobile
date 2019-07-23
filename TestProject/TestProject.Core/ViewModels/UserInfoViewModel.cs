using MvvmCross.ViewModels;
using MvvmCross.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using TestProject.Entities;
using System.Threading.Tasks;
using TestProject.Services.Storages.Interfaces;
using TestProject.Services.Storages;
using MvvmCross.Commands;
using TestProject.Services.Repositories.Interfaces;
using TestProject.Services.Repositories;
using TestProject.Services.Helpers;
using Acr.UserDialogs;

namespace TestProject.Core.ViewModels
{
    public class UserInfoViewModel : BaseViewModel
    {
        private readonly IUserRepository _userRepository;

        private readonly IUserDialogs _userDialogs;

        private readonly ILocalStorage<User> _storage;

        private string _userName;
        private string _password;

        public UserInfoViewModel(IMvxNavigationService navigationService, IUserDialogs userDialogs)
            : base(navigationService)
        {
            _userRepository = new UserRepository();

            _userDialogs = userDialogs;

            _storage = new LocalStorage<User>();

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

            User currentUser = _storage.Get();
            _userName = currentUser.Name;
            _password = currentUser.Password;
        }

        private async Task UserUpdated()
        {
            User currentUser = GetCurrentUser();
            if (!await UserValidationHelper.UserInfoIsValid(currentUser))
            {
                return;
            }

            await _userRepository.Update(currentUser);
            ReplaceUserInStorage(currentUser);

            await _navigationService.Navigate<TodoListItemViewModel>();
            await _navigationService.Navigate<MenuViewModel>();
        }

        private async Task UserDeleted()
        {
            var delete = await _userDialogs.ConfirmAsync(UserDialogsHelper.DeleteDialogConfig());

            if (!delete)
            {
                return;
            }

            User currentUser = GetCurrentUser();
            await _userRepository.Delete(currentUser);
            _storage.Clear();

            var result = await _navigationService.Navigate<LoginViewModel>();
        }

        private User GetCurrentUser()
        {
            User currentUser = _storage.Get();
            currentUser.Name = UserName;
            currentUser.Password = Password;
            return currentUser;
        }

        private void ReplaceUserInStorage(User user)
        {
            _storage.Clear();
            _storage.Store(user);
        }
    }
}
