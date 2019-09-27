using System.Threading.Tasks;

using MvvmCross.Commands;
using MvvmCross.Navigation;

using TestProject.Entities;
using TestProject.Resources;
using TestProject.Services.DataHandleResults;
using TestProject.Services.Helpers.Interfaces;
using TestProject.Services.Interfaces;

namespace TestProject.Core.ViewModels
{
    public class EditPasswordViewModel : BaseViewModel
    {
        private readonly IEditPasswordService _editPasswordService;

        private readonly IDialogsHelper _dialogsHelper;

        public EditPasswordViewModel(IMvxNavigationService navigationService, IUserStorageHelper storage, 
            IDialogsHelper dialogsHelper, IEditPasswordService editPasswordService)
            : base(navigationService, storage)
        {
            _dialogsHelper = dialogsHelper;
            _editPasswordService = editPasswordService;

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
            User currentUser = await _storage.Get();
            var result = new EditPasswordResult
            {
                Data = currentUser,
                OldPasswordConfirmation = OldPassword,
                NewPassword = NewPassword,
                NewPasswordConfirmation = NewPasswordConfirmation
            };

            await _editPasswordService.ChangePassword(result);
            if (result.IsSucceded)
            {
                await _navigationService.Close(this);
                _dialogsHelper.DisplayToastMessage(Strings.PasswordChangedMessage);
            }            
        }
    }
}
