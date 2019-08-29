﻿using System.Threading.Tasks;

using MvvmCross.Commands;
using MvvmCross.Navigation;

using TestProject.Core.Enums;
using TestProject.Entities;
using TestProject.Resources;
using TestProject.Services.Helpers.Interfaces;
using TestProject.Services.Repositories.Interfaces;

namespace TestProject.Core.ViewModels
{
    public class UserSettingsViewModel : UserViewModel
    {
        private User _currentUser;

        protected string _userName;

        public UserSettingsViewModel(IMvxNavigationService navigationService, IUserRepository userRepository, 
            IUserStorageHelper userStorage, IValidationHelper validationHelper, IDialogsHelper dialogsHelper)
            : base(navigationService, userStorage, userRepository, validationHelper, dialogsHelper)
        {
            UpdateUserCommand = new MvxAsyncCommand(UpdateUser);
            DeleteUserCommand = new MvxAsyncCommand(DeleteUser);
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

        public IMvxAsyncCommand UpdateUserCommand { get; private set; }

        public IMvxAsyncCommand DeleteUserCommand { get; private set; }

        public IMvxAsyncCommand ShowEditPasswordViewModelCommand { get; private set; }

        public async override Task Initialize()
        {
            await base.Initialize();

            _currentUser = await _storage.Get();

            _userName = _currentUser.Name;
        }

        protected override async Task GoBack()
        {
            YesNoCancelDialogResult result = await _navigationService.Navigate<CancelDialogViewModel, YesNoCancelDialogResult>();

            if (result == YesNoCancelDialogResult.Yes)
            {
                await UpdateUser();
                return;
            }

            await HandleDialogResult(result);
        }

        protected override async Task<bool> IsDataValid()
        {
            string oldUserName = _currentUser.Name;
            UserName = UserName.Trim();

            _currentUser.Name = UserName;
            bool isUserNameValid = _validationHelper.IsObjectValid(_currentUser);
            if (!isUserNameValid)
            {                
                _currentUser.Name = oldUserName;
                UserName = oldUserName;
                return false;
            }

            User retrievedUser = await _userRepository.GetUser(UserName);
            if (retrievedUser != null && retrievedUser.Id != _currentUser.Id)
            {
                _dialogsHelper.DisplayAlertMessage(Strings.UserAlreadyExistsMessage);
                _currentUser.Name = oldUserName;
                UserName = oldUserName;
                return false;
            }

            return true;
        }

        private async Task UpdateUser()
        {
            bool isUserValid = await IsDataValid();
            if (!isUserValid)
            {
                return;
            }

            await _userRepository.Update(_currentUser);
            _dialogsHelper.DisplayToastMessage(Strings.UserNameChangedMessage);

            await _navigationService.Navigate<TodoListItemViewModel>();
            // TODO: Correcrt issue with navigation to menu (username is not updated without navigating).
            await _navigationService.Navigate<MenuViewModel>();
        }

        private async Task DeleteUser()
        {
            bool isToDelete = await _dialogsHelper.IsConfirmed(Strings.DeleteMessageDialog);

            if (!isToDelete)
            {
                return;
            }

            await _userRepository.Delete(_currentUser);
            _storage.Clear();

            await _navigationService.Navigate<LoginViewModel>();
        }
    }
}
