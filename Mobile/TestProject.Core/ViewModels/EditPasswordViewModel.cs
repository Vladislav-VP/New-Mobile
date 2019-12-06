using System.Threading.Tasks;

using MvvmCross.Commands;
using MvvmCross.Navigation;

using TestProject.ApiModels.User;
using TestProject.Services.Helpers.Interfaces;
using TestProject.Services.Interfaces;

namespace TestProject.Core.ViewModels
{
    public class EditPasswordViewModel : BaseViewModel
    {
        private readonly IUserService _userService;

        private readonly IDialogsHelper _dialogsHelper;

        public EditPasswordViewModel(IMvxNavigationService navigationService, 
            IStorageHelper storage, IDialogsHelper dialogsHelper, IUserService userService)
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
            string userId = await _storage.Get();

            var user = new RequestChangePasswordUserApiModel
            {
                Id = userId,
                OldPasswordConfirmation = OldPassword,
                NewPassword = NewPassword,
                NewPasswordConfirmation = NewPasswordConfirmation
            };
            ResponseChangePasswordUserApiModel response = await _userService.ChangePassword(user);
            if (!response.IsSuccess)
            {
                _dialogsHelper.DisplayAlertMessage(response.Message);
                return;
            }
            if (response.IsSuccess)
            {
                await _navigationService.Close(this);
                _dialogsHelper.DisplayToastMessage(response.Message);
            }
        }
    }
}
