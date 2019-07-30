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
using TestProject.Resources;

namespace TestProject.Core.ViewModels
{
    public class UserSettingsViewModel : BaseViewModel
    {
        private readonly IUserRepository _userRepository;

        private readonly IDialogsHelper _dialogsHelper;

        private readonly IUserDialogs _userDialogs;

        private string _userName;

        private User _currentUser;

        public UserSettingsViewModel(IMvxNavigationService navigationService, IUserDialogs userDialogs)
            : base(navigationService)
        {
            _userRepository = new UserRepository();

            _dialogsHelper = new UserDialogsHelper();

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

            _currentUser = await _storage.Load();

            _userName = _currentUser.Name;
        }

        private async Task<bool> TryUpdateUserName()
        {
            string currentUserName = _currentUser.Name;
            UserName = UserName.Trim();

            _currentUser.Name = UserName;
            DataValidationHelper validationHelper = new DataValidationHelper();
            if (!validationHelper.UserNameIsValid(_currentUser))
            {
                _currentUser.Name = currentUserName;
                _dialogsHelper.ToastMessage(validationHelper.ValidationErrors[0].ErrorMessage);
                return false;
            }

            if (await _userRepository.UserExists(UserName) && UserName != currentUserName)
            {
                _dialogsHelper.ToastMessage(Strings.UserAlreadyExistsMessage);
                return false;
            }

            await _userRepository.Update(_currentUser);
            return true;
        }


        private async Task UserUpdated()
        {
            if(!await TryUpdateUserName())
            {
                return;
            }

            _dialogsHelper.ToastMessage(Strings.UserNameChangedMessage);
            await _navigationService.Navigate<TodoListItemViewModel>();
            await _navigationService.Navigate<MenuViewModel>();
        }

        private async Task UserDeleted()
        {
            var delete = await _dialogsHelper.ConfirmDelete();

            if (!delete)
            {
                return;
            }

            await _userRepository.Delete(_currentUser);
            _storage.Clear();

            var result = await _navigationService.Navigate<LoginViewModel>();
        }
    }
}
