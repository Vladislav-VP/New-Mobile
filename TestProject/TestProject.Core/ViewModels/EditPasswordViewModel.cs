using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using TestProject.Resources;
using TestProject.Entities;
using TestProject.Services.Helpers;
using TestProject.Services.Helpers.Interfaces;
using TestProject.Services.Repositories;
using TestProject.Services.Repositories.Interfaces;

namespace TestProject.Core.ViewModels
{
    public class EditPasswordViewModel : BaseViewModel
    {
        private readonly IUserRepository _userRepository;

        private User _currentUser;

        private readonly IDialogsHelper _dialogsHelper;

        private string _currentPassword;
        private string _newPassword1;
        private string _newPassword2;
        
        public EditPasswordViewModel(IMvxNavigationService navigationService)
            : base(navigationService)
        {
            _userRepository = new UserRepository();

            _dialogsHelper = new UserDialogsHelper();

            PasswordChangedCommand = new MvxAsyncCommand(PasswordChanged);
        }

        public string CurrentPassword
        {
            get => _currentPassword;
            set
            {
                _currentPassword = value;
                RaisePropertyChanged(() => CurrentPassword);
            }
        }

        public string NewPassword1
        {
            get => _newPassword1;
            set
            {
                _newPassword1 = value;
                RaisePropertyChanged(() => NewPassword1);
            }
        }

        public string NewPassword2
        {
            get => _newPassword2;
            set
            {
                _newPassword2 = value;
                RaisePropertyChanged(() => NewPassword2);
            }
        }

        public IMvxAsyncCommand PasswordChangedCommand { get; private set; }

        public override async Task Initialize()
        {
            await base.Initialize();

            _currentUser = await _storage.Load();
        }

        private bool ConfirmCurrerntPassword()
        {
            return CurrentPassword == _currentUser.Password;
        }

        private bool NewPasswordsEqual()
        {
            return NewPassword1 == NewPassword2;
        }

        private async Task<bool> TryChangePassword()
        {
            if (!ConfirmCurrerntPassword())
            {
                _dialogsHelper.AlertMessage(Strings.IncorrectCurrentPasswordMessage);
                return false;
            }

            if (!NewPasswordsEqual())
            {
                _dialogsHelper.AlertMessage(Strings.PasswordsNotCorrespondMessage);
                return false;
            }

            _currentUser.Password = NewPassword1;
            DataValidationHelper validationHelper = new DataValidationHelper();
            if (!validationHelper.PasswordIsValid(_currentUser))
            {
                _currentUser.Password = CurrentPassword;
                _dialogsHelper.AlertMessage(validationHelper.ValidationErrors[0].ErrorMessage);
                var errors = validationHelper.ValidationErrors;
                return false;
            }

            await _userRepository.Update(_currentUser);
            return true;
        }

        private async Task PasswordChanged()
        {
            if(!await TryChangePassword())
            {
                return;
            }

            _dialogsHelper.ToastMessage(Strings.PasswordChangedMessage);
            var result = await _navigationService.Close(this);
        }
    }
}
