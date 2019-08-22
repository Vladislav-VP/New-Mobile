using System.Linq;
using System.Threading.Tasks;

using MvvmCross.Commands;
using MvvmCross.Navigation;

using TestProject.Entities;
using TestProject.Resources;
using TestProject.Services.Helpers.Interfaces;
using TestProject.Services.Repositories.Interfaces;

namespace TestProject.Core.ViewModels
{
    public class EditPasswordViewModel : UserViewModel
    {
        private User _currentUser;
    
        public EditPasswordViewModel(IMvxNavigationService navigationService, IUserRepository userRepository,
            IUserStorageHelper userStorage, IValidationHelper validationHelper, IDialogsHelper dialogsHelper)
            : base(navigationService, userStorage, userRepository, validationHelper, dialogsHelper)
        {
            ChangePasswordCommand = new MvxAsyncCommand(ChangePassword);
        }

        private string _oldPassword;
        public string OldPassword
        {
            get => _oldPassword;
            set
            {
                _oldPassword = value;
                RaisePropertyChanged(() => OldPassword);
            }
        }

        private string _newPassword;
        public string NewPassword
        {
            get => _newPassword;
            set
            {
                _newPassword = value;
                RaisePropertyChanged(() => NewPassword);
            }
        }

        private string _newPasswordConfirmation;
        public string NewPasswordConfirmation
        {
            get => _newPasswordConfirmation;
            set
            {
                _newPasswordConfirmation = value;
                RaisePropertyChanged(() => NewPasswordConfirmation);
            }
        }

        public IMvxAsyncCommand ChangePasswordCommand { get; private set; }

        public override async Task Initialize()
        {
            await base.Initialize();

            _currentUser = await _storage.Get();
        }

        private async Task<bool> TryChangePassword()
        {
            if (OldPassword != _currentUser.Password)
            {
                _dialogsHelper.DisplayAlertMessage(Strings.IncorrectCurrentPasswordMessage);
                return false;
            }

            if (NewPassword != NewPasswordConfirmation)
            {
                _dialogsHelper.DisplayAlertMessage(Strings.PasswordsNotCorrespondMessage);
                return false;
            }

            _currentUser.Password = NewPassword;
            bool isValid = _validationHelper.ObjectIsValid<User>(_currentUser, nameof(_currentUser.Password));
            if (!isValid)
            {
                _currentUser.Password = OldPassword;
                string errorMessage = _validationHelper
                    .ValidationErrors
                    .FirstOrDefault()
                    .ErrorMessage;
                _dialogsHelper.DisplayAlertMessage(errorMessage);
                return false;
            }

            await _userRepository.Update(_currentUser);
            return true;
        }

        private async Task ChangePassword()
        {
            if(!await TryChangePassword())
            {
                return;
            }

            await _navigationService.Navigate<UserSettingsViewModel>();
            await _navigationService.Close(this);
            _dialogsHelper.DisplayToastMessage(Strings.PasswordChangedMessage);
        }
    }
}
