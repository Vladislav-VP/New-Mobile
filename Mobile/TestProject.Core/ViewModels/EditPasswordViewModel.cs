using System.Threading.Tasks;

using MvvmCross.Commands;
using MvvmCross.Navigation;

using TestProject.Entities;
using TestProject.Resources;
using TestProject.Services.DataHandleResults;
using TestProject.Services.Helpers;
using TestProject.Services.Helpers.Interfaces;
using TestProject.Services.Interfaces;

namespace TestProject.Core.ViewModels
{
    public class EditPasswordViewModel : BaseViewModel
    {
        private readonly IUserService _userService;

        private readonly IDialogsHelper _dialogsHelper;

        public EditPasswordViewModel(IMvxNavigationService navigationService, 
            IUserStorageHelper storage, IDialogsHelper dialogsHelper, IUserService userService)
            : base(navigationService, storage)
        {
            _dialogsHelper = dialogsHelper;
            _userService = userService;

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

        private async Task ChangePassword()
        {
            int userId = await _storage.Get();

            DataHandleResult<EditPasswordHelper> result = await _userService
                .ChangePassword(userId, OldPassword, NewPassword, NewPasswordConfirmation);

            if (result.IsSucceded)
            {
                await _navigationService.Close(this);
                _dialogsHelper.DisplayToastMessage(Strings.PasswordChangedMessage);
            }            
        }
    }
}
