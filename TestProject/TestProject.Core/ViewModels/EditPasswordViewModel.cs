using System.Threading.Tasks;

using MvvmCross.Commands;
using MvvmCross.Navigation;

using TestProject.Entities;
using TestProject.Resources;
using TestProject.Services.Helpers.Interfaces;
using TestProject.Services.Repositories.Interfaces;

namespace TestProject.Core.ViewModels
{
    public class EditPasswordViewModel : BaseViewModel
    {
        private User _currentUser;

        private readonly IUserRepository _userRepository;

        private readonly IValidationHelper _validationHelper;

        private readonly IDialogsHelper _dialogsHelper;

        public EditPasswordViewModel(IMvxNavigationService navigationService, IUserRepository userRepository, 
            IUserStorageHelper storage, IValidationHelper validationHelper, IDialogsHelper dialogsHelper)             
            : base(navigationService, storage)
        {
            _userRepository = userRepository;
            _validationHelper = validationHelper;
            _dialogsHelper = dialogsHelper;

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

        protected async Task<bool> IsDataValid()
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
            bool isPasswordValid = _validationHelper.IsObjectValid(_currentUser, nameof(_currentUser.Password));
            if (!isPasswordValid)
            {
                _currentUser.Password = OldPassword;
                return false;
            }

            return true;
        }

        private async Task ChangePassword()
        {
            bool isPasswordValid = await IsDataValid();
            if (!isPasswordValid)
            {
                return;
            }

            await _userRepository.Update(_currentUser);
            await _navigationService.Close(this);
            _dialogsHelper.DisplayToastMessage(Strings.PasswordChangedMessage);
        }
    }
}
