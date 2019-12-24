using System.Threading.Tasks;

using MvvmCross.Commands;
using MvvmCross.Navigation;

using TestProject.ApiModels.User;
using TestProject.Services.Helpers.Interfaces;
using TestProject.Services.Interfaces;

namespace TestProject.Core.ViewModels
{
    public class ResetPasswordViewModel : BaseViewModel
    {
        private readonly IUserService _userService;

        private readonly IDialogsHelper _dialogsHelper;

        public ResetPasswordViewModel(IMvxNavigationService navigationService,
            IUserService userService, IDialogsHelper dialogsHelper)
            : base(navigationService)
        {
            _userService = userService;
            _dialogsHelper = dialogsHelper;

            SendRecoveryMailCommand = new MvxAsyncCommand(SendRecoveryMail);
        }

        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                RaisePropertyChanged(() => Email);
            }
        }

        public IMvxAsyncCommand SendRecoveryMailCommand { get; private set; }

        private async Task SendRecoveryMail()
        {
            var requestPassword = new RequestForgotPasswordUserApiModel
            {
                Email = Email
            };
            ResponseForgotPasswordUserApiModel response = await _userService.ForgotPassword(requestPassword);
            if (response.IsSuccess)
            {
                await _navigationService.Close(this);
            }
            _dialogsHelper.DisplayAlertMessage(response.Message);
        }
    }
}
