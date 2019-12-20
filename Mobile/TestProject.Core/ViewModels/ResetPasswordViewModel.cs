using MvvmCross.Commands;
using MvvmCross.Navigation;

using System.Threading.Tasks;
using TestProject.ApiModels.User;
using TestProject.Services.Interfaces;

namespace TestProject.Core.ViewModels
{
    public class ResetPasswordViewModel : BaseViewModel
    {
        private readonly IUserService _userService;

        public ResetPasswordViewModel(IMvxNavigationService navigationService, IUserService userService)
            : base(navigationService)
        {
            _userService = userService;

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
        }
    }
}
