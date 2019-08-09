using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using TestProject.Resources;
using TestProject.Entities;
using TestProject.Services.Helpers;
using TestProject.Services.Helpers.Interfaces;
using TestProject.Services.Repositories.Interfaces;

namespace TestProject.Core.ViewModels
{
    public class EditPasswordViewModel : UserViewModel
    {
        private User _currentUser;

        private string _currentPassword;
        private string _newPassword1;
        private string _newPassword2;

        public EditPasswordViewModel(IMvxNavigationService navigationService, IUserRepository userRepository,
            IStorageHelper<User> userStorage, IValidationHelper validationHelper, IDialogsHelper dialogsHelper)
            : base(navigationService, userStorage, userRepository, validationHelper, dialogsHelper)
        {
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

            _currentUser = await _storage.Retrieve();
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
            bool userIsValid = _validationHelper.ObjectIsValid<User>(_currentUser, nameof(_currentUser.Password));
            bool validationErrorsEmpty = _validationHelper.ValidationErrors.Count == 0;
            if (!userIsValid && !validationErrorsEmpty)
            {
                _currentUser.Password = CurrentPassword;
                _dialogsHelper.AlertMessage(_validationHelper.ValidationErrors[0].ErrorMessage);
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

            await _navigationService.Navigate<UserSettingsViewModel>();
            await _navigationService.Close(this);
            _dialogsHelper.ToastMessage(Strings.PasswordChangedMessage);
        }
    }
}
